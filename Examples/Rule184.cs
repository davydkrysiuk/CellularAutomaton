namespace CellularAutomaton;

public class Rule184 : ElementaryAutomaton
{
    public Rule184(int width, int height, int scale = 1) : base(width, height, scale)
    {
        AddConditionString(State.On, "111");
        AddConditionString(State.Off, "110");
        AddConditionString(State.On, "101");
        AddConditionString(State.On, "100");
        AddConditionString(State.On, "011");
        AddConditionString(State.Off, "010");
        AddConditionString(State.Off, "001");
        AddConditionString(State.Off, "000");
    }

}