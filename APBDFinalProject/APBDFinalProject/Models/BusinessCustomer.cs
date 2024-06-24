using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

[Table("Business_Customers")]
public class BusinessCustomer : Customer
{
    [Required]
    public int KRS { get; set; }
    [Required]
    [MaxLength(50)]
    public string BusinessName { get; set; }
    
}