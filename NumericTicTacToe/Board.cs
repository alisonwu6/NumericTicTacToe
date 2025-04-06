using static System.Console;

public class Board
{
  public int Size { get; }
  public int?[,] Grid { get; set; }
  public int WinningScore { get; }

  public Board(int size)
  {
    Size = size;
    Grid = new int?[size, size];
    WinningScore = Size * (Size * Size + 1) / 2;
  }

  public void Display(string name)
  {
    WriteLine("------------------------------------------");
    WriteLine($"Your Numeric Tic-Tac-Toe Board ({name})");
    WriteLine($"Board Size: {Size} x {Size}, Winning Score: {WinningScore}");
    for (int i = 0; i < Size; i++)
    {
      for (int j = 0; j < Size; j++)
      {
        if (Grid[i, j].HasValue)
        {
          Write($"{Grid[i, j].Value:D2}  ");
        }
        else
        {
          Write("--  ");
        }
      }
      WriteLine();
    }
  }

  public bool IsValidMove(int row, int col)
  {
    if (row < 0 || row >= Size || col < 0 || col >= Size)
    {
      WriteLine($"!-- WARNING --! Your move is outside of the board. Valid range: 0 to {Size - 1}.");
      return false;
    }

    if (Grid[row, col].HasValue)
    {
      WriteLine($"!-- WARNING --! Your move [{row}, {col}] is already taken.");
      return false;
    }

    return true;
  }

  public bool PlaceNumber(int row, int col, int number)
  {
    if (!IsValidMove(row, col))
    {
      return false;
    }
    Grid[row, col] = number;
    return true;
  }

  public bool CheckWin()
  {
    for (int i = 0; i < Size; i++)
    {
      int rowSum = 0, colSum = 0;
      int rowCount = 0, colCount = 0;

      for (int j = 0; j < Size; j++)
      {
        if (Grid[i, j].HasValue)
        {
          rowSum += Grid[i, j].Value;
          rowCount++;
        }
        if (Grid[j, i].HasValue)
        {
          colSum += Grid[j, i].Value;
          colCount++;
        }
      }

      if ((rowCount == Size && rowSum == WinningScore) ||
          (colCount == Size && colSum == WinningScore))
      {
        return true;
      }
    }

    int d1 = 0, d2 = 0;
    int d1Count = 0, d2Count = 0;

    for (int i = 0; i < Size; i++)
    {
      if (Grid[i, i].HasValue)
      {
        d1 += Grid[i, i].Value;
        d1Count++;
      }

      if (Grid[i, Size - 1 - i].HasValue)
      {
        d2 += Grid[i, Size - 1 - i].Value;
        d2Count++;
      }
    }

    if ((d1Count == Size && d1 == WinningScore) ||
        (d2Count == Size && d2 == WinningScore))
    {
      return true;
    }

    return false;
  }

  public void ShowResult(Player currentPlayer)
  {
    Display("Final Result");
    WriteLine("==============");
    WriteLine("Game over");
    WriteLine($"{currentPlayer.Name} wins.");
    WriteLine("==============");
  }

  public void LoadGrid(List<List<int?>> sourceGrid)
  {
    for (int i = 0; i < Size; i++)
    {
      for (int j = 0; j < Size; j++)
      {
        Grid[i, j] = sourceGrid[i][j];
      }
    }
  }
}
