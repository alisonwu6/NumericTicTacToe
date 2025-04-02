using static System.Console;

class Player
{
  private int[] AllGameNumbers;
  private int AllNumbersCount;
  private bool IsOdd;
  public string Name;
  public int[] OddNumbers;
  public int[] EvenNumbers;


  public Player(bool isOdd, int size)
  {
    IsOdd = isOdd;
    string order = IsOdd ? "1" : "2";
    Name = "Player " + order;

    AllNumbersCount = size * size;
    AllGameNumbers = new int[AllNumbersCount];
    OddNumbers = new int[(AllNumbersCount + 1) / 2];
    EvenNumbers = new int[AllNumbersCount / 2];

    int oddCount = 0;
    int evenCount = 0;
    for (int num = 1; num < AllGameNumbers.Length + 1; num++)
    {
      AllGameNumbers[num - 1] = num;

      if (IsOdd && num % 2 == 1)
      {
        OddNumbers[oddCount] = num;
        oddCount++;
      }

      if (!IsOdd && num % 2 == 0)
      {
        EvenNumbers[evenCount] = num;
        evenCount++;
      }
    }
  }

  public int[] ShowPlayerNumbers()
  {
    return IsOdd ? OddNumbers : EvenNumbers;
  }

  public bool IsPlacedNumberValid(int selectedNumber)
  {
    int[] number = IsOdd ? OddNumbers : EvenNumbers;

    for (int i = 0; i < number.Length; i++)
    {
      if (selectedNumber == number[i])
      {
        number[i] = -1;
        return true;
      }
    }

    return false;
  }
}