using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Api.Models.Admin.User;
using Microsoft.Extensions.Configuration;
using Api.Services.Admin.UserService;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Functions.Admin
{
    public class AuthFunction
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public AuthFunction(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [FunctionName("AuthFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }


        [FunctionName("GetMyName")]
        public async Task<IActionResult> GetMyName(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(_userService.GetMyName());

            //var userName = User?.Identity?.Name;
            //var roleClaims = User?.FindAll(ClaimTypes.Role);
            //var roles = roleClaims?.Select(c => c.Value).ToList();
            //var roles2 = User?.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();
            //return Ok(new { userName, roles, roles2 });

         
        }

        [FunctionName("Register")]
        public async Task<IActionResult> Register(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
           string userDtoUserName,
           string userDtoPassword,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string passwordHash
             = BCrypt.Net.BCrypt.HashPassword(userDtoPassword);

            user.Username = userDtoUserName;
            user.PasswordHash = passwordHash;

            return new OkObjectResult(user);

           

            //var userName = User?.Identity?.Name;
            //var roleClaims = User?.FindAll(ClaimTypes.Role);
            //var roles = roleClaims?.Select(c => c.Value).ToList();
            //var roles2 = User?.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();
            //return Ok(new { userName, roles, roles2 });


        }

        [FunctionName("Login")]
        public async Task<IActionResult> Login(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            string userDtoUserName,
           string userDtoPassword,
           ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (user.Username != userDtoUserName)
            {
                return new BadRequestObjectResult("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(userDtoPassword, user.PasswordHash))
            {
                return new BadRequestObjectResult("Wrong password.");
            }

            string token = CreateToken(user);

            return new OkObjectResult(token);


        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
