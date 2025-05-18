namespace CellularAutomaton;

class DayAndNight : CellularAutomaton
{
    public DayAndNight(int height, int width, int scale = 1, State[,] input = null, Boolean hasInput = false) : base(height, width, scale, input, hasInput)
    {
        AddCondition(3, State.Off, State.On, State.On);
        AddCondition(6, State.Off, State.On, State.On);
        AddCondition(7, State.Off, State.On, State.On);
        AddCondition(8, State.Off, State.On, State.On);
        
        AddCondition(3, State.On, State.On, State.On);
        AddCondition(4, State.On, State.On, State.On);
        AddCondition(6, State.On, State.On, State.On);
        AddCondition(7, State.On, State.On, State.On);
        AddCondition(8, State.On, State.On, State.On);
    }
}