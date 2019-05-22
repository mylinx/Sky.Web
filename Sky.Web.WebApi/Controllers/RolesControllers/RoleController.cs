using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Sky.Common;
using Sky.Entity;
using Sky.RepsonsityService.IService;
using Sky.Web.WebApi.Models;

namespace Sky.Web.WebApi.Controllers.RolesContrllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        readonly IRolesRepsonsityService _rolesRepsonsityService;
        public RoleController(IRolesRepsonsityService rolesRepsonsityService)
        {
            _rolesRepsonsityService = rolesRepsonsityService;
        }
        
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetRoleList")]
        [HttpGet]
        public async Task<JObject> GetRoleList(string name , string pageIndex, string pageSize)
        {
            DataResult result = new DataResult() { Verifiaction = false };
            try
            {
                int _pageIndex = 1;
                int _pageSize = 20;
                Expression<Func<RolesEntity, bool>> expression = null;

                if (!name.IsEmpty())
                    expression = exp => exp.RolesName.Contains(name.Trim());
                 
                if (!pageIndex.IsEmpty())
                {
                    _pageIndex = pageIndex.ToInt();
                }

                if (!pageSize.IsEmpty())
                {
                    _pageSize = pageSize.ToInt();
                }

                IPagedList<RolesEntity> pagedList = await _rolesRepsonsityService.GetPagedListAsync(expression, null, null, _pageIndex, _pageSize);

                result.Verifiaction = true;
                result.Message = "获取成功!";

                result.Rows = pagedList;
            }
            finally
            {

            }
            return JObject.FromObject(result);


        }



        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("deleteRole")]
        [HttpDelete]
        public JObject Del(string id)
        {
            DataResult result = new DataResult()
            {
                Verifiaction = false
            };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _rolesRepsonsityService.Delete(id.Trim());

                    result.Verifiaction = true;
                    result.Message = "删除成功!";
                }
                else
                {
                    result.Message = "删除失败!";
                }
            } 
            finally
            {

            }
            return JObject.FromObject(result);
        }


        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="_userEntity"></param>
        /// <returns></returns>
        [Route("updateRole")]
        [HttpPost]
        public JObject Update([FromBody]RolesEntity _roleEntity)
        {
            DataResult result = new DataResult
            {
                Verifiaction = false
            };

            try
            {
                if (_rolesRepsonsityService.IsExists(x => x.RolesName == _roleEntity.RolesName))
                {
                    result.Message = "因角色名称重复,修改失败!";
                    return JObject.FromObject(result);
                }

                RolesEntity userEntity = new RolesEntity
                {
                    ID = _roleEntity.ID,
                    RolesName = _roleEntity.RolesName,
                    UpdateDate = _roleEntity.UpdateDate.ToString()
                };

                int a = _rolesRepsonsityService.Update(userEntity, new Expression<Func<RolesEntity, object>>[]
                {
                     m=>m.RolesName,
                     m=>m.UpdateDate,
                     m=>m.Remark
                });
                if (a > 0)
                {
                    result.Verifiaction = true;
                    result.Message = "更新成功";
                }
                else
                {
                    result.Message = "修改失败,可能是数据不存在或已删除!";
                }
            } 
            finally
            {

            } 
            return JObject.FromObject(result);
        }



        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="_userEntity"></param>
        /// <returns></returns>
        [Route("AddRoles")]
        [HttpPost]
        public JObject AddRolesInfo([FromBody]RolesEntity _userRoleEntity)
        {
            DataResult result = new DataResult()
            {
                Verifiaction = false
            };
            try
            {
                if (_rolesRepsonsityService.IsExists(x => x.RolesName == _userRoleEntity.RolesName))
                {
                    result.Message = "角色名称已经存在!";
                    return JObject.FromObject(result);
                }


                RolesEntity userRoleEntity = new RolesEntity
                {
                    ID = Guid.NewGuid().ToString(),
                    RolesName = _userRoleEntity.RolesName,
                    CreateDate = DateTime.Now.ToString(),
                    Remark = _userRoleEntity.Remark
                };
                _rolesRepsonsityService.Insert(userRoleEntity);

                result.Verifiaction = true;
                result.Message = "写入成功!";
            } 
            finally
            {

            }

            return JObject.FromObject(result);
        }

    }
}