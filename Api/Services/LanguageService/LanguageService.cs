using BlazorEcommerceStaticWebApp.Api.Data;
using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Services.LanguageService
{
    public class LanguageService : ILanguageService
    {
        private readonly ApplicationDbContext _context;

        public LanguageService(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public ServiceResponse<List<Language>> GetLanguages()
        {
            var response = new ServiceResponse<List<Language>>();
            try { 
                response.Data = _context.Languages.ToList<Language>();
                response.Success = true;
                response.Message = "Successfully retrieved languages";

            }
            catch ( Exception ex )
            {
                response.Data = null;
                response.Success = false;
                response.Message = $"Failed to retrieve languages : Error : {ex.Message}";

            }

            return response;
        }
    }
}
