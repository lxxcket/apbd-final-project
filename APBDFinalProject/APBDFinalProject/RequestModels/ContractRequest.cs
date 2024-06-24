namespace APBDFinalProject.RequestModels;

public class ContractRequest
{
    public int IdCustomer { get; set; }
    public int IdVersion { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SupportTime { get; set; }
    public int DaysSpan { get; set; }
    
}