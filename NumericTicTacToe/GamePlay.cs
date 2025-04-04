using static System.Console;
class GamePlay
{
  private Board? board;
  private Human? player1;
  private Player? player2;
  private Player? currentPlayer;
  private readonly int size = 3;
  private readonly int mode = 1;

  public static void Guild()
  {
    WriteLine("Welcome To Numeric Tic-Tac-Toe Gameplay.");
    WriteLine("=================================================================================================================================");
    WriteLine("Rules for the game: ");
    WriteLine("1. Place one of your numbers on the tic-tac-toe board.");
    WriteLine("2. Odds always goes first. ");
    WriteLine("3. Each number can only be used once.");
    WriteLine("4. The winner is the first player to complete a line(horizontal, vertical, or diagonal) with a sum of size(size * size + 1) / 2");
    WriteLine("5. The line may contain both even and odd numbers.");
    WriteLine("=================================================================================================================================");

    WriteLine("Are you ready? (y/n)");
    string ready = ReadLine() ?? "";
    if (ready == "n")
    {
      WriteLine("You can try it anytime, see you. ");
      return;
    }
  }

  public static void CustomizeGame()
  {
    // Board
    bool isSizeValid = false;
    while (!isSizeValid)
    {
      try
      {
        WriteLine("Enter a number to determine a board size for your game:");
        int size = int.Parse(ReadLine() ?? "");
        isSizeValid = size >= 3;
        if (!isSizeValid)
        {
          WriteLine($"!-- WARNING --! Your number must be greater or equal to 3.");
        }
      }
      catch (FormatException e)
      {
        WriteLine($"!-- WARNING --! Your entered number is not numeric. Error: {e}.");
      }
    }

    // Mode
    bool isModeValid = false;
    while (!isModeValid)
    {
      try
      {
        WriteLine("Enter 1 to play with human OR 2 with a computer:");
        int mode = int.Parse(ReadLine() ?? "");
        isModeValid = mode == 1 || mode == 2;
        if (!isModeValid)
        {
          WriteLine($"!-- WARNING --! Your number must 1 or 2");
        }
      }
      catch (FormatException e)
      {
        WriteLine($"!-- WARNING --! Your entered number is not numeric. Error: {e}.");
      }
    }
  }

  public void SetPlayers()
  {
    player1 = new Human(true, size);
    player2 = mode == 1 ? new Human(false, size) : new Computer(false, size);
    currentPlayer = player1;
  }

  public void StartGame()
  {
    board = new Board(size);
    int[] currentNumbers;

    while (true)
    {
      bool isMoveGridValid = false;
      int selectedNumber;
      int row = -1;
      int col = -1;

      // Human mode
      board.Display(currentPlayer.Name);

      // Entered number & validate if format is incorrect.
      currentNumbers = currentPlayer.ShowPlayerNumbers();

      while (true)
      {
        try
        {
          WriteLine($"({currentPlayer.Name}) Enter your number: {string.Join(", ", currentNumbers)}");
          selectedNumber = int.Parse(ReadLine() ?? "");
          break;
        }
        catch (FormatException e)
        {
          WriteLine($"!-- WARNING --! Your selected number is not in the right format. Error: {e} ");
        }
      }

      // validate the entered number within its lists.
      while (!currentPlayer.IsPlacedNumberValid(selectedNumber))
      {
        try
        {
          WriteLine($"Re-enter your number ({currentPlayer.Name}): {string.Join(", ", currentNumbers)}");
          selectedNumber = int.Parse(ReadLine() ?? "");
        }
        catch (FormatException e)
        {
          WriteLine($"!-- WARNING --! Your selected number is not in the right format. Error: {e} ");
        }
      }

      // validate the move in the grid
      while (!isMoveGridValid)
      {
        try
        {
          WriteLine($"({currentPlayer.Name}) Enter 0 to {size - 1} to set row: ");
          row = int.Parse(ReadLine() ?? "");

          WriteLine($"({currentPlayer.Name}) Enter 0 to {size - 1} to set column: ");
          col = int.Parse(ReadLine() ?? "");
        }
        catch (FormatException e)
        {
          WriteLine($"!-- WARNING --! Your move contains incorrect format. Error: {e} ");
        }
        isMoveGridValid = board.IsMoveGridValid(row, col, selectedNumber);
      }

      if (board.hasWon())
      {
        board.Display("Final Result");
        WriteLine("==============");
        WriteLine("Game over");
        WriteLine($"{currentPlayer.Name} wins.");
        WriteLine("==============");
        break;
      }
      currentPlayer = currentPlayer == player1 ? player2 : player1;
      // }
    }
  }
}