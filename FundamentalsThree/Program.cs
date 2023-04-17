static void PrintList(List<string> MyList)
{
    // Your code here
    foreach(string list in MyList){
        System.Console.WriteLine(list);
    }
}
List<string> TestStringList = new List<string>() {"Harry", "Steve", "Carla", "Jeanne"};
PrintList(TestStringList);

static void SumOfNumbers(List<int> IntList)
{
    // Your code here
    int sum = 0;
    foreach(int integer in IntList)
    {
        sum +=integer;
    }
        System.Console.WriteLine(sum);
}
List<int> TestIntList = new List<int>() {2,7,12,9,3};
// You should get back 33 in this example
SumOfNumbers(TestIntList);

static int FindMax(List<int> IntList)
{
    // Your code here
    int maxVal = IntList.Max();
    System.Console.WriteLine(maxVal);
    return maxVal;
}
List<int> TestIntList2 = new List<int>() {-9,12,10,3,17,5};
// You should get back 17 in this example
FindMax(TestIntList2);

static List<int> SquareValues(List<int> IntList)
{
    // Your code here
    List<int> squared = new List<int>();
    foreach(int integer in IntList)
    {
        int SquaredVal = integer * integer;
        squared.Add(SquaredVal);
        System.Console.WriteLine(SquaredVal);
    }
    return squared;
}
List<int> TestIntList3 = new List<int>() {1,2,3,4,5};
// You should get back [1,4,9,16,25], think about how you will show that this worked
SquareValues(TestIntList3);

static int[] NonNegatives(int[] IntArray)
{
    // Your code here
    int[] result = new int[IntArray.Length];
    for(int i = 0; i < IntArray.Length; i++)
    {
        if(IntArray[i] < 0)
        {
            result[i] = 0;
        }
        else
        {
            result[i] = IntArray[i];
        }
    }
    foreach(int number in result)
    {
        System.Console.WriteLine(number);
    }
    return result;
}
int[] TestIntArray = new int[] {-1,2,3,-4,5};

// You should get back [0,2,3,0,5], think about how you will show that this worked
NonNegatives(TestIntArray);

static void PrintDictionary(Dictionary<string,string> MyDictionary)
{
    // Your code here
    foreach(KeyValuePair<string, string> entry in MyDictionary){
        System.Console.WriteLine($"{entry.Key} - {entry.Value}");
    }
}
Dictionary<string,string> TestDict = new Dictionary<string,string>();
TestDict.Add("HeroName", "Iron Man");
TestDict.Add("RealName", "Tony Stark");
TestDict.Add("Powers", "Money and intelligence");
PrintDictionary(TestDict);

static bool FindKey(Dictionary<string,string> MyDictionary, string SearchTerm)
{
    // Your code here
    return MyDictionary.ContainsKey(SearchTerm);
}
// Use the TestDict from the earlier example or make your own
// This should print true
Console.WriteLine(FindKey(TestDict, "RealName"));
// This should print false
Console.WriteLine(FindKey(TestDict, "Name"));

// Ex: Given ["Julie", "Harold", "James", "Monica"] and [6,12,7,10], return a dictionary
// {
//	"Julie": 6,
//	"Harold": 12,
//	"James": 7,
//	"Monica": 10
// } 
static Dictionary<string,int> GenerateDictionary(List<string> Names, List<int> Numbers)
{
    // Your code here
    Dictionary<string, int> dict = new Dictionary<string, int>();
    for(int i = 0; i < Names.Count; i++)
    {
        dict.Add(Names[i], Numbers[i]);
        System.Console.WriteLine($"{Names[i]} - {Numbers[i]}");
    }
    return dict;
}
// We've shown several examples of how to set your tests up properly, it's your turn to set it up!
// Your test code here
List<string> names = new List<string>{"Julie", "Harold", "James", "Monica"};
List<int> numbers = new List<int>{6,12,7,10};
GenerateDictionary(names, numbers);



