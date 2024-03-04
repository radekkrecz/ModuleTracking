namespace ModuleTracking
{
    public class ModuleInFile : ModuleBase
    {
        const string endOfFileName = "module.txt";

        readonly string fileName;

        public override event PercentOfOperatableBelow90Delegate? PercentOfOperatableBelow90;

        public ModuleInFile(string name, string symbol) : base(name, symbol)
        {
            fileName = $"{Name}_{Symbol}_{endOfFileName}";
        }

        public override void AddPercent(float percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                using var writer = File.AppendText(fileName);
                writer.WriteLine(percent);

                if (PercentOfOperatableBelow90 != null && percent <= 90)
                {
                    PercentOfOperatableBelow90(this, new EventArgs());
                }
            }
            else
                throw new Exception("Procent poza zakresem.");
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();

            if(File.Exists(fileName))
            {
                int count = 0;  

                using( var writer = File.OpenText(fileName))
                {
                    var line  = writer.ReadLine();
                    
                    while(line != null)
                    {
                        var number = float.Parse(line);
                        statistics.AddPercent(number);  
                        count++;
                        line = writer.ReadLine();
                    }
                }
            }

            return statistics;
        }

        public override void RemoveAllPercents()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}
