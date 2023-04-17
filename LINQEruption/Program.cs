List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46, "Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};
// Example Query - Prints all Stratovolcano eruptions
// IEnumerable<Eruption> stratovolcanoEruptions = eruptions.Where(c => c.Type == "Stratovolcano");
// PrintEach(stratovolcanoEruptions, "Stratovolcano eruptions.");
// Execute Assignment Tasks here!

// Query 1
Eruption? chileEruption = eruptions.FirstOrDefault(e => e.Location == "Chile");
System.Console.WriteLine(chileEruption);

// Query 2?
Eruption? hawaiianEruption = eruptions.FirstOrDefault(e => e.Location == "Hawaiian Is");
System.Console.WriteLine(hawaiianEruption);
if(hawaiianEruption == null)
{
    System.Console.WriteLine("No Hawaiian Eruption found");
}

// Query 3?
Eruption? greenlandEruption = eruptions.FirstOrDefault(e => e.Location == "Greenland");
System.Console.WriteLine(greenlandEruption);
if(greenlandEruption == null)
{
    System.Console.WriteLine("No Greenland Eruption found");
}

// Query 4
List<Eruption> newList = eruptions.Where(e => e.Year > 1900 && e.Location == "New Zealand").ToList();
PrintEach(newList);

// Query 5
List<Eruption> overList = eruptions.Where(e => e.ElevationInMeters > 2000).ToList();
PrintEach(overList);

// Query 6
List<Eruption> startList = eruptions.Where(e => e.Volcano.StartsWith("L")).ToList();
int count = startList.Count();
PrintEach(startList);
System.Console.WriteLine($"There are {count} eruptions found");

// Query 7
int maxVal = eruptions.Max(e => e.ElevationInMeters);
System.Console.WriteLine(maxVal);

// Query 8 
List<string> hasList = eruptions.Where(e => e.ElevationInMeters == maxVal).Select(e => e.Volcano).ToList();
foreach(string v in hasList)
{
    System.Console.WriteLine(v);
}

// Query 9 
List<string> alphaList = eruptions.OrderBy(e => e.Volcano).Select(e => e.Volcano).ToList();
foreach(string item in alphaList)
{
    System.Console.WriteLine(item);
}

// Query 10
int sum = eruptions.Sum(e => e.ElevationInMeters);
System.Console.WriteLine(sum);

// Query 11
Boolean isFound = eruptions.Any(e => e.Year == 2000);
System.Console.WriteLine(isFound ? "Found" : "Not Found");

// Query 12
IEnumerable<Eruption> stratovolcanoEruptions = eruptions.Where(c => c.Type == "Stratovolcano").Take(3);
PrintEach(stratovolcanoEruptions);

// Query 13
List<Eruption> orderedList = eruptions.Where(e => e.Year < 1000).OrderBy(e => e.Volcano).ToList();
PrintEach(orderedList);

// Query 14
List<string> orderedNameList = eruptions.Where(e => e.Year < 1000).OrderBy(e => e.Volcano).Select(e => e.Volcano).ToList();
foreach(string e in orderedNameList)
{
    System.Console.WriteLine(e);
}

// Query 15
static void PrintEach(IEnumerable<Eruption> items, string msg = "")
{
    Console.WriteLine("\n" + msg);
    foreach (Eruption item in items)
    {
        Console.WriteLine(item.ToString());
    }
}

