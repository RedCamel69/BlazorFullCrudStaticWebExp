using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerceStaticWebApp.Shared
{
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
