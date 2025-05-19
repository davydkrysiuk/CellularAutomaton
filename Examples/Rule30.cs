namespace CellularAutomaton;

public class Rule30 : ElementaryAutomaton
{
    public Rule30(int width, int height, int scale = 1) : base(width, height, scale)
    {
        AddConditionString(State.Off, "111");
        AddConditionString(State.Off, "110");
        AddConditionString(State.Off, "101");
        AddConditionString(State.Off, "000");
        
        AddConditionString(State.On, "100");
        AddConditionString(State.On, "011");
        AddConditionString(State.On, "010");
        AddConditionString(State.On, "001");
        
    }
}