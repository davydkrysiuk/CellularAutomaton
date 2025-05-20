namespace CellularAutomaton;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class ElementaryAutomaton
{
    private int Width { get; init; }
    private int Height { get; init; }
    State[,] _grid;
    private readonly List<Tuple<State, State, State, State>> _conditions = new List<Tuple<State, State, State, State>>();
    private readonly List<Tuple<State, Rgba32>> _colors = new List<Tuple<State, Rgba32>>();
    
    private int _updateCount;
    private int _currentLine = 1;
    private readonly int _scale;
    protected ElementaryAutomaton(int height, int width, int scale = 1)
    {
        Width = width / scale;
        Height = height / scale;
        _scale = scale;
        _grid = new State[Height, Width];
    }
    
    protected void AddCondition(State newState, State stateOne, State stateTwo, State stateThree)
    {
        Tuple<State, State, State, State> tuple = new(newState, stateOne, stateTwo, stateThree);
        _conditions.Add(tuple);
    }

    protected void FillWith(State tofill)
    {
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                _grid[i, j] = tofill;
            }
        }
    }

    public void ColourPair(State state, Rgba32 colour)
    {
        Tuple<State, Rgba32> tuple = new(state, colour);
        _colors.Add(tuple);
    }
    protected void AddConditionString(State newState, string pattern)
    {
        if (pattern.Length != 3) return;
        var numbers = pattern.ToCharArray();
        
        AddCondition(newState, (State)(numbers[0] - '0'), (State)(numbers[1] - '0'), (State)(numbers[2] - '0'));
    }
    
    public void Randomize()
    {
        Random random = new();
        for (int i = 0; i < Width; i++)
        {
            int val = random.Next(0, 2);
            _grid[0, i] = (val == 0 ? State.On : State.Off);
        }
    }

    public void MiddleStart(State state, int width)
    {
        int middle = Width / 2;

        for (int i = middle - width; i < middle + width; i++)
        {
            _grid[0, i] = state;
        }
    }
    
    public void Update(Boolean produceImage = false)
    {
        State[,] gridUpdate = _grid;

        for (int j = 0; j < Width; j++)
        {
            foreach (var condition in _conditions)
            {
                State newState = condition.Item1;
                State oneBefore = _grid[_currentLine - 1, (j - 1 + Width) % Width];
                State current = _grid[_currentLine - 1, j];
                State oneAfter = _grid[_currentLine - 1, (j + 1) % Width];

                if (oneBefore == condition.Item2 && current == condition.Item3 && oneAfter == condition.Item4)
                {
                    gridUpdate[_currentLine, j] = newState;
                }
            }
        }

        _currentLine++;
        ProduceImage(gridUpdate, _updateCount + 1 + "");
        _updateCount++;
        _grid = gridUpdate;
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
                /*switch (grid[k, j])
                {
                    case State.Off:
                    {
                        pixel =  new Rgba32(82, 5, 123);
                        break;
                    }
                    case State.Dying:
                    {
                        pixel = new Rgba32(0,0,0);
                        break;
                    }
                    case State.On:
                    {
                        pixel = new Rgba32(240, 165, 0);
                        break;
                    }
                }*/
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