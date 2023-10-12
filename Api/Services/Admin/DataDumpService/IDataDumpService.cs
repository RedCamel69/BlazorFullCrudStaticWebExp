using BlazorEcommerceStaticWebApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Admin.DataDumpService
{
    public interface IDataDumpService
    {
        ServiceResponse<List<Business>> GetBusinesses();

        ServiceResponse<List<Tutor>> GetTutors();

        ServiceResponse<Everything> GetEverything();

        ServiceResponse<String> Diagnostics2();
    }
}
