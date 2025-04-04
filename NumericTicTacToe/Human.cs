using static System.Console;

class Human : Player
{
  public Human(bool isOdd, int size) : base(isOdd, size) { }

  public override void EnterNumber()
  {
    var currentNumbers = IsOdd ? OddNumbers : EvenNumbers;

    while (true)
    {
      try
      {
        WriteLine($"({Name}) Enter your number: {string.Join(", ", currentNumbers)}");
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

  public override (int, int) EnterMove(Board board)
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
          WriteLine($"!-- WARNING --! Your move [{row}, {col}] is outside of the board.");
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
}
