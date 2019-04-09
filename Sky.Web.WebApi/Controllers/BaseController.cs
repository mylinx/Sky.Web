using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sky.Web.WebApi.Jwt;

namespace Sky.Web.WebApi.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        IJwtAuthorization _jwtAuthorization;
        public BaseController(IJwtAuthorization jwtAuthorization)
        {
            this._jwtAuthorization = jwtAuthorization;
        }

        public override AcceptedResult Accepted()
        {

            return base.Accepted();
        }
    }
}
