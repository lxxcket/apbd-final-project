using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

[Table("Discounts")]
public class Discount
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Description { get; set; }
    [Required]
    public decimal DiscountPercentage { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    
}