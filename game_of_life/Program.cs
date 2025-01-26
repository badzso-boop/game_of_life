using game_of_life.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_of_life
{
    internal class Program
    {
        static Random globalRandom = new Random();
        static int w = 10;
        static int h = 10;
        static void Main(string[] args)
        {
            Map map = new Map(w, h, globalRandom);
            List<(Cell cell, string cause)> deathCells = new List<(Cell cell, string cause)>();
            List<Cell> cells = new List<Cell>
            {
                new Cell("Buksi", Random(1,w-1), Random(1,h-1), globalRandom),
                new Cell("Sajt",Random(1,w-1), Random(1,h-1), globalRandom),
                new Cell("Folti",Random(1,w-1), Random(1,h-1), globalRandom),
                new Cell("Tucsi",Random(1, w - 1), Random(1, h - 1), globalRandom)
            };

            //Console.Clear();
            //Console.WriteLine("Born cells:");
            //foreach (var cell in cells)
            //{
            //    Console.WriteLine($"{cell.Name} born");
            //    Console.WriteLine($"Hunger: {cell.Hunger}, Health: {cell.Health}, Speed: {cell.Speed}, Vision: {cell.Vision}, Alive: {cell.Alive}, Generation: {cell.Generation}, X: {cell.X}, Y: {cell.Y}");
            //    Console.WriteLine();
            //}
            //Console.ReadKey();

            Dictionary<(int, int), Food> foods = new Dictionary<(int, int), Food>();
            for (int i = 0; i < 10; i++)
            {
                int x = Random(1,w-1);
                int y = Random(1,h-1);
                int value = Random(2, 10);

                foods[(x, y)] = new Food(x, y, value);
            }

            // Kezdeti elhelyezés
            foreach (var cell in cells)
            {
                map.PlaceCell(cell);
            }

            // Kezdeti elhelyezés
            foreach (var food in foods.Values)
            {
                map.PlaceFood(food);
            }

            // Mozgatás
            while (cells.Count > 0)
            {
                foreach (var cell in cells.ToList())
                {
                    if (cell.Alive)
                    {
                        var (newX, newY, dead) = cell.Move(map.GetX(), map.GetY(), map.GetRadious(cell.X, cell.Y, cell.Vision));
                        if (dead)
                        {
                            cell.Alive = false;
                            cells.Remove(cell);
                            deathCells.Add((cell, "hunger"));
                            map.Set(cell.X, cell.Y, 0);
                        }
                        else
                        {
                            map.MoveCell(cell, newX, newY, cells, deathCells);

                            if (foods.ContainsKey((newX, newY)))
                            {
                                var food = foods[(newX, newY)];
                                Cell newCell = cell.Eat(food.Value);
                                if (newCell != null)
                                {
                                    map.PlaceCell(newCell);
                                    cells.Add(newCell);
                                }
                                foods.Remove((newX, newY));
                            }
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Death cells:");
            foreach (var (cell, cause) in deathCells)
            {
                Console.WriteLine($"{cell.Name} died due to {cause}");
                Console.WriteLine($"Hunger: {cell.Hunger}, Health: {cell.Health}, Speed: {cell.Speed}, Vision: {cell.Vision}, Alive: {cell.Alive}, Generation: {cell.Generation}, X: {cell.X}, Y: {cell.Y}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        static int Random(int x, int y)
        {
            return globalRandom.Next(x, y);
        }
    }
}
