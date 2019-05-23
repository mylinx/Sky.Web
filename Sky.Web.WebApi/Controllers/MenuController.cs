using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sky.Entity;
using Sky.Web.WebApi.Models;
using Sky.RepsonsityService.IService; 
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Sky.Web.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
       readonly  IMenuRepsonsityService _menuRepsonsityService;
        readonly IMenuroleRepsonsityService _menuroleRepsonsityService;


        public MenuController(
            IMenuRepsonsityService menuRepsonsityService,
            IMenuroleRepsonsityService menuroleRepsonsityService
            )
        {
            this._menuRepsonsityService = menuRepsonsityService;
            this._menuroleRepsonsityService = menuroleRepsonsityService;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [Route("GetMenusByRoleID")]
        [HttpGet]
        public JObject GetMenusByRoleID(string roleid)
        {

            roleid = "76d71a4d-daa1-4b9e-885a-33123213213";
            DataResult result = new DataResult
            {
                Verifiaction = false
            };
            try
            {
                List<MenuEntity> menuEntitys = new List<MenuEntity>();

                //var entitylist=_menuRepsonsityService.GetAllList(x=>x.r)
                //var rolesmenus = _menuroleRepsonsityService.GetAllList(x => x.RoleID == roleid);
                var list = from rolesmenus in _menuroleRepsonsityService.GetAllList(x => x.RoleID == roleid)
                           join menu in _menuRepsonsityService.GetAllList().DefaultIfEmpty() on rolesmenus.MenuID equals menu.id
                           select new
                           {
                               ID=menu.id,
                               MenuName= menu.menuname,
                               Icon = menu.meta_icon,
                               ParentID = menu.parentid,
                               Router = menu.path
                           };
                result.Verifiaction = true;
                result.Rows = list;

            }
            finally
            {

            }
            return JObject.FromObject(result);
        } 
    }
}