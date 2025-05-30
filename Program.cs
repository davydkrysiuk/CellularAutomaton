using CellularAutomaton.Examples;
using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton;

internal static class Program
{
    private static void Main()
    {
        Automaton a = new Rule73(1080  , 1920 , 10);
        a.ColourPair(State.Off, new Rgba32(0, 0, 0));
        a.ColourPair(State.On, new Rgba32(100,200,100));
        a.RandomizeAll();
        const int frames = 600;
        var i = 0;
        while (i < frames)
        {
            try
            {
                a.Update(i == frames - 1);
                Console.Write("" + (i + 1) + "/" + frames + "\n");
                i++;
            }
            catch
            {
                Console.Write("Finished.");
                break;
            }
        }
    }
}