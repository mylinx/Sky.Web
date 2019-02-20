using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.ViewModel
{
    public class NormalUserRolesViewModel
    {
        //用户主键ID
        public string ID { get; set; }

        //用户名
        public string Name { get; set; }

        //角色名称
        public string RoleName { get; set; }

        //角色权限
        public string RoleMenuID { get; set; }
        
    }
}
