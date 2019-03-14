using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Sky.Entity;
using Sky.RepsonsityService.IService;
using Sky.Web.WebApi.PostViewModel;

namespace Sky.Web.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepsonsityService _userRepsonsityService;
        IRolesperssionRepsonsityService _rolesperssionRepsonsityService;
        IPerssionRepsonsityService _perssionRepsonsityService;
        public UsersController(
            IUserRepsonsityService userRepsonsityService,
            IRolesperssionRepsonsityService rolesperssionRepsonsityService,
            IPerssionRepsonsityService perssionRepsonsityService 
            )
        {
            _userRepsonsityService = userRepsonsityService;
            _rolesperssionRepsonsityService = rolesperssionRepsonsityService;
            _perssionRepsonsityService = perssionRepsonsityService;
        }

        [Route("GetPerAll")]
        [HttpGet]
        public string GetUserPersion()
        { 
            var ss = _perssionRepsonsityService.GetAll();

            return "成功!";
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetUsers")]
        [HttpGet]
        public JObject Get(  string pageIndex, string pageSize,string userName)
        {
            Expression<Func<UserEntity, bool>> predicate = null;
            int PageIndex = 1;
            int PageSize = 20;
            if (!string.IsNullOrEmpty(userName))
                predicate = pred => pred.UserName == userName.Trim();

            if (!string.IsNullOrEmpty(pageIndex))
                PageIndex = Convert.ToInt32(pageIndex);


            if (!string.IsNullOrEmpty(pageSize))
                PageSize = Convert.ToInt32(pageSize);

            
           IPagedList<UserEntity> userlist = _userRepsonsityService.GetPagedList(predicate, null, null, PageIndex, PageSize, false);
            
            return JObject.FromObject(userlist);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            //var list = _userRepsonsityService.FromSql<Post_UserViewModel>("select UsersName as name, password  from userentity");
            var ss = (from a in _userRepsonsityService.GetAllList()
                      select new
                      {
                          a.UserName,
                          a.PassWord
                      }).AsQueryable().ToPagedList(3,10);
            
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
