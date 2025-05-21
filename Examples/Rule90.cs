namespace CellularAutomaton.Examples;

public class Rule90 : ElementaryAutomaton
{
    public Rule90(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionString(State.On, "110");
        AddConditionString(State.On, "101");
        AddConditionString(State.On, "011");
        AddConditionString(State.On, "001");
        AddConditionString(State.Off, "111");
        AddConditionString(State.Off, "100");
        AddConditionString(State.Off, "010");
        AddConditionString(State.Off, "000");
    }
}