using static System.Console;

class Player
{
  private readonly bool IsOdd;
  public string Name;
  public int[] OddNumbers;
  public int[] EvenNumbers;
  private int[]? currentNumbers;
  public int selectedNumber;

  public Player(bool isOdd, int size)
  {
    IsOdd = isOdd;
    Name = "Player " + (IsOdd ? "1" : "2");

    int total = size * size;
    OddNumbers = new int[(total + 1) / 2];
    EvenNumbers = new int[total / 2];

    int oddIndex = 0, evenIndex = 0;
    for (int i = 1; i <= total; i++)
    {
      if (i % 2 == 1 && IsOdd)
      {
        OddNumbers[oddIndex++] = i;
      }
      else if (i % 2 == 0 && !IsOdd)
      {
        EvenNumbers[evenIndex++] = i;
      }
    }
  }

  public void EnterNumber()
  {
    currentNumbers = IsOdd ? OddNumbers : EvenNumbers;

    while (true)
    {
      try
      {
        WriteLine($"({Name}) Enter your number: {string.Join(", ", currentNumbers.Where(n => n != -1))}");
        int input = int.Parse(ReadLine() ?? "");

        if (currentNumbers.Contains(input))
        {
          selectedNumber = input;
          return;
        }
        else
        {
          WriteLine("!-- WARNING --! Number not available. Choose again.");
        }
      }
      catch (FormatException e)
      {
        WriteLine($"!-- WARNING --! Your input is not a valid number. Error: {e.Message}");
      }
    }
  }

  public (int, int) EnterMove(Board board)
  {
    while (true)
    {
      try
      {
        WriteLine($"({Name}) Enter 0 to {board.Size - 1} to set row: ");
        int row = int.Parse(ReadLine() ?? "");

        WriteLine($"({Name}) Enter 0 to {board.Size - 1} to set column: ");
        int col = int.Parse(ReadLine() ?? "");

        if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
        {
          WriteLine("!-- WARNING --! Out of bounds. Try again.");
          continue;
        }

        return (row, col);
      }
      catch (FormatException e)
      {
        WriteLine($"!-- WARNING --! Invalid format. Error: {e.Message}");
      }
    }
  }

  public void UseNumber(int usedNumber)
  {
    int[] numbers = IsOdd ? OddNumbers : EvenNumbers;
    for (int i = 0; i < numbers.Length; i++)
    {
      if (numbers[i] == usedNumber)
      {
        numbers[i] = -1;
        break;
      }
    }
  }
}
