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
using System.Security.Claims;
namespace Sky.RepsonsityService.Service
{ 
	public class  MenuRepsonsityService : ResponsitryBase< MenuEntity>, IMenuRepsonsityService
	{
	  	 public  MenuRepsonsityService() :base("MySqlConnection", DBType.MySql)
	        {
	
	        }
	}
}