This is the folder for my solution to challenge 3 of IGN's CodeFoo Internship Application.

This folder contains the Visual Studio project folder and cs.files along with a screen shoot of the code being run.


Question Answers:
I started by constructing a 2D array to display a 3x3 grid of random numbers between 0 and 9. With this dont the next goal was to display all chains that equal 9 and fit the parameters given. I achieved this using breadth first search.
The first steps were to create a Node Struct to hold 2 ints row and col and a function that looped through the entire array and returned each elements adjacent elements as a Node. With both of these things I was able to implement my bfs method.

The method works by looping through the board, enquing the origin position as a path. It then Deques that queue into a list of node and finds the last node in the path. If a path is equal to my goal I add it to a list of confirmed paths and if a path is greater than my goal I skip all following code and move to the next queued path. The code beyond these checks find all adjacent nodes of the last node in my current path and returns a copy of my current path extended by the adjacent nodes as a new path.

Once this was working properly I finished up by adding in a couple of checks to exclude repeats ([2],[7],[0] | [2][0][7]) and single digit paths ([9]);

This can easily be implemented with a larger grid as the code was written with flexibility in mind. All you would need is to change the size of 2D and change the value used for checking minimum chain length to 1 less than the new grid size 