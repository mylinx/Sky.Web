using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sky.Entity;
using Sky.RepsonsityService.IService;
using Sky.Common;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Sky.Web.WebApi.Controllers
{
    /// <summary>
    /// 这是展示切换数据库链接的控制器
    /// </summary> 
    [Route("api/[controller]")]
    [ApiController]
    public class ConnetionDemoController : ControllerBase
    {
        protected ICacheService _cacheService;
        protected IRolesRepsonsityService _rolesRepsonsityService;
        public ConnetionDemoController(ICacheService cacheService,
            IRolesRepsonsityService rolesRepsonsityService
            )
        {
            _cacheService = cacheService;
            _rolesRepsonsityService = rolesRepsonsityService;
        }

        [Route("rolesinsert")]
        [HttpGet]
        public string RolesTest()
        {
            try
            {
                RolesEntity rolesEntity = new RolesEntity();
                rolesEntity.ID = Guid.NewGuid().ToString();
                rolesEntity.RolesName = "张三";
                rolesEntity.UpdateDate = DateTime.Now.ToString();
                _rolesRepsonsityService.Insert(rolesEntity);
                _rolesRepsonsityService.SaveChange();
            }
            catch (Exception ex)
            {
                 
            }
           
            return "成功";
        }


        [Route("rolesUpdate")]
        [HttpGet]
        public string RolesTest1()
        {
            try
            {
                RolesEntity rolesEntity = new RolesEntity();
                rolesEntity.ID = "76d71a4d-daa1-4b9e-885a-be789579a4e5";
                rolesEntity.RolesName = "李四";
                rolesEntity.CreateDate = DateTime.Now.ToString();
                _rolesRepsonsityService.Update(rolesEntity);
                _rolesRepsonsityService.SaveChange();
            }
            catch (Exception ex)
            {

            }

            return "成功";
        }


        [Route("rolesDel")]
        [HttpGet]
        public string RolesTest1(string id)
        {
            try
            {
                RolesEntity rolesEntity = new RolesEntity();
                _rolesRepsonsityService.Delete(id);
                _rolesRepsonsityService.SaveChange();
            }
            catch (Exception ex)
            {

            }

            return "成功";
        }




        [Route("rolesGetByID")]
        [HttpGet]
        public string GetModel(string id)
        {
            try
            {
                RolesEntity rolesEntity = new RolesEntity();
                rolesEntity= _rolesRepsonsityService.GetById(id);
                return JsonConvert.SerializeObject(rolesEntity);
            }
            catch (Exception ex)
            {

            }

            return "成功";
        }



        [Route("rolesGetAll")]
        [HttpGet]
        public string GetAll()
        {
            try
            {
                IPagedList<RolesEntity> list = _rolesRepsonsityService.GetPagedList(null,null,null, 1, 20, false);
                var list1 = _rolesRepsonsityService.FromSql<UserEntity>("select * from userentity");
                return JsonConvert.SerializeObject(list);
            }
            catch (Exception ex)
            {

            }
            return "成功";
        }



        [Authorize]
        [Route("getnull")]
        [HttpPost]
        public string Get()
        {
            return "value";
        }

        [Route("getbyid")]
        [HttpGet]
        public object Get(string id)
        {
            _cacheService.Add("a", "1");
            _cacheService.Add("b", "2");

            //Console.Write(_cacheService.Get("a"));
            //Console.Write(_cacheService.Get("b"));

            return _cacheService.Get("a");
        }


        [Route("getby")]
        [HttpGet]
        public object Get1(string id)
        {
            //_cacheService.Add("a", "1");
            //_cacheService.Add("b", "2");

            //Console.Write(_cacheService.Get("a"));
            //Console.Write(_cacheService.Get("b"));

            return _cacheService.Get(id);
        }


        [Route("remove")]
        [HttpGet]
        public object RemoveCache(string id)
        {
            //_cacheService.Add("a", "1");
            //_cacheService.Add("b", "2");

            //Console.Write(_cacheService.Get("a"));
            //Console.Write(_cacheService.Get("b"));
            _cacheService.Remove(id);
            return _cacheService.Get(id);
        }
    }
}
