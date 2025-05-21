namespace CellularAutomaton.Examples;

public class Rule241 : ElementaryAutomaton
{
    public Rule241(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionString(State.On, "111");
        AddConditionString(State.On, "110");
        AddConditionString(State.On, "101");
        AddConditionString(State.On, "100");
        AddConditionString(State.On, "000");
        
        AddConditionString(State.Off, "011");
        AddConditionString(State.Off, "010");
        AddConditionString(State.Off, "001");
    }

}