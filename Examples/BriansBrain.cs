namespace CellularAutomaton;

class BriansBrain : CellularAutomaton
{
    public BriansBrain(int height, int width, int scale = 1, State[,] input = null, Boolean hasInput = false) : base(height, width, scale, input, hasInput)
    {
        AddCondition(-1, State.On, State.Dying, State.On);
        AddCondition(2, State.Off, State.On, State.On);
    
        AddCondition(0, State.Off, State.Off, State.On);
        AddCondition(1, State.Off, State.Off, State.On);
        AddConditionRanged(3, 8, State.Off, State.Off, State.On);
        
        AddCondition(-1, State.Dying, State.Off, State.On);
    }
}