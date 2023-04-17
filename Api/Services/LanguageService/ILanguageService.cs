using BlazorEcommerceStaticWebApp.Shared;
using System.Collections.Generic;

namespace Api.Services.LanguageService
{
    public interface ILanguageService
    {
        ServiceResponse<List<Language>> GetLanguages();
    }
}
