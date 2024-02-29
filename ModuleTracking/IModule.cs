using static ModuleTracking.ModuleBase;

namespace ModuleTracking
{
    public interface IModule
    {
        string Name { get; }
        
        string Symbol { get; }
        
        event PercentOfOperatableBelow90Delegate PercentOfOperatableBelow90;

        void AddPercent(float percent);

        void AddPercent(double percent);

        void AddPercent(int percent);

        void AddPercent(string percent);

        void RemoveAllPercents();

        Statistics GetStatistics();
    }
}
