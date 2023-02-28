using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerceStaticWebApp.Shared
{
    [Table("Images")]
    public class Image
    {
        public int Id { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
