namespace APBDFinalProject.ResponseModels;

public class ContractResponse
{
    public int Id { get; set; }
    public int IdCustomer { get; set; }
    public int IdVersion { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalContractPrice { get; set; }
    public bool IsPaid { get; set; }
    public decimal AmountPaid { get; set; }
    public int SupportTime { get; set; }
    public int DaysSpan { get; set; }
}