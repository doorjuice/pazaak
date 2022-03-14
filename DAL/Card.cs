using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Card
    {
        public readonly int Value;

        public Card(int value)
        {
            if (0 < value && value <= 10)
                Value = value;
            else
                throw new ArgumentOutOfRangeException(nameof(value), "Card value must be between 1 and 10");
        }

        public override string ToString()
        {
            return $"+{Value}";
        }
    }
}
