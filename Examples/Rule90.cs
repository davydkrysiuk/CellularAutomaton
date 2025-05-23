namespace CellularAutomaton.Examples;

public class Rule90 : ElementaryAutomaton
{
    public Rule90(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionS(State.On, "110");
        AddConditionS(State.On, "101");
        AddConditionS(State.On, "011");
        AddConditionS(State.On, "001");
        AddConditionS(State.Off, "111");
        AddConditionS(State.Off, "100");
        AddConditionS(State.Off, "010");
        AddConditionS(State.Off, "000");
    }
}