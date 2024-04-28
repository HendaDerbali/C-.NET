// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, World!");

//  Random Array :
int[] tab;


static Array RandomArray(Array tab)
{
tab = new int[] {1,3,4,5,6,7,8,9,10};

foreach (int i in tab)
{
    // We no longer need the index, because variable 'car' already represents each indexed value
    Console.WriteLine(tab);
}
return tab;
}
X = RandomArray(tab);

