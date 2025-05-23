namespace CellularAutomaton.Examples;

public class Rule241 : ElementaryAutomaton
{
    public Rule241(int width, int height, int scale) : base(width, height, scale)
    {
        AddConditionS(State.On, "111");
        AddConditionS(State.On, "110");
        AddConditionS(State.On, "101");
        AddConditionS(State.On, "100");
        AddConditionS(State.On, "000");
        
        AddConditionS(State.Off, "011");
        AddConditionS(State.Off, "010");
        AddConditionS(State.Off, "001");
    }

}