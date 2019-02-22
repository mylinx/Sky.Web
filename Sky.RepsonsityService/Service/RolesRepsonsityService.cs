using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Configuration;
using Sky.Core;
using Sky.Entity;
using System.Linq;
using Sky.RepsonsityService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Sky.RepsonsityService.Service
{

    public class RolesRepsonsityService : ResponsitryBase<RolesEntity>, IRolesRepsonsityService
    {
        public RolesRepsonsityService() :base("default",DBType.MySql)
        {

        }



        public  IPagedList<RolesEntity> GetPagedList(Expression<Func<RolesEntity, bool>> predicate = null,
             Func<IQueryable<RolesEntity>, 
                IIncludableQueryable<RolesEntity, object>> include = null, 
            int pageIndex = 0, 
            int pageSize = 20, 
            bool disableTracking = true)
        {
            Func<IQueryable<RolesEntity>, IOrderedQueryable<RolesEntity>> orderBy = orde =>
                orde.OrderBy(x => x.ID).
                OrderByDescending(x => x.CreateDate); 

            return base.GetPagedList(predicate, orderBy, include, pageIndex, pageSize, disableTracking);
        }
    }
}
