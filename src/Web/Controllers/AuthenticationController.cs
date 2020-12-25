using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Web.Common;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identity;

        public AuthenticationController(IIdentityService identity)
        {
            _identity = identity;
        }

        [HttpPost("Authorize")]
        public async Task<IAppUser> Authorize()
        {
            var token = HttpContext.Request.Cookies["access_token"];
            var authenticated = HttpContext.Session.Get<bool>("authenticated");
            if (!authenticated && !string.IsNullOrWhiteSpace(token))
            {
                var accessToken = JsonConvert.DeserializeObject<AccessToken>(token);
                return await _identity.RefreshToken(accessToken);
            }
            return new AppUser(authenticated);
        }

        [HttpPost("SignIn")]
        public async Task<IAppUser> SignIn()
        {
            var authorization = HttpContext.Request.Headers["Authorization"];
            var jwtToken = authorization.ToString().Replace("Bearer ", "");
            return await _identity.AuthenticateToken(jwtToken);
        }

        [HttpPost("SignOut")]
        public ActionResult<bool> SignOut()
        {
            _identity.ClearAuthentication();
            return true;
        }
    }
}