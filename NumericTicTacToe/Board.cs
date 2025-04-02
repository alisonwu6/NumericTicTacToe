using static System.Console;
class Board
{
  public int Size;
  public int?[,] Grid;
  public int WinningScore;

  public Board(int size)
  {
    Size = size;
    Grid = new int?[size, size];
    WinningScore = Size * (Size * Size + 1) / 2;
  }

  public void Display()
  {
    WriteLine("------------------------------");
    WriteLine("Your Numeric Tic-Tac-Toe Board");
    for (int i = 0; i < Size; i++)
    {
      for (int j = 0; j < Size; j++)
      {
        if (Grid[i, j] != null)
        {
          Write(Grid[i, j].Value.ToString("D2") + "  ");
        }
        else
        {
          Write("--  ");
        }
      }
      WriteLine();
    }
  }

  public bool IsMoveValid(int row, int col, int number)
  {
    if (row < 0 || row >= Size || col < 0 || col >= Size)
    {
      WriteLine($"WARNING: Your move is not allowed due to the range is out of {Size}.");
      WriteLine("Please re-enter");
      return false;
    }

    if (Grid[row, col] != null)
    {
      WriteLine($"WARNING: Your move [{row}, {col}] is already taken. Please make another move.");
      WriteLine("Please re-enter");
      return false;
    }

    Grid[row, col] = number;
    return true;
  }

  public bool CheckWin()
  {
    // a line (horizontal, vertical, or diagonal) with a sum of 15

    // horizontal, vertical check
    for (int i = 0; i < Size; i++)
    {
      int horizontalSumResult = 0;
      int verticalSumResult = 0;
      for (int j = 0; j < Size; j++)
      {
        if (Grid[i, j].HasValue)
        {
          horizontalSumResult += Grid[i, j].Value;
        }

        if (Grid[j, i].HasValue)
        {
          verticalSumResult += Grid[j, i].Value;
        }
        // WriteLine($"[{i}, {j}]: {horizontalSumResult}");
        // WriteLine($"[{j}, {i}]: {verticalSumResult}");
      }

      if (horizontalSumResult == WinningScore || verticalSumResult == WinningScore)
      {
        // WriteLine("Win!!");
        return true;
      }
    }

    // diagonal check
    int d1 = 0;
    int d2 = 0;

    for (int i = 0; i < Size; i++)
    {
      if (Grid[i, i].HasValue)
      {
        d1 += Grid[i, i].Value;
      }

      if (Grid[i, Size - 1 - i].HasValue)
      {
        d2 += Grid[i, Size - 1 - i].Value;
      }
    }

    if (d1 == WinningScore || d2 == WinningScore)
    {
      WriteLine($"d1 - {d1 == WinningScore}, d2 - {d2 == WinningScore}");
      return true;
    }

    return false;
  }

  public void CheckEnd()
  {

  }
}