using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Route("deleteUser")]
        [HttpPost]
        public JObject Del(string id)
        {
            DataResult result = new DataResult();
            result.verifiaction = false;
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
            DataResult result = new DataResult();
            result.verifiaction = false;

            try
            {
                if (_userRepsonsityService.IsExists(x => x.UserName == _userEntity.UserName))
                {
                    result.message = "修改失败,已存在该账号!";
                    return JObject.FromObject(result);
                }

                UserEntity userEntity = new UserEntity();
                userEntity.ID = _userEntity.ID;
                userEntity.UserName = _userEntity.UserName;
                userEntity.PassWord = Encrypts.EncryptPassword(_userEntity.PassWord);

                userEntity.Email = _userEntity.Email;

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
        /// 注册账号
        /// </summary>
        /// <param name="_userEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public JObject Register([FromBody]UserEntity _userEntity)
        {
            DataResult result = new DataResult();
            result.verifiaction = false;
            try
            {
                if (_userRepsonsityService.IsExists(x => x.UserName == _userEntity.UserName))
                {
                    result.message = "已存在该账号!";
                    return JObject.FromObject(result);
                }

                UserEntity userEntity = new UserEntity();
                userEntity.ID = Guid.NewGuid().ToString();
                userEntity.UserName = _userEntity.UserName;
                userEntity.PassWord = Encrypts.EncryptPassword(_userEntity.PassWord);

                userEntity.Email = _userEntity.Email;
                userEntity.IsLock = 0;
                userEntity.LoginLastDate = DateTime.Now.ToString();
                userEntity.CreateDate = DateTime.Now.ToString();
                userEntity.Remark = _userEntity.Remark;
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
