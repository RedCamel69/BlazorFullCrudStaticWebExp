using BlazorEcommerceStaticWebApp.Shared;

namespace BlazorEcommerceStaticWebApp.Client.Services.LanguageService
{
    public interface ILanguageService
    {
        event Action LanguagesChanged;

        List<Language> Languages { get; set; }

        Task GetLanguages();
    }
}
