namespace ModuleTracking
{
    public class ModuleInList : ModuleBase
    {
        List<float> percentsOfOperable = new();

        public ModuleInList(string name, string symbol) : base(name, symbol)
        {
        }

        public override event PercentOfOperatableBelow90Delegate? PercentOfOperatableBelow90;

        public override void AddPercent(float percent)
        {
            if (percent >= 0 && percent <= 100)
            {
                percentsOfOperable.Add(percent);

                if (PercentOfOperatableBelow90 != null && percent <= 90)
                {
                    PercentOfOperatableBelow90(this, new EventArgs());
                }
            }
            else
            {
                throw new Exception("Procent poza zakresem.");
            }
        }


        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();

            foreach(var percent in  percentsOfOperable)
                statistics.AddPercent(percent); 

            return statistics;  
        }

        public override void RemoveAllPercents()
        {
            percentsOfOperable.Clear();
        }
    }
}
