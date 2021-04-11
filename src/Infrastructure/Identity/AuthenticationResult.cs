using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AuthenticationResult : IAuthenticationResult
    {
        public AuthenticationResult()
        {

        }
        public AuthenticationResult(bool auth)
        {
            authenticated = auth;
        }
        public bool authenticated { get; set; }
    }
}
