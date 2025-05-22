namespace CellularAutomaton.Examples;

public class UnknownOne : ElementaryAutomaton
{
    public UnknownOne(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionString(State.On, "111");
        AddConditionString(State.Off, "110");
        AddConditionString(State.On, "101");
        AddConditionString(State.Off, "000");
        AddConditionString(State.On, "100");
        AddConditionString(State.Off, "011");
        AddConditionString(State.On, "010");
        AddConditionString(State.Off, "001");
    }
}