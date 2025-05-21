using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton; 

public class ElementaryAutomaton : Automaton
{
    private readonly List<Tuple<State, State, State, State>> _conditions = new List<Tuple<State, State, State, State>>();
    private readonly List<Tuple<State, Rgba32>> _colors = new List<Tuple<State, Rgba32>>();
    public Boolean StopWhenDone { get; set; } = true;
    private int _currentLine = 1;
    private readonly int _scale;
    protected ElementaryAutomaton(int height, int width, int scale) : base(height, width, scale)
    {
        Width = width / scale;
        Height = height / scale;
        _scale = scale;
        Grid = new State[Height, Width];
    }
    
    protected void AddCondition(State newState, State stateOne, State stateTwo, State stateThree)
    {
        Tuple<State, State, State, State> tuple = new(newState, stateOne, stateTwo, stateThree);
        _conditions.Add(tuple);
    }
    
    protected void AddConditionString(State newState, string pattern)
    {
        if (pattern.Length != 3) return;
        var numbers = pattern.ToCharArray();
        
        AddCondition(newState, (State)(numbers[0] - '0'), (State)(numbers[1] - '0'), (State)(numbers[2] - '0'));
    }
    
    public override void Update(Boolean produceImage = false)
    {
        State[,] gridUpdate = Grid;

        if (StopWhenDone && _currentLine + 1> Height)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        for (int j = 0; j < Width; j++)
        {
            foreach (var condition in _conditions)
            {
                State newState = condition.Item1;
                
                State oneBefore = Grid[(_currentLine - 1 + Height) % Height, (j - 1 + Width) % Width];
                State current = Grid[(_currentLine - 1 + Height) % Height , (j + Width) % Width];
                State oneAfter = Grid[(_currentLine - 1 + Height) % Height, (j + 1) % Width];

                if (oneBefore == condition.Item2 && current == condition.Item3 && oneAfter == condition.Item4)
                {
                    gridUpdate[(_currentLine + Height) % Height, (j + Width) % Width] = newState;
                }
            }
        }

        _currentLine++;
        ProduceImage(gridUpdate, UpdateCount + 1 + "");
        UpdateCount++;
        Grid = gridUpdate;
    }
}