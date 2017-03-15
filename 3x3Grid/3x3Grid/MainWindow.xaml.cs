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

namespace _3x3Grid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int ROW_WIDTH = 3;
        public const int COL_HEIGHT = 3;

        public Random rnd = new Random();

        public int[,] board = new int[ROW_WIDTH, COL_HEIGHT];
        public List<int> usedNumbers = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunCode(object sender, RoutedEventArgs e)
        {
            //run code here
            Output(txtInput.Text);
        }

        private void Output(string value)
        {
            //txtOutput.Text += value + Environment.NewLine;
            FillGrid();
            DisplayGrid();
            BoardIndex l = new BoardIndex(1, 2);
            txtOutput.Text += l.row;

        }

        private void ClearOutput(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = "";
        }

        private void OutputChains(object sender, RoutedEventArgs e)
        {
            for (int row = 0; row < ROW_WIDTH; row++)
            {
                for (int col = 0; col < COL_HEIGHT; col++)
                {
                    var results = AdjacentElements(board, row, col);
                    foreach (var result in results)
                    {
                        //txtOutput.Text += result;
                        TryToChain(board[row, col], result);
                    }
                    txtOutput.Text += Environment.NewLine;
                }
            }
            
        }

        private void FillGrid()
        {
            usedNumbers.Clear();
            for (int row = 0; row < ROW_WIDTH; row++)
            {
                for (int col = 0; col < COL_HEIGHT; col++)
                {
                    int rndValue;

                    do
                    {
                        rndValue = rnd.Next(0, 10);
                    } while (usedNumbers.Contains(rndValue));
           
                    board[row, col] = rndValue;
                    usedNumbers.Add(rndValue);
                }
            }
        }

        private void DisplayGrid()
        {
            for(int i = 0; i < ROW_WIDTH; i++)
            {
                for(int j = 0; j < COL_HEIGHT; j++)
                {
                    txtOutput.Text += board[i, j];
                    if((j+1) % 3 == 0)
                    {
                        txtOutput.Text += Environment.NewLine;
                    }
                }
            }
        }

        public static IEnumerable<T> AdjacentElements<T>(T[,] arr, int row, int col)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for(int i = row - 1; i <= row + 1; i++)
            {
                for (int j  = col - 1; j <= col + 1; j++)
                {
                    if(i >= 0 && j>=0 && i < rows && j < cols && !(i == row && j == col))
                    {
                        yield return arr[i, j];
                    }
                }
            }

        }

        public void TryToChain(int arrayValue, int adjacentValue)
        {
            int currentValue = arrayValue;
           
                currentValue += adjacentValue;
            
            if(currentValue == 9)
            {
                txtOutput.Text += "" + arrayValue + " " + adjacentValue;
            }       
        } 
    }

    public class BoardIndex
    {
        public int row;
        public int col;

        public BoardIndex(int r, int c)
        {
            this.row = r;
            this.col = c;
        }
    }
}
