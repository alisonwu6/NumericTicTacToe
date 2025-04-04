using System.Collections.Generic;

public class GameState
{
  public int Size;
  public List<List<int?>> Grid = new();
  public int[] Player1Numbers = new int[0];
  public int[] Player2Numbers = new int[0];
  public bool IsPlayer1Turn;
}
