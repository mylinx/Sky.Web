﻿using Sky.Core;
using System;
using Sky.Entity;
using System.Linq;
using Sky.RepsonsityService.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;
using Sky.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Sky.RepsonsityService.Service
{
    public class NormalUserRepsonsityService : ResponsitryBase<NormalUserEntity>, INormalUserRepsonsityService
    {
        ResponsitryBase<UserEntity> responsitryBase;
        public NormalUserRepsonsityService()
        {
            responsitryBase = new ResponsitryBase<UserEntity>(DBType.MySql);
        }


        public string GetUserEntity()
        {
           var str = responsitryBase.FromSql<UserEntity>("SELECT * FROM userentity ");
            return "";
        }
    }
}
