using WebshopProject.Api.Helpers;
using WebshopProject.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopProject.Api.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, ICustomerService customerService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var customerId = jwtUtils.ValidateJwtToken(token);
            if (customerId != null)
            {
                // attach customer to context on successful jwt validation
                context.Items["Customer"] = await customerService.GetById(customerId.Value);
            }

            await _next(context);
        }
    }
}
