﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace CellularAutomaton; 

public enum State
{
    Off,
    On,
    Dying,
}

public abstract class Automaton(int width, int height, int scale = 1)
{
    protected int Width { get; init; } = width; 
    protected int Height { get; init; } = height;
    protected State[,] Grid = new State[height, width];
    private readonly List<Tuple<State, Rgba32>> _colors = [];
    protected int UpdateCount = 0;

    public void ColourPair(State state, Rgba32 colour)
    {
        Tuple<State, Rgba32> tuple = new(state, colour);
        _colors.Add(tuple);
    }
    
    public void RandomizeLine(int line)
    {
        Random random = new();
        for (var i = 0; i < Width; i++)
        {
            var val = random.Next(0, 2);
            Grid[line, i] = (val == 0 ? State.On : State.Off);
        }
    }

    public void FillLine(int line, State state)
    {
        for (int i = 0; i < Width; i++)
        {
            Grid[line, i] = state;
        }
    }

    public void FillAll(State state)
    {
        for (var i = 0; i < Height; i++)
        {
            FillLine(i, state);
        }
    }
    public void RandomizeAll()
    {
        for (int i = 0; i < Height; i++)
        {
            RandomizeLine(i);
        }
    }
    
    protected void ProduceImage(State[,] grid, string filename)
    {
        var image = new Image<Rgba32>(Width * scale,Height * scale);
        Parallel.For(0, Width, j => 
        {
            for (int k = 0; k < Height; k++)
            {
                var pixel = new Rgba32(0,0,0);
                foreach (var colour in _colors)
                {
                    if (colour.Item1 == grid[k, j])
                    {
                        pixel = colour.Item2;
                    }
                }
                
                for (var x = 0; x < scale; x++)
                {
                    for (var y = 0; y < scale; y++)
                    {
                        image[(j * scale) + x, (k * scale) + y] = pixel;
                    }
                }
            }
        });
        
        var bloomImage = image.Clone((ctx => ctx.GaussianBlur(5)));
        image.Mutate((ctx => ctx.DrawImage(bloomImage, PixelColorBlendingMode.Screen, 1f)));

        var filePath = "./images/" + filename + ".jpeg";
        image.Save(filePath);
        
    }

    public abstract void Update(Boolean produceImage = false);
}