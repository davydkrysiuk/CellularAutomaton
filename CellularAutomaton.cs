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
    private int _scale = 0;
    protected CellularAutomaton(int height, int width, int scale = 1, State[,] input = null, Boolean hasInput = false)
    {
        Width = width / scale;
        Height = height / scale;
        _scale = scale;
        _grid = hasInput ? input : new State[Height, Width];
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
    
    public State[,] Update(Boolean produceImage = false)
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

                    Neighborhood neighborhood = new Neighborhood();
                    if (currentState == fromState)
                    {
                        if (toCount == -1)
                        {
                            gridUpdate[i, j] = toState;
                        }
                        else if (neighborhood.Moore(_grid, stateToCount, i, j) == toCount)
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
        return _grid;
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
        using var image = new Image<Rgba32>(Width * _scale,Height * _scale);
        Parallel.For(0, Width, j => 
        {
            Random random = new();
            for (int k = 0; k < Height; k++)
            {
                Rgba32 pixel = new Rgba32(0,0,0);
                switch (grid[k, j])
                {
                    case State.Off:
                    {
                        pixel =  new Rgba32(0, 0,0);
                        break;
                    }
                    case State.Dying:
                    {
                        pixel = new Rgba32(0,0,0);
                        break;
                    }
                    case State.On:
                    {
                        pixel = random.Next(0, 2) == 0 ? new Rgba32(204, 102, 255) : new Rgba32(204, 0, 255);
                        break;
                    }
                }
                
                for (int x = 0; x < _scale; x++)
                {
                    for (int y = 0; y < _scale; y++)
                    {
                        image[(j * _scale) + x, (k * _scale) + y] = pixel;
                    }
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
