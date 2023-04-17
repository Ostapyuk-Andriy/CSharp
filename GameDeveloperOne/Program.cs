
Attack Fireball = new Attack("Fireball", 10);
Attack IceBlast = new Attack("Ice Blast", 8);
Attack WrathOfNature = new Attack("Wrath of Nature", 100);

List<Attack> AllAttacks = new List<Attack>()
{
    Fireball, IceBlast, WrathOfNature 
};
Enemy Mage = new Enemy("Mage", 100, AllAttacks);
Mage.RandomAttack(AllAttacks);

class Attack
{
    public string Name{get; set;}
    public int DamageAmount{get; set;}
    public Attack(string name, int damageAmount)
    {
        Name = name;
        DamageAmount = damageAmount;
    }
}

class Enemy
{
    public string Name{get; set;}
    public int Health{get; set;} = 100;
    public List<Attack> AllAttacks{get; set;} = new List<Attack>();
    public Enemy(string name, int health, List<Attack> allAttacks)
    {
        Name = name;
        Health = health;
        AllAttacks = allAttacks;
    }
    public Attack RandomAttack(List<Attack> AllAttacks)
    {
        Random rand = new Random();
        int i = rand.Next(AllAttacks.Count);
        System.Console.WriteLine(AllAttacks[i].Name);
        return AllAttacks[i];
    }
}

