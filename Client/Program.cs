using BlazorEcommerceStaticWebApp.Client;
using BlazorEcommerceStaticWebApp.Client.Services.Admin;
using BlazorEcommerceStaticWebApp.Client.Services.BusinessService;
using BlazorEcommerceStaticWebApp.Client.Services.CourseService;
using BlazorEcommerceStaticWebApp.Client.Services.Helper;
using BlazorEcommerceStaticWebApp.Client.Services.LanguageService;
using BlazorEcommerceStaticWebApp.Client.Services.ProductService;
using BlazorEcommerceStaticWebApp.Client.Services.StudentService;
using BlazorEcommerceStaticWebApp.Client.Services.TutorService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Azure.Functions.Authentication.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<IBusinessService, BusinessService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<ISyncService, SyncService>();

builder.Services.AddScoped<StateContainer>();

builder.Services.AddStaticWebAppsAuthentication();



await builder.Build().RunAsync();
