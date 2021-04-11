using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAccessToken
    {
        string token_type { get; set; }
        string access_token { get; set; }
        DateTime expires_at { get; set; }
    }
}
