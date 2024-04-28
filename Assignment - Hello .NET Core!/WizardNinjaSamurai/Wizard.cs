namespace WizardNinjaSamurai;

public class Wizard : Human
{
    public Wizard(int Strength,int Dexterity ) : base ("Wizard", Strength, 25, Dexterity, 50 )
    {
    }

       // Override Attack method
    public override int Attack(Human target)
    {
       int  x=base.Attack(target) -3 * target.Intelligence ;

        return x;

    }

        public  int Heal(Human target)
    {
        target.Health +=3* target.Intelligence;
        return target.Health;
    }
}



