using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Card
    {
        protected int fixedValue, toggleValue;
        public int Value { get { return fixedValue + toggleValue; } }

        protected bool toggleable = false;
        public bool Toggleable { 
            get { return toggleable && toggleValue != 0; }
            set { toggleable = value; } 
        }

        public Card(int fixedValue, int toggleValue)
        {
            this.fixedValue = fixedValue;
            this.toggleValue = toggleValue;
        }

        public void Toggle()
        {
            toggleValue = -toggleValue;
        }

        public override string ToString()
        {
            return Value > 0 ? $"+{Value}" : $"{Value}";
        }
    }

    public class BlankCard : Card
    {
        public BlankCard() : base(0, 0) { }

        public override string ToString()
        {
            return "";
        }
    }

    public class StandardCard : Card
    {
        public StandardCard(int fixedValue) : base(fixedValue, 0) { }
    }

    public class ToggleCard : Card
    {
        public ToggleCard(int toggleValue) : base(0, toggleValue)
        { 
            toggleable = true;
        }

        public override string ToString()
        {
            return Toggleable ? $"±{Math.Abs(toggleValue)}" : base.ToString();
        }
    }
}
