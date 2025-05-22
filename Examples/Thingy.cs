namespace CellularAutomaton.Examples;

public class Thingy : ElementaryAutomaton
{
    public Thingy(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionString(State.Off, "110");
        AddConditionString(State.On, "011");
        AddConditionString(State.On, "010");
        AddConditionString(State.On, "100");
        AddConditionString(State.Off, "001");
        
        AddConditionString(State.On, "101");
        AddConditionString(State.Off, "000");
    }

}