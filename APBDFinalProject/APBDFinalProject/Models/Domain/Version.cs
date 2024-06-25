using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBDFinalProject.Models;

[Table("Versions")]
public class Version
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required]
    [MaxLength(20)]
    public string VersionName { get; set; }
    [Required]
    public int IdSoftware { get; set; }
    [ForeignKey(nameof(IdSoftware))]
    public Software Software { get; set; }
    
}