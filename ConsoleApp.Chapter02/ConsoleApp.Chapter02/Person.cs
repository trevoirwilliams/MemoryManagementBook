public class Person
{
    // Attributes - Properties and fields
    public string FirstName { get; set; } // property - public access
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    private int _age; // field - not publicly accessible

    // Methods
    public double GetAge()
    {
        return DateTime.Now.Year - DateOfBirth.Year;
    }
}
