using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton; 

public abstract class CellularAutomaton : Automaton
{
    private readonly List<Tuple<int, State, State, State>> _conditions = new List<Tuple<int, State, State, State>>();
    private readonly List<Tuple<State, Rgba32>> _colors = new List<Tuple<State, Rgba32>>();

    private readonly int _scale;
    protected CellularAutomaton(int height, int width, int scale) : base(height, width, scale)
    {
        Width = width / scale;
        Height = height / scale;
        _scale = scale;
        Grid = new State[Height, Width];
    }
    
    private int Moore(State[,] grid, State counted, int x, int y)
    {
        int amount = 0;
        
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int newX = (x + i + Width) % Width;
                int newY = (y + j + Height) % Height;

                if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
                {
                    if (grid[newY, newX] == counted) amount++;
                }
            }
        }
        return amount;
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
    
    public override void Update(Boolean produceImage = false)
    {
        State[,] gridUpdate = new State[Height, Width];

        Parallel.For(0, Height, i =>
        {
            Parallel.For(0, Width, j =>
            {
                State currentState = Grid[i, j];
                foreach (var (toCount, fromState, toState, stateToCount) in _conditions)
                {
                    if (currentState == fromState)
                    {
                        if (toCount == -1)
                        {
                            gridUpdate[i, j] = toState;
                        }
                        else if (Moore(Grid, stateToCount, i, j) == toCount)
                        {
                            gridUpdate[i, j] = toState;
                        }
                    }
                }
            });
        });
        ProduceImage(gridUpdate, UpdateCount + 1 + "");
        UpdateCount++;
        Grid = gridUpdate;
    }
}
