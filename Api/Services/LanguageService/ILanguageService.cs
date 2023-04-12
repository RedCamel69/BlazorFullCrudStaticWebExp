using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.LanguageService
{
    public interface ILanguageService
    {
        ServiceResponse<List<Language>> GetLanguages();
    }
}
