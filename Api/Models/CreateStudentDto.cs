using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerceStaticWebApp.Api.Data;

public class CreateStudentDto
{
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string School { get; set; }
}