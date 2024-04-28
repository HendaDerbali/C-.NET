// Create an array to hold integer values 0 through 9 :
int[] arr = new int[10];
for (int i = 0 ; i<= 9; i++ ) 
{
    arr[i] = i;
}
// Create an array of the names "Tim", "Martin", "Nikki", & "Sara"

string[] array3;
array3 = new string[] {"Tim", "Martin", "Nikki", "Sara"};



for ( int i =0 ; i< array3.Length; i++)
Console.WriteLine(array3[i]);


// Create an array of length 10 that alternates between true and false values, starting with true
bool[] arr4 = new bool[10];
for (int i = 0 ; i<= 9 ; i = i+2) 
{
    arr4[i] = true;
    arr4[i+1] = false;

}
for ( int i =0 ; i< arr4.Length; i++)
Console.WriteLine(arr4[i]);


// Create a list of ice cream flavors that holds at least 5 different flavors (feel free to add more than 5!)
List<string> flavors = new List<string>();
flavors.Add("flavor1");
flavors.Add("flavor2");
flavors.Add("flavor3");
flavors.Add("flavor4");
flavors.Add("flavor5");
Console.WriteLine(flavors.Count);
Console.WriteLine(flavors[2]);
flavors.Remove("flavor3");
Console.WriteLine(flavors.Count);

// User Info Dictionary :
Dictionary<string,string> dic = new Dictionary<string,string>();
    for ( int i =0 ; i< array3.Length; i++ )
   { 
    dic.Add(array3[i], flavors[i]);
   }

    foreach (KeyValuePair<string,string> entry in dic)
{
    Console.WriteLine(entry.Key + " - " + entry.Value);
}






