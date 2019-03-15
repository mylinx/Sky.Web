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
using Sky.Web.WebApi.ReturnViewModel;

namespace Sky.Web.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSystemController : ControllerBase
    {
        IUserRepsonsityService _userRepsonsityService;
        public UserSystemController(IUserRepsonsityService userRepsonsityService)
        {
            _userRepsonsityService = userRepsonsityService;
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
                int _pageSize = 20;
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

                IPagedList<UserEntity> pagedList = await _userRepsonsityService.GetPagedListAsync(expression, null, null, _pageIndex, _pageSize);

                result.verifiaction = true;
                result.message = "获取成功!";

                result.rows = pagedList;
            }
            catch (Exception ex)
            {
                result.message = ex.Message.ToString();
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
                     
                    result.verifiaction = true;
                    result.message = "删除成功!";
                }
                else
                {
                    result.message = "删除失败!";
                }
            }
            catch (Exception ex)
            {
                result.message = ex.Message.ToString();
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
        [Route("updateUsers")]
        [HttpPost]
        public JObject Update([FromBody]UserEntity _userEntity)
        {
            DataResult result = new DataResult
            {
                verifiaction = false
            };

            try
            {
                if (_userRepsonsityService.IsExists(x => x.UserName == _userEntity.UserName))
                {
                    result.message = "修改失败,已存在该账号!";
                    return JObject.FromObject(result);
                }

                UserEntity userEntity = new UserEntity
                {
                    ID = _userEntity.ID,
                    UserName = _userEntity.UserName,
                    PassWord = Encrypts.EncryptPassword(_userEntity.PassWord), 
                    Email = _userEntity.Email
                };

                int a=  _userRepsonsityService.Update(userEntity, new Expression<Func<UserEntity, object>>[]
                {
                     m=>m.UserName,
                     m=>m.PassWord,
                     m=>m.Email
                });

                result.verifiaction = true;
                result.message = "写入成功!";
            }
            catch (Exception ex)
            {
                result.message = ex.Message.ToString();
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

                    Email = _userEntity.Email,
                    IsLock = 0,
                    LoginLastDate = DateTime.Now.ToString(),
                    CreateDate = DateTime.Now.ToString(),
                    Remark = _userEntity.Remark
                };
                _userRepsonsityService.Insert(userEntity);

                result.verifiaction = true;
                result.message = "写入成功!";
            }
            catch (Exception ex)
            {
                result.message = ex.Message.ToString();
            }
            finally
            {

            }

            return JObject.FromObject(result);
        }

    }
}
