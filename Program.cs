using System;
using System.Collections.Generic;
using Raylib_cs;

namespace Maze_Generation
{
    class Program
    {
        const int WIDTH = 1800;
        const int HEIGHT = 900;
        const int SPACING = 30;

        static int rows;
        static int cols;

        static Cell[,] grid;
        static List<Cell> stack = new List<Cell>();
        static Cell currentCell;

        static Random random = new Random();

        static void Main(string[] args)
        {
            rows = HEIGHT / SPACING;
            cols = WIDTH / SPACING;
            grid = new Cell[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    grid[row, col] = new Cell(row, col, SPACING);
                }
            }
            grid[0, 0].Visited = true;
            stack.Add(grid[0, 0]);

            Raylib.InitWindow(WIDTH, HEIGHT, "Maze Generation - First Depth Search");
            //Raylib.SetTargetFPS(3);

            while (!Raylib.WindowShouldClose())
            {
                //while (stack.Count > 0)
                //{
                    Update();
                //}
                Display();
            }
            Raylib.CloseWindow();
        }

        static void Update()
        {
            if (stack.Count > 0)
            {
                currentCell = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                List<Cell> neighbours = currentCell.GetUnvisitedNeighbours(grid);
                if (neighbours.Count > 0)
                {
                    stack.Add(currentCell);
                    Cell chosenCell = neighbours[random.Next(neighbours.Count)];
                    currentCell.RemoveWallBetween(chosenCell);
                    currentCell.Highlighted = false;
                    currentCell = chosenCell;
                    currentCell.Visited = true;
                    currentCell.Highlighted = true;
                    stack.Add(currentCell);
                }
                else
                {
                    currentCell.Highlighted = false;
                    currentCell.Backtracked = true;
                }
            }
        }

        static void Display()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    grid[row, col].Display();
                }
            }
            //Raylib.DrawFPS(10, 10);
            Raylib.EndDrawing();
        }
    }
}
