using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

[Table("Payments")]
public class Payment
{
    [Key]
    public int Id { get; set; }
    public int IdContract { get; set; }
    [Required]
    public DateTime PaymentDate { get; set; }
    [Required]
    public decimal AmountPaid { get; set; }
    [ForeignKey(nameof(IdContract))]
    public virtual Contract Contract { get; set; }
}