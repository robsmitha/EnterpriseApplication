using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IAppUserService
    {
        /// <summary>
        /// unique ID of current authenticated app user
        /// </summary>
        Guid? ClaimID { get; }

        /// <summary>
        /// Email (Customer) or Username (User) of current authenticated app user
        /// </summary>
        string UniqueIdentifier { get; }

        /// <summary>
        /// Mobile phone of current authenticated app user
        /// </summary>
        string MobilePhone { get; }
        void SetIdentity(IAccessToken accessToken = null);
        public IAccessToken GetAccessToken(string token);
    }
}
