using static System.Console;

class GamePlay
{
  private Board? board;
  private Player? player1;
  private Player? player2;
  private Player? currentPlayer;
  private int size = 3;
  private int mode = 1;

  public void CheckLastGameState()
  {
    if (File.Exists("last_game_state.json"))
    {
      WriteLine("Detected a saved game. Do you want to continue? (y/n)");
      string answer = ReadLine() ?? "";

      if (answer == "y")
      {
        var saved = SaveLoadManager.Load();
        if (saved != null)
        {
          board = new Board(saved.Size);
          board.LoadGrid(saved.Grid);

          player1 = new Human(true, saved.Size);
          player2 = mode == 1 ? new Human(false, saved.Size) : new Computer(false, saved.Size);

          if (player1.IsOdd)
          {
            player1.OddNumbers = saved.Player1Numbers;
          }
          else
          {
            player1.EvenNumbers = saved.Player1Numbers;
          }

          if (player2.IsOdd)
          {
            player2.OddNumbers = saved.Player2Numbers;
          }
          else
          {
            player2.EvenNumbers = saved.Player2Numbers;
          }

          currentPlayer = saved.IsPlayer1Turn ? player1 : player2;
          WriteLine("Loaded previous game.");
        }
        else
        {
          WriteLine("Failed to load saved game. Starting new game.");
        }
      }
      else
      {
        CustomizeGame();
        SetGame();
      }
    }
    else
    {
      Guide();
      CustomizeGame();
      SetGame();
    }
  }

  public static void Guide()
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

  public void CustomizeGame()
  {
    // Board Size
    bool isSizeValid = false;
    while (!isSizeValid)
    {
      try
      {
        WriteLine("Enter a number to determine a board size for your game:");
        int input = int.Parse(ReadLine() ?? "");
        if (input >= 3)
        {
          size = input;
          isSizeValid = true;
        }
        else
        {
          WriteLine("!-- WARNING --! The board size must be >= 3.");
        }
      }
      catch (FormatException e)
      {
        WriteLine($"!-- WARNING --! Invalid number format. Error: {e.Message}");
      }
    }

    // Game Mode
    bool isModeValid = false;
    while (!isModeValid)
    {
      try
      {
        WriteLine("Enter 1 to play with another human, or 2 to play with a computer:");
        int input = int.Parse(ReadLine() ?? "");
        if (input == 1 || input == 2)
        {
          mode = input;
          isModeValid = true;
        }
        else
        {
          WriteLine("!-- WARNING --! Only 1 or 2 is accepted.");
        }
      }
      catch (FormatException e)
      {
        WriteLine($"!-- WARNING --! Invalid number format. Error: {e.Message}");
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
      WriteLine("\nOptions: (P)lay | (S)ave | (Q)uit");
      string choice = ReadLine() ?? "";

      if (choice == "s")
      {
        SaveLoadManager.Save(board, player1, player2, currentPlayer);
        WriteLine("Game progress saved.");
        continue;
      }
      else if (choice == "q")
      {
        WriteLine("Game exited.");
        return;
      }
      else if (choice != "p")
      {
        WriteLine("Invalid input. Please enter P, S, or Q.");
        continue;
      }

      board.Display(currentPlayer.Name);
      currentPlayer.EnterNumber();
      var (row, col) = currentPlayer.EnterMove(board);

      if (board.PlaceNumber(row, col, currentPlayer.selectedNumber))
      {
        currentPlayer.UseNumber(currentPlayer.selectedNumber);

        if (board.CheckWin())
        {
          board.Display(currentPlayer.Name);
          board.ShowResult(currentPlayer);
          File.Delete("last_game_state.json");
          break;
        }

        SaveLoadManager.Save(board, player1, player2, currentPlayer);

        currentPlayer = currentPlayer == player1 ? player2 : player1;
      }
      else
      {
        WriteLine("Invalid move. Please try again.");
      }
    }
  }
}
