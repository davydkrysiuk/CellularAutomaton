using CellularAutomaton.Examples;
using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton;

internal static class Program
{
    static void Main()
    {
        ElementaryAutomaton a = new Thingy(1080  , 1920 , 6);
        a.RandomizeLine(0);
        a.ColourPair(State.On, new Rgba32(0, 0, 255));
        a.ColourPair(State.Off, new Rgba32(255, 255, 0));
        a.ColourPair(State.Dying, new Rgba32(250, 246, 233));
        a.StopWhenDone = true;
        int frames = 1920;
        int i = 0;
        while (i < frames)
        {
            try
            {
                a.Update(true);
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