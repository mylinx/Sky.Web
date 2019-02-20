using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Configuration;
using Sky.Core;
using Sky.Entity;
using System.Linq;

namespace Sky.RepsonsityService.IService
{
   public interface IUserRepsonsityService : IResponsitryBase<UserEntity>
    {
        bool Register(UserEntity userEntity);
        UserEntity Login(string username, string password);
    }
}
