using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerceStaticWebApp.Shared.Validations
{
    public class ProtopageUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var urlToInspect = value as string;

            if (urlToInspect.Contains("//www.protopage.com") || (urlToInspect.Contains("//protopage.com")))
            {
                return true;
            }

            return false;

        }
    }
}
