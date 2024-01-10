int num1 = 3;
int num2 = num1;
num1 = 5;
Console.WriteLine($"num1: {num1}");
Console.WriteLine($"num2: {num2}");

Person nobody; // null reference
Person arthur = new Person
{
    FirstName = "Arthur",
    LastName = "Wint",
    DateOfBirth = new DateOnly(2001,10,25)
}; // reference to Person object 
Person james = arthur;
string fullName = person.GetFullName();
Console.WriteLine($"Full name: {fullName}");