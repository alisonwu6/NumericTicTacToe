using static System.Console;

class Player
{
  private int[] AllGameNumbers;
  private int AllNumbersCount;
  private readonly bool IsOdd;
  public string Name;
  public int[] OddNumbers;
  public int[] EvenNumbers;
  private int[]? currentNumbers;
  public int selectedNumber;


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

  public void EnterNumber()
  {
    currentNumbers = IsOdd ? OddNumbers : EvenNumbers;
    try
    {
      WriteLine($"({Name}) Enter your number: {string.Join(", ", currentNumbers)}");
      selectedNumber = int.Parse(ReadLine() ?? "");
    }
    catch (FormatException e)
    {
      WriteLine($"!-- WARNING --! Your selected number is not in the right format. Error: {e} ");
    }
  }

  // public bool CheckEnteredNumber(int selectedNumber)
  // {
  //   int[] number = IsOdd ? OddNumbers : EvenNumbers;

  //   if (selectedNumber == -1)
  //   {
  //     WriteLine($"!-- WARNING --! -1 is a flag indicates that position of number has been placed on the board.");
  //     return false;
  //   }

  //   for (int i = 0; i < number.Length; i++)
  //   {
  //     if (selectedNumber == number[i])
  //     {
  //       number[i] = -1;
  //       return true;
  //     }
  //   }

  //   WriteLine($"!-- WARNING --! Your selected number is not in the {(IsOdd ? "odd" : "even")} number list.");
  //   return false;
  // }
}

// ChooseNumber 記錄數字
// ChoosePosition 選擇位置