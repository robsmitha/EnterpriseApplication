using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Web.Common;

namespace Web.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Unique int PK of current user
        /// </summary>
        public Guid? ClaimID => Guid.TryParse(
            _httpContextAccessor?
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
            out var @int)
            ? @int
            : (Guid?)null;

        /// <summary>
        /// Unique string identifier of current user
        /// </summary>
        public string UniqueIdentifier => _httpContextAccessor?
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        /// <summary>
        /// Mobile phone of current user
        /// </summary>
        public string MobilePhone => _httpContextAccessor?
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;

        public IAccessToken GetAccessToken(string token)
        {
            var exp = _httpContextAccessor?
                     .HttpContext
                     .User
                     .Claims
                     .FirstOrDefault(x => x.Type == "exp")?.Value;
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiration = epoch.AddSeconds( Convert.ToDouble(exp));
            return new AccessToken
            {
                access_token = token,
                expires_at = expiration
            };
        }

        public void SetIdentity(IAccessToken accessToken = null)
        {
            if (_httpContextAccessor?.HttpContext == null) return;

            //remove previous if exists
            _httpContextAccessor
               .HttpContext
               .Response
               .Cookies.Delete("access_token");

            //set clear authenticated flag
            _httpContextAccessor
                .HttpContext
                .Session
                .Set("authenticated", false);

            if (accessToken != null)
            {
                //add or replace token
                _httpContextAccessor
                   .HttpContext
                   .Response
                   .Cookies.Append("access_token", JsonSerializer.Serialize(accessToken),
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = Convert.ToDateTime(accessToken.expires_at)
                    });

                //reset authenticated flag
                _httpContextAccessor
                    .HttpContext
                    .Session
                    .Set("authenticated", true);
            }
        }
    }
}
