using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sky.Entity;
using Sky.Common; 
using Newtonsoft.Json.Linq;
using Sky.RepsonsityService.IService;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Sky.Web.WebApi.ReturnViewModel;
using System.Linq.Expressions;
using Sky.Web.WebApi.PostViewModel;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Sky.Web.WebApi.Controllers.PerssionControllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PerssionController : ControllerBase
    {
        readonly IPerssionRepsonsityService _perssionRepsonsityService;
        readonly IRoles_RoutersRepsonsityService _roles_RoutersRepsonsity;
      
        readonly ICacheService _cacheService;
        public PerssionController(IPerssionRepsonsityService perssionRepsonsityService,
            IServiceProvider serviceProvider,
            IPessiondetailRepsonsityService pessiondetailRepsonsityService,
             IRoles_RoutersRepsonsityService roles_RoutersRepsonsit
           
            )
        {
            _perssionRepsonsityService = perssionRepsonsityService;
            _cacheService = serviceProvider.GetService<MemoryCacheService>();
            _roles_RoutersRepsonsity = roles_RoutersRepsonsit;
        }

       
 

        /// <summary>
        /// 新增/更新权限
        /// </summary>
        /// <param name="perssionEntity">实体对象</param>
        /// <param name="IsEnable">是否默认创建(增删改查按钮权限)</param>
        /// <returns></returns>

        public async Task<JObject> AddOrUpdataPerssion(PerssionEntity perssionEntity, int IsEnable)
        {
            DataResult dataResult = new DataResult()
            {
                verifiaction = false
            };

            try
            {

                //如果ID为空,则默认为新增
                if (perssionEntity.ID.IsEmpty())
                {
                    perssionEntity.ID = Guid.NewGuid().ToString();
                    await _perssionRepsonsityService.InsertAsync(perssionEntity);
                }
                else
                {
                    //查询是否存在数据,存在则更新,不存在则新增
                    if (!_perssionRepsonsityService.IsExists(x => x.ID == perssionEntity.ID))
                    {
                        dataResult.message = "删除失败,可能该数据被删除!";
                        return JObject.FromObject(dataResult);
                    }
                    else
                    {
                        _perssionRepsonsityService.Update(perssionEntity);
                    }
                }

                if (IsEnable.ToBool())//如果为True默认创建按钮权限.
                {
                    PessiondetailEntity entity = new PessiondetailEntity();
                    string[] str = { "Add", "Del", "Update" };
                    for (int i = 0; i < 4; i++)
                    {
                        entity.ID = Guid.NewGuid().ToString();
                        entity.PerssionID = perssionEntity.ID;
                        entity.BtName = str[i];
                        entity.CreateDate = DateTime.Now.ToString();
                        // await _pessiondetailRepsonsityService.InsertAsync(entity);
                    }
                }

                dataResult.verifiaction = true;
                dataResult.message = "新增成功!";
            }
            finally
            {
            }
            return JObject.FromObject(dataResult);
        }


        /// <summary>
        /// 删除权限
        /// </summary>
        /// <returns></returns>
        [Route("DeletePerssion")]
        [HttpDelete]
        public JObject DeletePersionAnsy(string ID)
        {
            DataResult dataResult = new DataResult()
            {
                verifiaction = false
            };

            try
            {
                if (!ID.IsEmpty())
                {
                    _perssionRepsonsityService.Delete(ID.Trim());

                    dataResult.verifiaction = true;
                    dataResult.message = "删除成功!";
                }
                else
                {
                    dataResult.message = "删除失败!ID不能为空";
                }
            }
            finally
            {

            }
            return JObject.FromObject(dataResult); 
        }


    }
}