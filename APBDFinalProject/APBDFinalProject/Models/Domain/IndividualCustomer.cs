using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

[Table("Individual_Customers")]
public class IndividualCustomer : Customer
{
    [Required]
    [MaxLength(11)]
    public long PESEL { get; set; }
    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(30)]
    public string LastName { get; set; }
    [Required]
    public bool IsDeleted { get; set; }
}