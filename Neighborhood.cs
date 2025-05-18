namespace CellularAutomaton;

public class Neighborhood
{
    public int Moore(State[,] grid, State counted, int x, int y)
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

    public int Linear(State[,] grid, State counted, int x, int y, int radius)
    {
        int gridSizeX = grid.GetLength(0);
        int amount = 0;

        for (int i = -radius; i <= radius; i++)
        {
            if (i == x) continue;
            int newX = (x + i + gridSizeX) % gridSizeX;

            if (newX >= 0 && newX < gridSizeX)
            {
                if (grid[y, i] == counted) amount++;
            }
        }

        return amount;
    }
}