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
    int[] currentPlayerNumbers;


    /*
    * 2.Players
    * numbers used n *n
    * oddPlayer: 1,3,5,7,9
    * evenPlayer: 2,4,6,8
    */

    Player player1 = new(true, size);
    Player player2 = new(false, size);
    currentPlayer = player1;
    currentPlayerNumbers = currentPlayer.ShowPlayerNumbers();

    while (true)
    {
      bool isMoveValid = false;
      int row = -1;
      int col = -1;

      // Human mode
      board.Display();

      // Choose a number
      currentNumbers = currentPlayer.ShowPlayerNumbers();
      WriteLine($"{currentPlayer.Name}: Choose your numbers: {string.Join(", ", currentNumbers)}");
      int selectedNumber = int.Parse(ReadLine() ?? "");

      if (currentPlayer.IsPlacedNumberValid(selectedNumber))
      {
        while (!isMoveValid)
        {
          WriteLine($"{currentPlayer.Name}: Enter 0 to {size - 1} to set row: ");
          row = int.Parse(ReadLine() ?? "");

          WriteLine($"{currentPlayer.Name}: Enter 0 到 {size - 1} to set column: ");
          col = int.Parse(ReadLine() ?? "");

          isMoveValid = board.IsMoveValid(row, col, selectedNumber);
        }
      }




      currentPlayer = currentPlayer == player1 ? player2 : player1;

    }

    // Start a game

    /*
    * 1. choose game mode
    * 2. size of the board
    * 3. display current game board and prompts the player to make a move,
    *    save the game or view the `help menu`.
    * 4. display the final result before existing
    */
  }
}