using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.Models
{
    public class TokenAccess
    {
        public string AccessToken { get; set; }

        public string Expires { get; set; }
    }
}
