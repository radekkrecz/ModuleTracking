using System.Globalization;

namespace ModuleTracking
{
    public abstract class ModuleBase : IModule
    {
        public string Name { get; private set; }
        public string Symbol { get; private set;}

        public delegate void PercentOfOperatableBelow90Delegate(object sender, EventArgs e);

        public abstract event PercentOfOperatableBelow90Delegate? PercentOfOperatableBelow90;

        public ModuleBase(string name, string symbol)
        {
            if (name == null || symbol == null || name.Length == 0 || symbol.Length == 0)
                throw new ArgumentException("Brak nazwy/symbolu modułu!");

            Name = name;
            Symbol = symbol;    
        }

        public abstract void AddPercent(float percent);

        public void AddPercent(double percent)
        {
            AddPercent((float)percent);
        }

        public void AddPercent(int percent)
        {
            AddPercent((float)percent);
        }

        public void AddPercent(string percent)
        {
            if (float.TryParse(percent, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float value))
            {
                AddPercent(value);
            }
            else
            {
                throw new Exception("To nie jest wartość liczbowa!");
            }
        }

        public abstract void RemoveAllPercents();  

        public abstract Statistics GetStatistics();
    }
}
