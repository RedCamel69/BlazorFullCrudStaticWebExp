using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerceStaticWebApp.Shared
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public int StudentCapacity { get; set; }

        [Required]
        public int? LanguageId { get; set; }
        public Language? Language { get; set; }

        [Required]
        public int? TutorId { get; set; }
        public Tutor? Tutor { get; set; }

        //todo: student/course many to many
    }
}
