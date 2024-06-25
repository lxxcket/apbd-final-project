using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

[Table("Customers")]
public abstract class Customer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Address { get; set; }
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
    [Required]
    [MaxLength(9)]
    public string PhoneNumber { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    
}