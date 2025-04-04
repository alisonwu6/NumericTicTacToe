public abstract class Player
{
  protected bool IsOdd;
  public string Name;
  public int[] OddNumbers;
  public int[] EvenNumbers;
  public int selectedNumber;

  public Player(bool isOdd, int size)
  {
    IsOdd = isOdd;
    Name = "Player " + (IsOdd ? "1" : "2");

    int total = size * size;
    OddNumbers = new int[(total + 1) / 2];
    EvenNumbers = new int[total / 2];

    int oddIndex = 0, evenIndex = 0;
    for (int i = 1; i <= total; i++)
    {
      if (i % 2 == 1 && IsOdd)
      {
        OddNumbers[oddIndex++] = i;
      }
      else if (i % 2 == 0 && !IsOdd)
      {
        EvenNumbers[evenIndex++] = i;
      }
    }
  }

  public abstract void EnterNumber();
  public abstract (int, int) EnterMove(Board board);

  public void UseNumber(int usedNumber)
  {
    int[] numbers = IsOdd ? OddNumbers : EvenNumbers;
    for (int i = 0; i < numbers.Length; i++)
    {
      if (numbers[i] == usedNumber)
      {
        numbers[i] = -1;
        break;
      }
    }
  }
}
