using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerceStaticWebApp.Shared
{
    [Table("Images")]
    public class Image
    {
        public int Id { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
