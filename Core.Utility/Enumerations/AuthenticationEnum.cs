using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMMS.Utility.Enumerations
{
    public class AuthenticationEnum
    {
        public enum AuthType
        {
            ApiKey = 1,
            Basic = 2,
            OAuth2 = 3,
            Jwt = 4
        }
    }
}
