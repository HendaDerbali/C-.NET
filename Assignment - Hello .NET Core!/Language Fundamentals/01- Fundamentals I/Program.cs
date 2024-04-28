// Create a Loop that prints all values from 1-255 : 
for (int i = 1; i <= 255; i++)
{
    // Console.WriteLine(i);
}

//  Create a new loop that prints all values from 1-100 that are divisible by 3 or 5, but not both
for (int i = 1; i <= 100; i++)
{
   
    if (i % 3 == 0 || i % 5 == 0 ) 
    { 
        if (!(i % 3 == 0 && i % 5 == 0) )
       
        Console.WriteLine(i);
}
    }
   
// Modify the previous loop to print "Fizz" for multiples of 3, "Buzz" for multiples of 5, and "FizzBuzz" for numbers that are multiples of both 3 and 5 :
for (int i = 1; i <= 100; i++)
{
   
    if (i % 3 == 0 && i % 5 == 0) 
        Console.WriteLine("FizzBuzz");
    else if (i % 3 == 0 )    
        Console.WriteLine("Fizz");
    else if (i % 5 == 0 )    
        Console.WriteLine("Buzz");    
     
            
    
    }
      