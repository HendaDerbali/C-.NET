namespace WizardNinjaSamurai;

public class Ninja : Human
{
    public Ninja(int Strength,int Intelligence, int Health) : base ("Ninja", Strength, Intelligence,75, Health )
    {}

    // Override Attack method
    public override int Attack(Human target)
    {
       int  y=base.Attack(target) -3 * target.Dexterity ;

        return y;

    }

    public  int Steal(Ninja targetNinja, Human target)
    {
        target.Health -= 5 ;
        targetNinja.Health += 5;
        return targetNinja.Health;
    }
}