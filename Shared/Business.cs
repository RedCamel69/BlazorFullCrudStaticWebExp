using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerceStaticWebApp.Shared
{
    [Index(nameof(Name), IsUnique = true)] // using Microsoft.EntityFrameworkCore
    public class Business
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

       // public ICollection<Tutor>? Tutors { get; set; }
    }
}
