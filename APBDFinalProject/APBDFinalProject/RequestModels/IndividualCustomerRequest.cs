namespace APBDFinalProject.RequestModels;

public class IndividualCustomerRequest
{
    public int PESEL { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}