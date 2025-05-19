namespace CellularAutomaton;

public class Rule90 : ElementaryAutomaton
{
    public Rule90(int width, int height, int scale = 1) : base(width, height, scale)
    {
        AddConditionString(State.On, "110");

        AddConditionString(State.On, "101");

        AddConditionString(State.On, "011");

        AddConditionString(State.On, "001");

        AddConditionString(State.Off, "111");

        AddConditionString(State.Off, "100");

        AddConditionString(State.Off, "010");

        AddConditionString(State.Off, "000");
    }
}

/*AddConditionString(State.On, "111");
AddConditionString(State.On, "110");
AddConditionString(State.On, "101");
AddConditionString(State.On, "100");
AddConditionString(State.On, "011");
AddConditionString(State.Off, "010");
AddConditionString(State.On, "001");
AddConditionString(State.Off, "000");*/  
        
/*
AddConditionString(State.On, "111");
AddConditionString(State.Off, "110");
AddConditionString(State.On, "101");
AddConditionString(State.On, "100");
AddConditionString(State.On, "011");
AddConditionString(State.On, "010");
AddConditionString(State.On, "001");
AddConditionString(State.Off, "000");      */
