using System;
using System.Collections.Generic;
using Raylib_cs;

namespace Maze_Generation
{
    class Cell
    {
        int _x;
        int _y;
        int _size;
        bool[] walls = { true, true, true, true };

        public int Row { get; set; }
        public int Column { get; set; }
        public bool Visited { get; set; }
        public bool Highlighted { get; set; } // The current cell
        public bool Backtracked { get; set; }

        public Cell(int row, int col, int size)
        {
            Row = row;
            Column = col;
            _size = size;
            _x = col * size;
            _y = row * size;
        }

        public void Display()
        {
            if (Visited) Raylib.DrawRectangle(_x, _y, _size, _size, Color.GREEN);
            if (Backtracked) Raylib.DrawRectangle(_x, _y, _size, _size, Color.DARKBLUE);
            if (Highlighted) Raylib.DrawRectangle(_x, _y, _size, _size, Color.RED);
            if (walls[0]) Raylib.DrawLine(_x, _y, _x + _size, _y, Color.WHITE);
            if (walls[1]) Raylib.DrawLine(_x + _size, _y, _x + _size, _y + _size, Color.WHITE);
            if (walls[2]) Raylib.DrawLine(_x + _size, _y + _size, _x, _y + _size, Color.WHITE);
            if (walls[3]) Raylib.DrawLine(_x, _y + _size, _x, _y, Color.WHITE);
        }

        public List<Cell> GetUnvisitedNeighbours(Cell[,] grid)
        {
            int[,] options = new int[4, 2]
            {
                { 1, 0 },
                { -1, 0 },
                { 0, 1 },
                { 0, -1 }
            };
            List<Cell> neighbours = new List<Cell>();
            for (int i = 0; i < options.GetLength(0); i++)
            {
                int nextRow = options[i, 0] + Row;
                int nextCol = options[i, 1] + Column;
                if (!(nextRow < 0 || nextCol < 0 || nextRow > grid.GetLength(0)-1 || nextCol > grid.GetLength(1)-1))
                {
                    if (!grid[nextRow, nextCol].Visited)
                    {
                        neighbours.Add(grid[nextRow, nextCol]);
                    }
                }
            }
            return neighbours;
        }

        public void RemoveWallBetween(Cell chosenCell)
        {
            if (Row < chosenCell.Row)
            {
                walls[2] = false;
                chosenCell.walls[0] = false;
            }
            else if (Row > chosenCell.Row)
            {
                walls[0] = false;
                chosenCell.walls[2] = false;
            }
            else if (Column > chosenCell.Column)
            {
                walls[3] = false;
                chosenCell.walls[1] = false;
            }
            else if (Column < chosenCell.Column)
            {
                walls[1] = false;
                chosenCell.walls[3] = false;
            }
        }
    }
}
