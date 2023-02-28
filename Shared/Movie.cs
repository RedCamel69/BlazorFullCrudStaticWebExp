using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerceStaticWebApp.Shared
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Synopsis { get; set; }

        public int? Year { get; set; }
    }
}
