using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3x3Grid {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public const int ROW_WIDTH = 3;
        public const int COL_HEIGHT = 3;

        public Random rnd = new Random();

        public int[,] board = new int[ROW_WIDTH, COL_HEIGHT];
        public List<int> usedNumbers = new List<int>();

        public MainWindow() {
            InitializeComponent();
        }

        private void RunCode(object sender, RoutedEventArgs e) {
            //run code here
            Output(txtInput.Text);
        }

        private void Output(string value) {
            //txtOutput.Text += value + Environment.NewLine;
            FillGrid();
            DisplayGrid();
            

        }

        private void ClearOutput(object sender, RoutedEventArgs e) {
            txtOutput.Text = "";
        }

        private void OutputChains(object sender, RoutedEventArgs e) {
           

            List<List<Node>> recordedPaths = new List<List<Node>>();

            for (int row = 0; row < ROW_WIDTH; row++) {
                for (int col = 0; col < COL_HEIGHT; col++) {

                    List<List<Node>> pathsFound = bfs(board, new Node(row, col), 9);
                    foreach (List<Node> path in pathsFound) {
                        if(path.Count > 1) {
                            bool pathCheckCleared = false;
                            if (recordedPaths.Count == 0) {
                                foreach (Node node in path) {
                                    //txtOutput.Text += "[" + node.row + "," + node.col + "]";
                                    txtOutput.Text += "[" + board[node.row, node.col] + "],";
                                }
                                recordedPaths.Add(path);
                                //txtOutput.Text += Environment.NewLine;
                            }
                            else {
                                foreach (List<Node> recordedPath in recordedPaths) {
                                    if (IsEqualPaths(recordedPath, path) == true) {
                                        pathCheckCleared = false;
                                        break;
                                    }
                                    else {
                                        pathCheckCleared = true;
                                    }

                                }

                                if (pathCheckCleared == true) {
                                    txtOutput.Text += Environment.NewLine;
                                    foreach (Node node in path) {
                                        txtOutput.Text += "[" + board[node.row, node.col] + "],";
                                    }
                                    recordedPaths.Add(path);

                                }


                            }

                        }

                    }

                    //txtOutput.Text += Environment.NewLine;
                }
            }

            
        }

        private void FillGrid() {
            usedNumbers.Clear();
            for (int row = 0; row < ROW_WIDTH; row++) {
                for (int col = 0; col < COL_HEIGHT; col++) {
                    int rndValue;

                    do {
                        rndValue = rnd.Next(0, 10);
                    } while (usedNumbers.Contains(rndValue));

                    board[row, col] = rndValue;
                    usedNumbers.Add(rndValue);
                }
            }
        }

        private void DisplayGrid() {
            for (int i = 0; i < ROW_WIDTH; i++) {
                for (int j = 0; j < COL_HEIGHT; j++) {
                    txtOutput.Text += board[i, j];
                    if ((j + 1) % 3 == 0) {
                        txtOutput.Text += Environment.NewLine;
                    }
                }
            }
        }

        public static IEnumerable<T> AdjacentElements<T>(T[,] arr, int row, int col) {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int i = row - 1; i <= row + 1; i++) {
                for (int j = col - 1; j <= col + 1; j++) {
                    if (i >= 0 && j >= 0 && i < rows && j < cols && !(i == row && j == col)) {

                        yield return arr[i, j];
                    }
                }
            }

        }


        public IEnumerable<Node> GetAdjNodes<T>(T[,] arr, Node node) {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int i = node.row - 1; i <= node.row + 1; i++) {
                for (int j = node.col - 1; j <= node.col + 1; j++) {
                    if (i >= 0 && j >= 0 && i < rows && j < cols && !(i == node.row && j == node.col)) {

                        yield return new Node(i, j);
                    }
                }
            }
        }

        public List<List<Node>> bfs(int[,] board, Node start, int targetSum) {
            //breadth first search

            //create a list to store found paths
            List<List<Node>> pathsFound = new List<List<Node>>();
            //create a queue
            Queue<List<Node>> queue = new Queue<List<Node>>();

            //push our starting node to queue
            var intialPath = new List<Node>();
            intialPath.Add(start); 
            queue.Enqueue(intialPath);

            while(queue.Count > 0) {
               
                //get first path from queue
                List<Node> p = queue.Dequeue();
                Node n = p.Last();

                if(GetPathSum(board,p) == targetSum) {
                    pathsFound.Add(p);
                }

                if (GetPathSum(board, p) > targetSum) {
                    continue;
                }

                var results = GetAdjNodes(board, n).ToArray();
                foreach(var result in results) {
                    if (p.Contains(result)) {
                        continue;
                    }
                    List<Node> newPath = new List<Node>(p);
                    newPath.Add(result);
               
                    queue.Enqueue(newPath);
                    
                }
                
            }
            return pathsFound;

        }

        public int GetPathSum(int[,] board, List<Node> path) {
            int sumOfNodes = 0;
            foreach(Node node in path) {
                sumOfNodes += board[node.row, node.col];  
            }
            return sumOfNodes;
        }

        public bool IsEqualPaths(List<Node> recordedPath, List<Node> newPath) {
           var rPath = new List<Node>(recordedPath).ToArray();
           var nPath = new List<Node>(newPath).ToArray();

            var rPathRows = new List<int>();
            var rPathCols = new List<int>();
            var nPathRows = new List<int>();
            var nPathCols = new List<int>();

            foreach (Node n in rPath) {
                rPathRows.Add(n.row);
                rPathCols.Add(n.col);
            }

            foreach (Node n in nPath) {
                nPathRows.Add(n.row);
                nPathCols.Add(n.col);
            }

            rPathRows.Sort();
            rPathCols.Sort();
            nPathRows.Sort();
            nPathCols.Sort();


            if (rPathRows.SequenceEqual(nPathRows) && rPathCols.SequenceEqual(nPathCols)){
                return true;
            } else
                return false;
        }



public struct Node {
            public int row;
            public int col;

            public Node(int r, int c) {
                this.row = r;
                this.col = c;
            }
        }


    }
}
