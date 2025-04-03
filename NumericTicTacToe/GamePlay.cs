using static System.Console;
class GamePlay
{
  private Board board;
  private Human player1;
  private Player player2;
  private Player currentPlayer;
  private bool isAgainstComputer = false;

  public void start()
  {


    WriteLine("Welcome To Numeric Tic-Tac-Toe Settings");
    // [1]-- Board Size: size x size
    WriteLine("Please provide a number to determine a size of the board：");
    int size = int.Parse(ReadLine() ?? "");

    WriteLine("Would you like to play with human or with computer: Enter 1 for human, 2 for computer.");
    int mode = int.Parse(ReadLine() ?? "");
    isAgainstComputer = mode == 2;

    WriteLine("==========================================================================================================================");
    WriteLine("Rules for the game: ");
    WriteLine("First player is going to have odd numbers, and the 2ed one is going to have even numbers; ");
    WriteLine("whichever player has the odds ALWAYS GOES FIRST.");
    WriteLine("Each number can only be used once.");
    WriteLine($"The winner is the first player to complete a line (horizontal, vertical, or diagonal) with a sum of {size * (size * size + 1) / 2}");
    WriteLine("The line may contain both even and odd numbers.");
    WriteLine("==========================================================================================================================");

    WriteLine("Are you ready? (y/n)");
    string ready = ReadLine() ?? "";
    if (ready == "n") {
      return;
    } 


    board = new Board(size);
    int[] currentNumbers;

    player1 = new Human(true, size);

    if (isAgainstComputer)
    {
      player2 = new Computer(true, size);
    }
    else
    {

      player2 = new Human(false, size);
      currentPlayer = player1;

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
            WriteLine($"Enter your numbers ({currentPlayer.Name}): {string.Join(", ", currentNumbers)}");
            selectedNumber = int.Parse(ReadLine() ?? "");
            break;
          }
          catch (FormatException e)
          {
            WriteLine($"***WARNING!!! Your selected number is not in the right format. Error: {e} ***");
          }
        }

        // validate the entered number within its lists.
        while (!currentPlayer.IsPlacedNumberValid(selectedNumber))
        {
          try
          {
            WriteLine($"Re-enter your numbers ({currentPlayer.Name}): {string.Join(", ", currentNumbers)}");
            selectedNumber = int.Parse(ReadLine() ?? "");
          }
          catch (FormatException e)
          {
            WriteLine($"***WARNING!!! Your selected number is not in the right format. Error: {e} ***");
          }
        }

        // validate the move in the grid
        while (!isMoveGridValid)
        {
          try
          {
            WriteLine($"{currentPlayer.Name}: Enter 0 to {size - 1} to set row: ");
            row = int.Parse(ReadLine() ?? "");

            WriteLine($"{currentPlayer.Name}: Enter 0 到 {size - 1} to set column: ");
            col = int.Parse(ReadLine() ?? "");
          }
          catch (FormatException e)
          {
            WriteLine($"***WARNING!!! Your move contains incorrect format. Error: {e} ***");
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
      }
    }
    /*
    * 1. choose game mode
    * 2. size of the board
    * 3. display current game board and prompts the player to make a move,
    *    save the game or view the `help menu`.
    * 4. display the final result before existing
    */
  }
}