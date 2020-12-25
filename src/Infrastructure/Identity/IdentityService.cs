using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IApplicationDbContext _context;
        private readonly IAppUserService _user;
        private AppSettings _appSettings;

        public IdentityService(IOptions<AppSettings> appSettings,
            IAppUserService user,
            IApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _user = user;
        }

        public async Task<IAppUser> AuthenticateToken(string token)
        {
            var accessToken = _user.GetAccessToken(token);
            var authenticated = _user.ClaimID != null;
            _user.SetIdentity(accessToken);
            return await Task.FromResult(new AppUser(authenticated));
        }
        public async Task<IAppUser> RefreshToken(IAccessToken token)
        {
            //TODO: Call refresh
            return await Task.FromResult(new AppUser(true));
        }

        public void ClearAuthentication()
        {
            //TODO: Call logout
            _user.SetIdentity();
        }
    }
}
