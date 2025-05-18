using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton;
using SixLabors.ImageSharp;

public enum State
{
    Off,
    On,
    Dying
}

public abstract class CellularAutomaton
{
    private int Width { get; init; }
    private int Height { get; init; }
    State[,] _grid;
    private readonly List<Tuple<int, State, State, State>> _conditions = new List<Tuple<int, State, State, State>>();
    private int _updateCount = 0;
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
    
    public void Update(Boolean produceImage = false)
    {
        State[,] gridUpdate = new State[Height, Width];

        Parallel.For(0, Height, i =>
        {
            Parallel.For(0, Width, j =>
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
            });
        });
        ProduceImage(gridUpdate, _updateCount + "");
        _updateCount++;
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

    private void ProduceImage(State[,] grid, string filename)
    {
        using var image = new Image<Rgba32>(Width, Height);
        Parallel.For(0, Width, j => 
        {
            for (int k = 0; k < Height; k++)
            {
                switch (grid[k, j])
                {
                    case State.Off: image[j, k] = new Rgba32(222,31,38); break;
                    case State.Dying: image[j, k] = new Rgba32(255, 0, 255); break;
                    case State.On: image[j, k] = new Rgba32	(112,189,84); break;
                }
            }
        });

        string filePath = "./images/" + filename + ".jpeg";
        image.Save(filePath);
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
                        Console.BackgroundColor = ConsoleColor.Red;
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
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(' ');
                        break;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
