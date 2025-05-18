namespace CellularAutomaton;

public class MooreNeighborhood
{
    public static int GetNeighbors(State[,] grid, int x, int y, State counted)
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
}

class Program
{
    static void Main()
    {
        CellularAutomaton a = new DayAndNight(1080 , 1920);
        a.Randomize();
        int frames = 18000;
        int i = 0;
        while (i < frames)
        {
            a.Update(true);
            Console.WriteLine(i + "/" + frames + " updates generated. " + ((double)i / (double)frames) * 100 + "%");
            i++;
        }
    }
}