
# Sodoku Solver

Welcom to my sodoku solver that solves all boards from 1x1 up to 25x25!

## Description

My project is an advanced Sudoku solver written in C#, that solves sodoku boards from 1x1 up to 16x16 in under **one second**, using backtracking and heurisitcs to optimize the backtracking.
<p align="left"> <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a> </p>

## The game rules

Sudoku is a logic-based number puzzle played on an N×N grid, typically 9×9, divided into N smaller regions (e.g., 3×3 in a 9×9 board). The goal is to fill the grid so that each row, column, and region contains every number from 1 to N exactly once. Variants exist with different sizes, such as 4×4, 16×16, and even 25×25, but the core rule remains the same—no duplicate numbers in any row, column, or region.

## The main logic

### The data structuers

The project has 2 main data structures are the board and the cell.<br />
The Board class represents a Sudoku board, storing its state in a two-dimensional array (a grid like a sodokus board) of ICell objects, Each cell is solved or unsolved. The UnsolvedCell class represents an unsolved cell, storing a set of possible values as a hashset along with its row, column, and box positions. The SolvedCell class represents a cell that has been solved, storing the fixed value along with its position. The board is initialized using an integer array, with each cell being populated as either UnsolvedCell or SolvedCell based on the input data. A good visual presentation is in the heuristics examples.

### Backtracking algorithm

I implemented a recursive backtracking algorithm to solve the Sudoku puzzle.<br />
The Board class holds the grid of ICell objects, which can be either UnsolvedCell or SolvedCell. The backtracking algorithm recursively tries different values for each UnsolvedCell, updating the board with SolvedCell values as it progresses. Heuristics help guide the search for a solution and optimize the solution. If a valid solution is found, the algorithm completes. Otherwise, it backtracks to previous cells and tries different options until the puzzle is solved or discovered to be unsolved.

### Heuristics

Heuristics are problem-solving strategies that use practical methods or rules of thumb to find a solution more efficiently, often by focusing on the most promising options.<br />
I used these:
* Naked single - A naked single occurs when a cell has only one possible value left, making it the  "single" valid option for that cell.<br />
<p align="center">
    <img src="https://hodoku.sourceforge.net/examples/fh02.png" width="45%">
    <img src="https://hodoku.sourceforge.net/examples/fh01.png" width="45%">
</p>
* Hidden single - A hidden single occurs when a number can only be placed in one unsolved cell within a row, column, or box, even if other numbers are possible options for that cell<br />
<p align="center">
    <img src="https://sudoku.com/img/post-images/1646984732-8.%20Hidden%20singles_1.png" width="45%">
    <img src="https://sudoku.com/img/post-images/1646984732-8.%20Hidden%20singles_2.png" width="45%">
</p>
* Naked pairs - A naked pair occurs when two unsolved cells in a row, column, or box have the same two possible values and no other possibility, and no cell in that group can contain those values. These two values can then be removed as options from other cells in the same row, column, or box.<br /><br />
<p align="center">
    <img src="https://hodoku.sourceforge.net/examples/l202.png" width="45%">
</p>
* Naked sets - A naked set occurs when a set of n unsolved cells in a unit (row, column, or box) have exactly n unique options between them. If the union of their options matches the number of cells (n), it means those n cells must take those n values, and these values can be removed as options from other cells in the same unit. naked sets generalize naked pairs.<br />
* Least options first - This heuristic finds the unsolved cell with the fewest possible values (options). It helps prioritize cells that are most constrained, aiding the solver in making decisions that narrow down possibilities.


## Executing program

* Clone the repository.
```
https://github.com/NoamTheGoat/SodokuProject.git
```
* Open File Explorer and go to the folder where the repository was cloned. Inside this folder, locate the .sln (solution) file and open it.
* Press F5 on your keyboard, or go to the Debug menu and select "Start Without Debugging".

## The UI  

When running the program, you will have four options:  

<p align="center">
    <img src="https://media-hosting.imagekit.io//41b2daf9043b4ef6/SodokuMenu.png?Expires=1833923632&Key-Pair-Id=K2ZIVPTIP2VGHC&Signature=BeYJCkb5m-GLLY3oik-PxlTXthY2Q8gyUtPspdAIQF0aTKQBmZR5d2sHTxkQ5wMH0o42G3pCSt45UTUdYA5uUpUHGWtYFljs4CSrtAsiZrGuBjpt04MXDFSG3Piymc~CR59lT0lVRbWF84NT2Qf-YgRE2uHuCVK0LQA~PNsMVKZ88lVYlL615afR8xcwWGfe5uMKVpBE~KZGLYUCUZijHJ6xmPMg-o5SXhdMwUt6rSjMEWIP9r-BRvpBsRgF8THdJcZWxjlEi~kuyAUtgNf6ODkYl7Q-kifUfsxP3RS-e-otn6oZYTI6Iw0xnk5sfd8LXmKKyN3yVm1HcCObgyT5qg__" width="45%">
</p>

1. **Enter Sudoku Board via Console**  
   - Press **`c`** and hit Enter.  
   - Enter a Sudoku board in the following format (81 digits, where `0` represents an empty cell):  
     ```
     000005080000601043000000000010500000000106000300000005530000061000000004000000000
     ```

2. **Read Sudoku Board from a File**  
   - Press **`f`** and hit Enter.  
   - Enter the full file path to a `.txt` file containing the board (e.g., `C:\Example\File\path.txt`).  

3. **Clear the Console**  
   - If too many boards have been displayed, you can clear the console by entering **`clr`**.  

4. **Exit the Program**  
   - To exit, press **`x`** or use the shortcut **`Ctrl + C`**.



