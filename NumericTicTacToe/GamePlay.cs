using static System.Console;
class GamePlay
{
  private Board board;
  private Player player1;
  private Player player2;
  private Player currentPlayer;
  private string gameMode;

  public void start()
  {
    WriteLine("Welcome To Numeric Tic-Tac-Toe Settings");
    // [1]-- Board Size: size x size
    // WriteLine("Please provide a number to determine a size of the board：");
    // int size = int.Parse(ReadLine());
    int size = 3;
    board = new Board(size);
    int[] currentNumbers;

    player1 = new(true, size);
    player2 = new(false, size);
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
        WriteLine("==============");
        WriteLine("Game over");
        WriteLine($"{currentPlayer.Name} wins.");
        WriteLine("==============");
        board.Display("Final Result");
        break;
      }

      currentPlayer = currentPlayer == player1 ? player2 : player1;
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