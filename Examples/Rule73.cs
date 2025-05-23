namespace CellularAutomaton.Examples;

public class Rule73 : ElementaryAutomaton
{
    public Rule73(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionS(State.Off, "111");
        AddConditionS(State.On, "110");
        AddConditionS(State.Off, "101");
        AddConditionS(State.Off, "100");
        AddConditionS(State.On, "011");
        AddConditionS(State.On, "000");
        AddConditionS(State.Off, "010");
        AddConditionS(State.Off, "001");
        
    }
}