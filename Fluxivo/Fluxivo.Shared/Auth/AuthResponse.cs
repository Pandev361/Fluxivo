using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxivo.Shared.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }

}
