namespace APBDFinalProject.RequestModels;

public class PaymentRequest
{
    public int IdContract { get; set; }
    public int IdCustomer { get; set; }
    public decimal AmountToPay { get; set; }
}