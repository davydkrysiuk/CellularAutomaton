namespace CellularAutomaton;
    
public enum State
{
    Off,
    On,
    Dying
}

abstract class CellularAutomaton
{
    private int Width { get; init; }
    private int Height { get; init; }
    State[,] _grid;
    private readonly List<Tuple<int, State, State, State>> _conditions = [];
    
    protected CellularAutomaton(int height, int width)
    {
        Width = width;
        Height = height;
        _grid = new State[Height, Width];
    }
    
    protected void AddCondition(int neededAmount, State fromState, State toState, State toCount)
    {
        Tuple<int, State, State, State> tuple = new(neededAmount, fromState, toState, toCount);
        _conditions.Add(tuple);
    }

    protected void AddConditionRanged(int from, int to, State fromState, State toState, State toCount)
    {
        for (int i = from; i < to; i++)
        {
            AddCondition(i, fromState, toState, toCount);
        }
    }
    
    public void Update()
    {
        State[,] gridUpdate = new State[Height, Width];

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                State currentState = _grid[i, j];
                foreach (var condition in _conditions)
                {
                    int toCount = condition.Item1;
                    State fromState = condition.Item2;
                    State toState = condition.Item3;
                    State stateToCount = condition.Item4;
                    
                    if (currentState == fromState)
                    {
                        if (toCount == -1)
                        {
                            gridUpdate[i, j] = toState;
                        } 
                        else if (MooreNeighborhood.GetNeighbors(_grid, i, j, stateToCount) == toCount)
                        {
                            gridUpdate[i, j] = toState;
                        }
                    }
                }
            }
        }

        _grid = gridUpdate;
    }
    
    public void Randomize()
    {
        Random random = new();
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                int val = random.Next(0, 2);
                _grid[i, j] = (val == 0 ? State.On : State.Off);
            }
        }
    }
    
    
    public virtual void Display()
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                switch (_grid[i, j])
                {
                    case State.On:
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(' ');
                        break;
                    }
                    case State.Off:
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                        break;
                    }
                    case State.Dying:
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(' ');
                        break;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
