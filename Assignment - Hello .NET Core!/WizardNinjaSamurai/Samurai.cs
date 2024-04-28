namespace WizardNinjaSamurai;

public class Samurai : Human
{
     public Samurai(int Strength,int Intelligence, int Dexterity) : base ("Ninja", Strength, Intelligence,Dexterity, 200 )
    {}

        // Override Attack method
    public override int Attack(Human target)
    {
       int  y=base.Attack(target) ;
       if (  y <50 )
       {
            target.Health = 0;
       }
      

        return target.Health;

    }

    public  int Mediate(Samurai targetSamurai, Human target)
    {
        targetSamurai.Health = target.Health;
        return targetSamurai.Health;
    }
}
