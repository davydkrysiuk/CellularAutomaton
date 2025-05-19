namespace CellularAutomaton;

class GameOfLife : CellularAutomaton
{
    public GameOfLife(int height, int width, int scale = 1) : base(height, width, scale)
    {
        AddCondition(0, State.On, State.Off, State.On);
        AddCondition(1, State.On, State.Off, State.On);
        
        AddCondition(2, State.On, State.On, State.On);
        AddCondition(3, State.On, State.On, State.On);
        
        AddConditionRanged(4, 8, State.On, State.Off, State.On);
        
        AddCondition(3, State.Off, State.On, State.On);
    }
}