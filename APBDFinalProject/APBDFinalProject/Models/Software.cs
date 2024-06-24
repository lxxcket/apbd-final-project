using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

public enum Category
{
    Finance, Education
}
[Table("Softwares")]
public class Software
{
    [Key]
    public int Id { get; set; }
    [Required]
    public decimal YearlyPrice { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required]
    public Category Category { get; set; }

    public ICollection<Version> Versions { get; set; } = new List<Version>();
}