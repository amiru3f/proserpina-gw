using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Proserpina.Auth.Api.Service;
using Proserpina.Common;

namespace Proserpina.Auth.Api.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        public AuthorizationController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        [HttpGet, Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [Route("getToken")]
        public async Task<IActionResult> GetToken([FromQuery] string username, [FromQuery] string password)
        {
            User user = await authorizationService.AuthorizeAsync(username, password);
            if (null == user)
            {
                return Unauthorized();
            }

            return Ok(System.Guid.NewGuid().ToString());
        }

        [HttpGet("validateToken")]
        public async Task<IActionResult> IsValidToken()
        {
            Microsoft.Extensions.Primitives.StringValues tokenValues;
            string token = string.Empty;
            if (Request.Headers.TryGetValue("token", out tokenValues))
            {
                token = tokenValues.FirstOrDefault()?.Trim();
                if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
            }
            else
            {
                return Unauthorized();
            }

            User user = await authorizationService.DeserializeToken(token);
            if (null == user) return Unauthorized();

            Response.Headers.Add("user-id", user.Id.ToString());
            Response.Headers.Add("fullname", $"{user.Firstname} {user.Lastname}");

            return Ok();
        }
    }
}