using BlazorEcommerceStaticWebApp.Shared.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerceStaticWebApp.Shared
{
    public class Tutor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Url]
        [ProtopageUrl(ErrorMessage = "Not a valid Protopage Url")]
        public string ProtopageUrl { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Phone]
        public string? MobilePhone { get; set; }

        [Required(ErrorMessage = "Please select a business")]
        public int? BusinessId { get; set; }

        public Business Business { get; set; }
    }
}
