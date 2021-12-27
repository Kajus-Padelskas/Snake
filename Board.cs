using System;

public class Board
{
    private Board();

    public Board(int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Map[i][j] = 0;
            }
        }
    }

    public List<List<Int16>> Map { get; set; }

    public void displayMap()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                System.Console.WriteLine(Map[i][j]);
            }
        }
    }
}
