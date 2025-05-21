namespace CellularAutomaton.Examples;

public class Rule73 : ElementaryAutomaton
{
    public Rule73(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionString(State.Off, "111");
        AddConditionString(State.On, "110");
        AddConditionString(State.Off, "101");
        AddConditionString(State.Off, "100");
        AddConditionString(State.On, "011");
        AddConditionString(State.On, "000");
        AddConditionString(State.Off, "010");
        AddConditionString(State.Off, "001");
        
    }
}