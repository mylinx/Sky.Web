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

        public ConnetionDemoController(ICacheService cacheService)
        {
            _cacheService = cacheService;
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
