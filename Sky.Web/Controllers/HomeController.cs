using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sky.Core;
using Sky.Entity;
using Sky.RepsonsityService;
using Sky.RepsonsityService.IService;
using Sky.Web.Models;
using Sky.Web.ViewModels;

namespace Sky.Web.Controllers
{
    /// <summary>
    /// 日志记录/ 数据库链接切换/DbSet动态注入/事务/路由
    /// </summary>
    public class HomeController : Controller
    {
        //IUsersRepsonsityService usersRepsonsityService;
        public HomeController()
        {
            //usersRepsonsityService = _usersRepsonsityService;
        }

        public IActionResult Index()
        {
            //var ss= usersRepsonsityService.GetById("123123");

            //var uid = "123123";
            
            ////SQL 尽量使用此方法$,这种是防止SQL 注入;
            //var ss12 = usersRepsonsityService.FromSql<UserEntityVModel>("exec sp_GetUsers");
            //foreach (var item in ss12)
            //{
            //    string s1 = item.ID;
            //    string s2 = item.UserName;
            //}
            //string ss1 = "";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
