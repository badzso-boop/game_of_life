using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace game_of_life.classes
{
    public class Food
    {
        private int x;
        private int y;
        private int value;

        public Food(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public int Value
        {
            get => value;
            set
            {
                if (value > 1 && value < 11)
                {
                    this.value = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 2 and 10.");
                }
            }
        }
    }

}
