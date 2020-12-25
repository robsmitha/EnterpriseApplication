using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        /// <summary>
        /// Clears token/session authentication values
        /// </summary>
        void ClearAuthentication();

        /// <summary>
        /// Set authentication values
        /// </summary>
        Task<IAppUser> AuthenticateToken(string token);

        /// <summary>
        /// Refreshes JWT token cookie when user visits site
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IAppUser> RefreshToken(IAccessToken token);
    }
}
