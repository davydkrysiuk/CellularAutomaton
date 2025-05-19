namespace CellularAutomaton;

class Program
{
    static void Main()
    {
        /*CellularAutomaton a = new DayAndNight(1080  , 1920 , 10);
        a.Randomize();
        int frames = 12;
        int i = 0;
        while (i < frames)
        {
            a.Update(true);
            Console.Write("" + i + 1 + "/" + frames + "\n");
            i++;
        }*/
        ElementaryAutomaton b = new Rule30(1080, 1920, 2);
        b.MiddleStart(State.On, 1);
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

        }
    }
}