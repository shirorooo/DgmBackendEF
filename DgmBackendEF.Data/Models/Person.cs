using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DgmBackendEF.Data.Models;

[Table("Person")]
public class Person
{
    public int Id { get; set; }
    [Required]

    public string? Name { get; set; }
    [Required]

    public string? Gender { get; set; }
    [Required]

    public string? Birthday { get; set; }
    [Required]

    public int? Age { get; set; }
    [Required]

    public string ContactNo {  get; set; }
    [Required]

    public string Address {  get; set; }
    [Required]

    public string? Email { get; set; }

    public string? FBName { get; set; }

    public string? School { get; set; }

    public string? DgroupLeader {  get; set; }
}
