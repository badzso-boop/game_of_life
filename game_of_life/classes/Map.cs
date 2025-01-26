using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace game_of_life.classes
{
    public class Map
    {
        int[,] map;
        int x;
        int y;
        private Random rnd;

        public Map(int x, int y, Random rnd)
        {
            this.map = new int[x, y];
            this.x = x;
            this.y = y;
            this.Generate();
            this.Refresh();
            this.rnd = rnd;

        }

        public bool IsOccupied(int x, int y)
        {
            return this.map[x, y] != 0;
        }

        private void Refresh()
        {
            Console.Clear();
            this.Write();
            Thread.Sleep(1000);
        }

        public int[,] Generate()
        {
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    map[i, j] = 0;
                }
            }

            return this.map;
        }

        public void Write()
        {
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    if (map[i,j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    else if(map[i, j] > 1 && map[i, j] < 11)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(map[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(map[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        public int GetX() => x;
        public int GetY() => y;

        public void Set(int x, int y, int value)
        {
            this.map[x, y] = value;
            this.Refresh();
        }

        public void PlaceCell(Cell cell)
        {
            this.map[cell.X, cell.Y] = 1;
            this.Refresh();
        }

        public void PlaceCell(Cell cell, int value)
        {
            this.map[cell.X, cell.Y] = value;
            this.Refresh();
        }

        public void PlaceFood(Food food)
        {
            this.map[food.X, food.Y] = food.Value;
            this.Refresh();
        }

        public int[,] GetRadious(int x, int y, int radious)
        {
            int[,] circle = new int[(2 * radious + 1), (2 * radious + 1)];
            int k = 0;
            int e = 0;
            for (int i = x-radious; i < x+radious; i++)
            {
                for (int j = y-radious; j < y+radious; j++)
                {
                    if (i > x || i < 0)
                    {
                        circle[k, e] = 0;
                    }
                    else if (j > y || j < 0)
                    {
                        circle[k, e] = 0;
                    }
                    else
                    {
                        circle[k,e] = map[i, j];
                    }
                    e++;
                }
                k++;
                e = 0;
            }
            return circle;
        }

        public void MoveCell(Cell cell, int newX, int newY, List<Cell> allCells, List<(Cell cell, string cause)> deathCells)
        {
            if (IsOccupied(newX, newY))
            {
                Cell otherCell = allCells.FirstOrDefault(c => c.X == newX && c.Y == newY);

                if (otherCell != null)
                {
                    // Harc: A gyengébb sejt elpusztul
                    if (cell.Health > otherCell.Health)
                    {
                        deathCells.Add((otherCell, "fight"));
                        allCells.Remove(otherCell);
                        this.map[otherCell.X, otherCell.Y] = 0; // Töröld a térképről
                    }
                    else
                    {
                        deathCells.Add((cell, "fight"));
                        allCells.Remove(cell);
                        this.map[cell.X, cell.Y] = 0; // Töröld a térképről
                        return;
                    }
                }
            }

            this.map[cell.X, cell.Y] = 0; // Delete old position
            cell.X = newX;
            cell.Y = newY;
            this.map[newX, newY] = 1; // Set new position
            this.Refresh();
        }
    }
}
