namespace CellularAutomaton.Examples;

public class UnknownOne : ElementaryAutomaton
{
    public UnknownOne(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionS(State.On, "111");
        AddConditionS(State.Off, "110");
        AddConditionS(State.On, "101");
        AddConditionS(State.Off, "000");
        AddConditionS(State.On, "100");
        AddConditionS(State.Off, "011");
        AddConditionS(State.On, "010");
        AddConditionS(State.Off, "001");
    }
}