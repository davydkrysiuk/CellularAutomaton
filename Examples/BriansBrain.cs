namespace CellularAutomaton.Examples;

class BriansBrain : CellularAutomaton
{
    public BriansBrain(int height, int width, int scale) : base(height, width, scale)
    {
        AddCondition(-1, State.On, State.Dying, State.On);
        AddCondition(2, State.Off, State.On, State.On);
        AddCondition(0, State.Off, State.Off, State.On);
        AddCondition(1, State.Off, State.Off, State.On);
        AddConditionRanged(3, 8, State.Off, State.Off, State.On);
        AddCondition(-1, State.Dying, State.Off, State.On);
    }
}