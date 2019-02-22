using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Configuration;
using Sky.Core;
using Sky.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Sky.RepsonsityService.IService
{

    public interface IRolesRepsonsityService : IResponsitryBase<RolesEntity>
    {
         IPagedList<RolesEntity> GetPagedList(Expression<Func<RolesEntity, bool>> predicate = null,
             Func<IQueryable<RolesEntity>,
                IIncludableQueryable<RolesEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true);
    }
}
