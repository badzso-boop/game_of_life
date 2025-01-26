using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_of_life.classes
{
    public class Cell
    {
        private string name;
        private int hunger;
        private int health;
        private int speed;
        private int vision;
        private int generation = 0;
        private int x;
        private int y;
        private Random rnd;
        private bool alive = true;

        public Cell(string name, int x, int y, Random random)
        {
            this.rnd = random;
            Name = name;
            Hunger = rnd.Next(1, 51);
            Health = rnd.Next(1, 21);
            Speed = rnd.Next(1, 6);
            Vision = rnd.Next(1, 4);
            X = x;
            Y = y;
        }

        public Cell(string name, int hunger, int health, int speed,int vision,int generation, int x, int y, Random random)
        {
            this.rnd = random;
            Name = name;
            Hunger = hunger;
            Health = health;
            Speed = speed;
            Vision = vision;
            Generation = generation;
            X = x;
            Y = y;
        }

        public string Name { get => name; set => name = value; }
        public int Hunger { get => hunger; set => hunger = value; }
        public int Health { get => health; set => health = value; }
        public int Speed { get => speed; set => speed = value; }
        public bool Alive { get => alive; set => alive = value; }
        public int Vision { get => vision; set => vision = value; }
        public int Generation { get => generation; set => generation = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        //x, y, dead
        public (int, int, bool) Move(int mapWidth, int mapHeight, int[,] FOV)
        {
            int direction = Scan(FOV);

            if (direction < 0)
                direction = rnd.Next(1, 5);

            int nextX = this.X;
            int nextY = this.Y;

            Hunger -= Speed;
            Health -= 1;

            if (Hunger <= 0 || Health <= 0)
            {
                return (nextX, nextY, true);
            }

            switch (direction)
            {
                case 1: // Left
                    if (nextY - 1 >= 0) nextY--;
                    else nextY++;
                    break;
                case 2: // Right
                    if (nextY + 1 < mapHeight) nextY++;
                    else nextY--;
                    break;
                case 3: // Up
                    if (nextX - 1 >= 0) nextX--;
                    else nextX++;
                    break;
                case 4: // Down
                    if (nextX + 1 < mapWidth) nextX++;
                    else nextX--;
                    break;
            }

            return (nextX, nextY, false);
        }

        public Cell Eat(int foodValue)
        {
            this.Hunger += foodValue;
            this.Health = this.Health + (foodValue / 2);
            return this.Divison(this);
        }

        private Cell Divison(Cell cell)
        {
            if (this.Hunger / 2 < 10 || this.Health / 2 < 5)
            {
                return null;
            }
            this.Hunger /= 2;
            this.Health /= 2;
            string name = cell.name + "-" + rnd.Next(1,9);
            int hunger = Math.Max(1, cell.hunger + rnd.Next(-5, 6));
            int health = Math.Max(1, cell.health + rnd.Next(-5, 6) + cell.Generation);
            int speed = Math.Max(1, cell.speed + rnd.Next(-5, 6));
            int vision = Math.Max(1, cell.vision + rnd.Next(-5, 6));
            int generation = cell.generation + 1;
            int x = cell.x + 1;
            int y = cell.y + 1;

            return new Cell(name, hunger, health, speed, vision,generation, x, y, this.rnd);
        }

        private int Scan(int[,] FOV)
        {
            int maxX = 0;
            int maxY = 0;
            for (int i = 0; i < (2*Vision+1); i++)
            {
                for (int j = 0; j < (2*Vision+1); j++)
                {
                    if (FOV[i,j] > FOV[maxX, maxY])
                    {
                        maxX = i;
                        maxY = j;
                    }
                }
            }

            int center = Vision;

            // Elmozdulások kiszámítása
            int deltaX = maxX - center; // Vízszintes elmozdulás
            int deltaY = maxY - center; // Függőleges elmozdulás

            if (deltaX == 0 && deltaY == 0)
                return -1;

            // Irány meghatározása
            if (Math.Abs(deltaX) > Math.Abs(deltaY))
            {
                // Ha a vízszintes elmozdulás nagyobb
                if (deltaX < 0) return 3; // Felfelé
                else return 4;            // Lefelé
            }
            else
            {
                // Ha a függőleges elmozdulás nagyobb vagy egyenlő
                if (deltaY < 0) return 1; // Balra
                else return 2;            // Jobbra
            }
        }
    }
}
