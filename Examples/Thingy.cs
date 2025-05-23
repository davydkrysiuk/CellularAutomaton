namespace CellularAutomaton.Examples;

public class Thingy : ElementaryAutomaton
{
    public Thingy(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionS(State.Off, "110");
        AddConditionS(State.On, "011");
        AddConditionS(State.On, "010");
        AddConditionS(State.On, "100");
        AddConditionS(State.Off, "001");
        
        AddConditionS(State.On, "101");
        AddConditionS(State.Off, "000");
    }

}