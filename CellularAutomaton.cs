using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton;
using SixLabors.ImageSharp;

public enum State
{
    Off,
    On,
    Dying,
    Colour,
    Uncolour
}

public abstract class CellularAutomaton
{
    private int Width { get; init; }
    private int Height { get; init; }
    State[,] _grid;
    private readonly List<Tuple<int, State, State, State>> _conditions = new List<Tuple<int, State, State, State>>();
    private readonly List<Tuple<State, Rgba32>> _colors = new List<Tuple<State, Rgba32>>();

    private int _updateCount;
    private readonly int _scale;
    protected CellularAutomaton(int height, int width, int scale = 1)
    {
        Width = width / scale;
        Height = height / scale;
        _scale = scale;
        _grid = new State[Height, Width];
    }
    
    private int Moore(State[,] grid, State counted, int x, int y)
    {
        int gridSizeX = grid.GetLength(0);
        int gridSizeY = grid.GetLength(1);
        int amount = 0;
        
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int newX = (x + i + gridSizeX) % gridSizeX;
                int newY = (y + j + gridSizeY) % gridSizeY;

                if (newX >= 0 && newX < gridSizeX && newY >= 0 && newY < gridSizeY)
                {
                    if (grid[newX, newY] == counted) amount++;
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

    public void ColourPair(State state, Rgba32 colour)
    {
        Tuple<State, Rgba32> tuple = new(state, colour);
        _colors.Add(tuple);
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

                    if (currentState == fromState)
                    {
                        if (toCount == -1)
                        {
                            gridUpdate[i, j] = toState;
                        }
                        else if (Moore(_grid, stateToCount, i, j) == toCount)
                        {
                            gridUpdate[i, j] = toState;
                        }
                    }
                }
            });
        });
        ProduceImage(gridUpdate, _updateCount + 1 + "");
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
                foreach (var colour in _colors)
                {
                    if (colour.Item1 == grid[k, j])
                    {
                        pixel = colour.Item2;
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
}
