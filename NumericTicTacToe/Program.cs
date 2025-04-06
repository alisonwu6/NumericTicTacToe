using static System.Console;
namespace NumericTicTacToe;

class Program
{
    static void Main(string[] args)
    {
        WriteLine("Welcome To Numeric Tic-Tac-Toe Gameplay.");
        GamePlay gamePlay = new();
        GamePlay.HelpMenu();
        gamePlay.CheckLastGameState();
        gamePlay.StartGame();
    }
}

/*
 * 1. Board
 *    - n * n
 *
 * 2. Players
 *  numbers used n * n
 *  oddPlayer: 1,3,5,7,9
 *  evenPlayer: 2,4,6,8
 *
 * 3. Winning Checks
 *   - a horizontal, vertical, or diagonal line
 *   - n(n * n + 1)/2 points wins the game
 *   - validate moves
 *
 * 4. Two Modes
 *   - Human vs human
 *   - Human vs computer
 *
 * 5. State saved and restored
 *
 * 6. Guide users
 *    1. load an existing game
 *          or
 *    2. initiate a fresh game
 *      1. choose game mode & size of the board
 *      2. display current game board and prompts the player to make a move, save the game or view the `help menu`.
 *      3. display the final result before existing
 */
