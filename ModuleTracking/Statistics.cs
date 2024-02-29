namespace ModuleTracking
{
    public class Statistics
    {
        public float Sum {  get; private set; }
        public int Count {  get; private set; }    
        public float Min {  get; private set; }
        public float Max { get; private set; }

        public float Average 
        {
            get 
            {
                return Count != 0 ? Sum / Count : 0;
            }
        }

        public Statistics() 
        {
            Sum = 0;
            Count = 0;
            Min = float.MaxValue;
            Max = float.MinValue;   
        }

        public void AddPercent(float percent)
        {
            Sum += percent;
            Count++;
            Min = Math.Min(Min, percent);
            Max = Math.Max(Max, percent);
        }
    }
}
