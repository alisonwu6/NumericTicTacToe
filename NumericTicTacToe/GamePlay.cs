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

  public void SetGame()
  {
    board = new Board(size);
    player1 = new Human(true, size);
    player2 = mode == 1 ? new Human(false, size) : new Computer(false, size);
    currentPlayer = player1;
  }

  public void StartGame()
  {
    while (true)
    {
      board.Display(currentPlayer.Name);

      currentPlayer.EnterNumber();
      var (row, col) = currentPlayer.EnterMove(board);

      if (board.IsValidMove(row, col))
      {
        board.PlaceNumber(row, col, currentPlayer.SelectedNumber);
        currentPlayer.UseNumber(currentPlayer.SelectedNumber);

        if (board.CheckWin())
        {
          board.Display(currentPlayer.Name);
          board.ShowResult(currentPlayer);
          break;
        }

        // 輪替
        currentPlayer = currentPlayer == player1 ? player2 : player1;
      }
      else
      {
        Console.WriteLine("Invalid move! Try again.");
      }
    }
  }
Ｆ
}

// validate the entered number within its lists.
// while (!currentPlayer.CheckEnteredNumber(selectedNumber))
// {
//   try
//   {
//     WriteLine($"({currentPlayer.Name}) Re-enter your number: {string.Join(", ", currentNumbers)}");
//     selectedNumber = int.Parse(ReadLine() ?? "");
//   }
//   catch (FormatException e)
//   {
//     WriteLine($"!-- WARNING --! Your selected number is not in the right format. Error: {e} ");
//   }
// }