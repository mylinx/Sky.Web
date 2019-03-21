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

namespace Sky.Web.WebApi.Controllers.PerssionControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerssionController : ControllerBase
    {
        readonly IPerssionRepsonsityService _perssionRepsonsityService;
       // readonly IPessiondetailRepsonsityService _pessiondetailRepsonsityService;
        readonly ICacheService _cacheService;
        public PerssionController(IPerssionRepsonsityService perssionRepsonsityService,
            IServiceProvider serviceProvider,
            IPessiondetailRepsonsityService pessiondetailRepsonsityService)
        {
            _perssionRepsonsityService = perssionRepsonsityService;
            _cacheService = serviceProvider.GetService<MemoryCacheService>();
           // _pessiondetailRepsonsityService = pessiondetailRepsonsityService;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        [Route("GetPersion")]
        [HttpGet]
        public string GetPessionList(string name)
        {
           Expression<Func<PerssionEntity,bool>> expression = null;
            if (!name.IsEmpty())
            {
                expression = exp => exp.Name == name;
             }

            //expression = exp => exp.ParentID == "0";

            List<PerssionEntity> list =  _perssionRepsonsityService.GetAllList();

            List<TreeChildViewModel> treeViewModels = new List<TreeChildViewModel>();
            // AddChild(list);
            treeViewModels= AddChildN("0");
            return  JsonConvert.SerializeObject(treeViewModels);
        }

        private List<TreeChildViewModel>  AddChildN(string Pid)
        {
            var data = _perssionRepsonsityService.GetAllList().Where(x => x.ParentID == Pid);
            List<TreeChildViewModel> list = new List<TreeChildViewModel>();
            foreach (var item in data)
            { 
                TreeChildViewModel childViewModel = new TreeChildViewModel();
                childViewModel.id = item.ID;
                childViewModel.component = item.Component;
                childViewModel.name = item.Name;
                childViewModel.meta_icon = item.Meta_icon;
                childViewModel.meta_title = item.Meta_title;
                childViewModel.meta_content = item.Meta_content; 
                childViewModel.treeChildren = GetChildList(childViewModel);
                list.Add(childViewModel);
            } 
            return list;
        }

        public List<TreeChildViewModel> GetChildList(TreeChildViewModel treeChildView)
        {
            if (!_perssionRepsonsityService.IsExists(x => x.ParentID == treeChildView.id))
            {
                return null;
            }
            else
            {
              return   AddChildN(treeChildView.id);
            }
        }


        private void AddChild(List<TreeChildViewModel> perssionlist,string Pid)
        {
            var data = _perssionRepsonsityService.GetAllList().Where(x => x.ParentID == Pid);
            if (data.Count() == 0)
                return;
            foreach (var item in data)
            {
                if (!_perssionRepsonsityService.IsExists(x => x.ParentID == item.ID))
                {
                    TreeChildViewModel childViewModel = new TreeChildViewModel();
                    childViewModel.id = item.ID;
                    childViewModel.component = item.Component;
                    childViewModel.name = item.Name;
                    childViewModel.meta_icon = item.Meta_icon;
                    childViewModel.meta_title = item.Meta_title;
                    childViewModel.meta_content = item.Meta_content;
                    perssionlist.Add(childViewModel);
                }
                else
                {
                    TreeChildViewModel childViewModel = new TreeChildViewModel();
                    childViewModel.id = item.ID;
                    childViewModel.component = item.Component;
                    childViewModel.name = item.Name;
                    childViewModel.meta_icon = item.Meta_icon;
                    childViewModel.meta_title = item.Meta_title;
                    childViewModel.meta_content = item.Meta_content;
                    AddChild(perssionlist, item.ID);
                } 
            }
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
                    string[] str = { "Add", "Del", "Update"};
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                dataResult.message = ex.Message.ToString();
            }
            return JObject.FromObject(dataResult); 
        }


    }
}