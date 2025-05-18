namespace CellularAutomaton;

class Program
{
    static void Main()
    {
        CellularAutomaton a = new DayAndNight(1080  , 1920 , 10);
        a.Randomize();
        int frames = 900;
        int i = 0;
        while (i < frames)
        {
            a.Update(true);
            Console.Write("" + i + "/" + frames + "\n");
            i++;
        }
    }
}