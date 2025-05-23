namespace CellularAutomaton.Examples;

public class Rule30 : ElementaryAutomaton
{
    public Rule30(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionS(State.Off, "111");
        AddConditionS(State.Off, "110");
        AddConditionS(State.Off, "101");
        AddConditionS(State.Off, "000");
        AddConditionS(State.On, "100");
        AddConditionS(State.On, "011");
        AddConditionS(State.On, "010");
        AddConditionS(State.On, "001");
        
    }
}