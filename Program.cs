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
        CellularAutomaton a = new BriansBrain(Console.WindowHeight -2, Console.WindowWidth);
        a.Randomize();
        Console.SetCursorPosition(0, 1);
        while (true)
        {
            a.Display();
            a.Update();
            Console.SetCursorPosition(0, 1);
        }
    }
}