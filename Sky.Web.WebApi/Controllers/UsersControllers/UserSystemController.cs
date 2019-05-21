using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Sky.Common;
using Sky.Entity;
using Sky.RepsonsityService.IService;
using Sky.Web.WebApi.ReturnViewModel;
using System.Net;

namespace Sky.Web.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserSystemController : ControllerBase
    {
        IUserRepsonsityService _userRepsonsityService;
        IRolesRepsonsityService _rolesRepsonsityService;
        public UserSystemController(IUserRepsonsityService userRepsonsityService,
            IRolesRepsonsityService rolesRepsonsityService)
        {
            _userRepsonsityService = userRepsonsityService;
            _rolesRepsonsityService = rolesRepsonsityService;
        }
         

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="rolesID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetUserList")]
        [HttpGet]
        public async Task<JObject> GetUserList(string userName,string rolesID,string pageIndex,string pageSize)
        {
            DataResult result = new DataResult() { verifiaction=false };
            try
            {
                int _pageIndex = 1;
                int _pageSize = 10;
                Expression<Func<UserEntity, bool>> expression = null;
                //if (!string.IsNullOrEmpty(userName))
                if (!userName.IsEmpty())
                    expression = exp => exp.UserName.Contains(userName.Trim());

                if (!rolesID.IsEmpty())
                    expression = exp => exp.RoleID == rolesID;

                if (!pageIndex.IsEmpty())
                {
                    _pageIndex = pageIndex.ToInt();
                }

                if (!pageSize.IsEmpty())
                {
                    _pageSize = pageSize.ToInt();
                }

                IPagedList<UserEntity> pagedList = await _userRepsonsityService.GetPagedListAsync(expression, x=>x.OrderByDescending(p=>p.CreateDate), null, _pageIndex, _pageSize);
                
                result.verifiaction = true;
                result.message = "获取成功!";
                result.rows = new
                {
                     total=pagedList.TotalCount,
                     pageindex= pagedList.PageIndex,
                     pagesize=pagedList.PageSize,
                     items=(from p in  pagedList.Items select new
                     {
                         id=p.ID,
                         name=p.UserName,
                         rolename=GetRolesNameByID(p.RoleID),
                         email=p.Email,
                         remark=p.Remark,
                         createdate=p.CreateDate
                     })
                };
            }
            finally
            {

            }
            return JObject.FromObject(result);
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("deleteUser")]
        [HttpDelete]
        public JObject Del(string id)
        {
            DataResult result = new DataResult()
            {
                verifiaction = false
            };
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _userRepsonsityService.Delete(id.Trim());
                    _userRepsonsityService.SaveChange();
                    result.verifiaction = true;
                    result.message = "删除成功!";
                }
                else
                {
                    result.message = "删除失败,id参数不能为空!";
                }
            }
            finally
            {

            }
            return JObject.FromObject(result);
        }
        

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="_userEntity"></param>
        /// <returns></returns>
        [Route("updateUser")]
        [HttpPost]
        public JObject Update([FromBody]UserEntity _userEntity)
        {
            DataResult result = new DataResult
            {
                verifiaction = false
            };

            try
            {
                //if (_userRepsonsityService.IsExists(x => x.UserName == _userEntity.UserName))
                //{
                //    result.message = "修改失败,已存在该账号!";
                //    return JObject.FromObject(result);
                //}
                 
                UserEntity userEntity= _userRepsonsityService.Find(_userEntity.ID);
                if (userEntity != null)
                {
                    userEntity.ID = _userEntity.ID;
                    userEntity.RoleID = _userEntity.RoleID;
                    //UserName = _userEntity.UserName,
                    if (!string.IsNullOrEmpty(_userEntity.PassWord))
                    {
                        userEntity.PassWord = Encrypts.EncryptPassword(_userEntity.PassWord);
                    } 
                    userEntity.Email = _userEntity.Email;
                    userEntity.Remark = _userEntity.Remark;
                }



                //UserEntity userEntity = new UserEntity
                //{
                //    ID = _userEntity.ID,
                //    RoleID=_userEntity.RoleID,
                //    //UserName = _userEntity.UserName,
                //    PassWord = Encrypts.EncryptPassword(_userEntity.PassWord), 
                //    Email = _userEntity.Email,
                //    Remark=_userEntity.Remark
                //};

                int a=  _userRepsonsityService.Update(userEntity, new Expression<Func<UserEntity, object>>[]
                {
                     //m=>m.UserName,
                     m=>m.RoleID,
                     m=>m.PassWord,
                     m=>m.Email,
                     m=>m.Remark
                });
                _userRepsonsityService.SaveChange();
                result.verifiaction = true;
                result.message = "写入成功!";
            }
            finally
            {

            }


            return JObject.FromObject(result);
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [Route("detailsUser")]
        public JObject Details(string id)
        {
            DataResult result = new DataResult()
            {
                verifiaction = false,
                statecode=(int)HttpStatusCode.BadRequest
            };
            try
            {
                if(string.IsNullOrEmpty(id))
                    return JObject.FromObject(result);


                UserEntity entity = _userRepsonsityService.Find(id);
                if (entity != null)
                {
                    result.verifiaction = true;
                    result.statecode = (int)HttpStatusCode.OK;
                    result.rows = new
                    {
                        id=entity.ID,
                        username=entity.UserName,
                        roleid=entity.RoleID, 
                        email=entity.Email
                    };
                }
            }
            finally
            {
                
            }
            return JObject.FromObject(result);
        }

        /// <summary>
        /// 新增账号
        /// </summary>
        /// <param name="_userEntity"></param>
        /// <returns></returns>
        [Route("addUser")]
        [HttpPost]
        public JObject Register([FromBody]UserEntity _userEntity)
        {
            DataResult result = new DataResult()
            {
                verifiaction = false
            };
            try
            {
                if (_userRepsonsityService.IsExists(x => x.UserName == _userEntity.UserName))
                {
                    result.message = "已存在该账号!";
                    return JObject.FromObject(result);
                }

                UserEntity userEntity = new UserEntity
                {
                    ID = Guid.NewGuid().ToString(),
                    UserName = _userEntity.UserName,
                    PassWord = Encrypts.EncryptPassword(_userEntity.PassWord),
                    RoleID = _userEntity.RoleID,
                    Email = _userEntity.Email,
                    IsLock = 0,
                    LoginLastDate = DateTime.Now.ToString(),
                    CreateDate = DateTime.Now.ToString(),
                    Remark = _userEntity.Remark
                };
                _userRepsonsityService.Insert(userEntity);
                _userRepsonsityService.SaveChange();
                result.verifiaction = true;
                result.message = "写入成功!";
            }
            finally
            {

            } 
            return JObject.FromObject(result);
        }


        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [Route("getroles")]
        [HttpGet]
        public JObject GetRoles()
        { 
            DataResult result = new DataResult()
            {
                verifiaction = false,
                statecode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                List<RolesEntity> list = _rolesRepsonsityService.GetAllList();
                if (list.Count > 0)
                {
                    
                    result.verifiaction = true;
                    result.rows = from roles in list
                                  select new
                                  {
                                      id=roles.ID,
                                      name=roles.RolesName
                                  };
                    result.statecode = (int)HttpStatusCode.OK;
                }
                return JObject.FromObject(result);
            }
            finally
            {
                
            }
        }



        /// <summary>
        /// 获取角色ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private  string GetRolesNameByID(string ID)
        {
            try
            {
                if (string.IsNullOrEmpty(ID))
                    return "";
                RolesEntity rolesEntity = _rolesRepsonsityService.Find(ID);
                if (rolesEntity != null)
                    return rolesEntity.RolesName;
            }
            finally
            {

            }
            return "";
        }
    }
}
