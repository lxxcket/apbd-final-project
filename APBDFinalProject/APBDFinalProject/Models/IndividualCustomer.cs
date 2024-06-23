namespace APBDFinalProject.Models;

public class IndividualCustomer : Customer
{
    public int PESEL { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsDeleted { get; set; }
}