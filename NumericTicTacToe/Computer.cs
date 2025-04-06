using static System.Console;

class Computer : Player
{
  private Random randomNum = new Random();

  public Computer(bool isOdd, int size) : base(isOdd, size) { }

  public override void EnterNumber()
  {
    var current = EvenNumbers;
    
    List<int> available = new List<int>();
    foreach (int n in current)
    {
      if (n != -1)
      {
        available.Add(n);
      }
    }

    int index = randomNum.Next(available.Count);
    selectedNumber = available[index];

    WriteLine($"(Computer) selected number: {selectedNumber}");
  }

  public override (int, int) EnterMove(Board board)
  {
    while (true)
    {
      int row = randomNum.Next(board.Size);
      int col = randomNum.Next(board.Size);

      if (board.IsValidMove(row, col))
      {
        WriteLine($"(Computer) places at ({row}, {col})");
        return (row, col);
      }
    }
  }
}
