namespace CellularAutomaton.Examples;

public class Rule184 : ElementaryAutomaton
{
    public Rule184(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionS(State.On, "111");
        AddConditionS(State.Off, "110");
        AddConditionS(State.On, "101");
        AddConditionS(State.On, "100");
        AddConditionS(State.On, "011");
        AddConditionS(State.Off, "010");
        AddConditionS(State.Off, "001");
        AddConditionS(State.Off, "000");
    }

}