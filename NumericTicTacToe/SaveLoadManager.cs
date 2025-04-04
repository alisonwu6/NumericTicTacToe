using System.Text.Json;
using static System.Console;

class SaveLoadManager
{
  private const string SavePath = "savegame.json";

  public static void Save(Board board, Player player1, Player player2, Player currentPlayer)
  {
    var gridList = new List<List<int?>>();
    for (int i = 0; i < board.Size; i++)
    {
      var row = new List<int?>();
      for (int j = 0; j < board.Size; j++)
      {
        row.Add(board.Grid[i, j]);
      }
      gridList.Add(row);
    }

    var state = new GameState
    {
      Size = board.Size,
      Grid = gridList,
      Player1Numbers = player1.IsOdd ? player1.OddNumbers : player1.EvenNumbers,
      Player2Numbers = player2.IsOdd ? player2.OddNumbers : player2.EvenNumbers,
      IsPlayer1Turn = currentPlayer == player1
    };

    var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
    File.WriteAllText(SavePath, JsonSerializer.Serialize(state, options));

    WriteLine("Game saved.");
  }

  public static GameState? Load()
  {
    if (!File.Exists(SavePath)) return null;

    var options = new JsonSerializerOptions { IncludeFields = true };
    string json = File.ReadAllText(SavePath);
    return JsonSerializer.Deserialize<GameState>(json, options);
  }
}
