namespace Human;


class Human
{
    // Properties for Human
    public string Name;
    
    public int Strength;
    public int Intelligence;
    public int Dexterity;
    public int Health;



    // Add a constructor that takes a value to set Name, and set the remaining fields to default values

    public Human(string Name)
    {
        Name = Name;
        Strength = 3;
        Intelligence = 3;
        Dexterity = 3;
        Health = 3 ;


    }



    // Add a constructor to assign custom values to all fields

    public Human(string Name, int Strength, int Intelligence, int Dexterity, int Health)
    {
        Name = Name;
        Strength = Strength;
        Intelligence = Intelligence;
        Dexterity = Dexterity;
        Health = Health ;


    }

    // Build Attack method
    public int Attack(Human target)
    {
  
        int dmg = Strength * 3;
        target.Health -= dmg;
        Console.WriteLine($"{Name} attacked {target.Name} for {dmg} damage!");
        return target.Health;
    }
    
}


