using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sky.Common;
using Sky.Entity;
using Sky.RepsonsityService.IService;
using Sky.Web.WebApi.Jwt;
using Sky.Web.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.Controllers.RoutersControllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RoutersController : ControllerBase
    {
        readonly IRoutersRepsonsityService _routersRepsonsity;
        readonly IRoles_RoutersRepsonsityService _roles_RoutersRepsonsityService;
        readonly IJwtAuthorization _jwtAuthorization;
        public RoutersController(
              IRoutersRepsonsityService routersRepsonsity,
              IRoles_RoutersRepsonsityService roles_RoutersRepsonsityService,
              IJwtAuthorization jwtAuthorization
            )
        {
            _routersRepsonsity = routersRepsonsity;
            _roles_RoutersRepsonsityService = roles_RoutersRepsonsityService;
            _jwtAuthorization = jwtAuthorization;
        }


        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        [Route("GetPersion")]
        [HttpGet]
        public string GetRoutersList(string name,string rid)
        {
            DataResult result = new DataResult()
            {
                Verifiaction = false
            };
            Expression<Func<RoutersEntity, bool>> expression = null;
            if (!name.IsEmpty())
            {
                expression = exp => exp.Name == name;
            }
            expression = exp => exp.ParentID == "0";
            List<RoutersEntity> list = _routersRepsonsity.GetAllList(expression);
            List<TreeChildViewModel> treeViewModels = new List<TreeChildViewModel>();
             
            treeViewModels = AddChildN("0");
            if (treeViewModels.Count > 0)
            {
                result.Verifiaction = true;
                result.Rows = treeViewModels.OrderBy(x=>x.Sorts);
            }
            return JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// 父级菜单
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        private List<TreeChildViewModel> AddChildN(string pid)
        {
            var data = _routersRepsonsity.GetAllList().Where(x => x.ParentID == pid).OrderBy(x=>x.Sorts);
            List<TreeChildViewModel> list = new List<TreeChildViewModel>();


            List<Roles_routersEntity> roles_Routers = new List<Roles_routersEntity>();

            string ss = _jwtAuthorization.GetField("RolesID");

            roles_Routers = _roles_RoutersRepsonsityService.GetAllList(x => x.RolesID == _jwtAuthorization.GetField("RolesID")); 
            foreach (var item in data)
            {
                foreach (var router in roles_Routers)
                {
                    if (router.RoutersID == item.ID)
                    {
                        TreeChildViewModel childViewModel = new TreeChildViewModel
                        {
                            Id = item.ID,
                            PId = item.ParentID,
                            PathRouter = item.PathRouter,
                            Component = item.Component,
                            Name = item.Name,
                            Meta_icon = item.Meta_icon,
                            Meta_title = item.Meta_title,
                            Meta_content = item.Meta_content,
                            Sorts = item.Sorts
                        };
                        childViewModel.TreeChildren = GetChildList(childViewModel);
                        list.Add(childViewModel);
                        break;
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 子集菜单
        /// </summary>
        /// <param name="treeChildView"></param>
        /// <returns></returns>
        private List<TreeChildViewModel> GetChildList(TreeChildViewModel treeChildView)
        {
            if (!_routersRepsonsity.IsExists(x => x.ParentID == treeChildView.Id))
            {
                return null;
            }
            else
            {
                return AddChildN(treeChildView.Id);
            }
        }
         
    }
}
