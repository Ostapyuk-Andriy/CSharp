int[] numArray = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

string[] names = new string[] {"Tim", "Martin", "Nikki", "Sara"};

bool[] boolArray = new bool[10];
for(int i = 0; i < boolArray.Length; i++)
{
    if(i % 2 == 0)
    {
        boolArray[i] = true;
    }
    else
    {
        boolArray[i] = false;
    }
}

List<string> icecreamFlavors = new List<string>(){"Vanilla", "Chocolate", "Fruit", "Oreo", "Mix"};
// icecreamFlavors.Add("Vanilla");
// icecreamFlavors.Add("Chocolate");
// icecreamFlavors.Add("Fruit");
// icecreamFlavors.Add("Oreo");
// icecreamFlavors.Add("Mix");
System.Console.WriteLine(icecreamFlavors.Count);
System.Console.WriteLine(icecreamFlavors[2]);
icecreamFlavors.RemoveAt(2);
System.Console.WriteLine(icecreamFlavors.Count);

Dictionary<string,string> profile = new Dictionary<string,string>();
Random rand = new Random();
foreach(string name in names)
{
    int randomIdx = rand.Next(icecreamFlavors.Count);
    profile.Add(name, icecreamFlavors[randomIdx]);
}

foreach(KeyValuePair<string,string> entry in profile)
{
    Console.WriteLine($"{entry.Key} - {entry.Value}");
}
