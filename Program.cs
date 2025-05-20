using SixLabors.ImageSharp.PixelFormats;

namespace CellularAutomaton;

class Program
{
    static void Main()
    {
        CellularAutomaton a = new BriansBrain(1080  , 1920 , 4);
        a.Randomize();
        a.ColourPair(State.On, new Rgba32(0, 153, 51));
        a.ColourPair(State.Off, new Rgba32(0, 0, 0));
        a.ColourPair(State.Dying, new Rgba32(255, 255, 255));
        int frames = 500;
        int i = 0;
        while (i < frames)
        {
            a.Update(true);
            Console.Write("" + i + 1 + "/" + frames + "\n");
            i++;
        }
        
        /*
        ElementaryAutomaton b = new DayAndNight(1080, 1920, 8);
        b.Randomize();
        b.ColourPair(State.On, new Rgba32(0, 153, 51));
        b.ColourPair(State.Off, new Rgba32(0, 0, 0));
        while (true)
        {
            Console.Write("Workin\n");
            try
            {
                b.Update(true);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Finished.");
                break;
            }

        }*/
    }
}