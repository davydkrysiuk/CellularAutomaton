using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton;
using SixLabors.ImageSharp;

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
    private readonly List<Tuple<int, State, State, State>> _conditions = new List<Tuple<int, State, State, State>>();
    private readonly List<State[,]> _updates = new List<State[,]>();
    
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

        Parallel.For(0, Height, i =>
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
        });
        
        _grid = gridUpdate;
        _updates.Add(_grid);
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

    public virtual void ProduceImages()
    {
        Parallel.For(0, _updates.Count, i =>
        {
            using var image = new Image<Rgba32>(Width, Height);

            for (int j = 0; j < Width; j++)
            {
                for (int k = 0; k < Height; k++)
                {
                    switch (_updates[i][k, j])
                    {
                        case State.Off: image[j, k] = new Rgba32(0, 0, 0, 255); break;
                        case State.Dying: image[j, k] = new Rgba32(255, 255, 255, 255); break;
                        case State.On: image[j, k] = new Rgba32(57, 255, 20, 255); break;
                    }
                }
            }

            string filePath = "./images/" + i + ".jpeg";
            image.Save(filePath);
            Console.WriteLine(i + "/" + _updates.Count  + " images generated.");

        });
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
