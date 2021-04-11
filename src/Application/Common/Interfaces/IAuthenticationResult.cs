using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationResult
    {
        /// <summary>
        /// Indicate client's jwt token httpOnlyCookie is valid if exists
        /// </summary>
        public bool authenticated { get; set; }
    }
}
