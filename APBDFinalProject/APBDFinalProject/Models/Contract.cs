using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

[Table("Contracts")]
public class Contract
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int IdCustomer { get; set; }
    [Required]
    public int IdVersion { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public decimal TotalContractPrice { get; set; }
    [Required]
    public bool IsPaid { get; set; }
    [Required]
    public decimal AmountPaid { get; set; }
    [Required]
    [Range(1,4)]
    public int SupportTime { get; set; }
    
    [Range(3,30)]
    public int DaysSpan { get; set; }
    
    [ForeignKey(nameof(IdCustomer))]
    public Customer Customer { get; set; } = null!;
    [ForeignKey(nameof(IdVersion))]
    public Version Version { get; set; } = null!;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}