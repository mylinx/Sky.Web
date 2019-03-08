using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.Filters
{
    public class ApiAuthorzationMiddle
    {
        private readonly RequestDelegate _next;
        public ApiAuthorzationMiddle(RequestDelegate next)
        {
            _next = next;
        }



        public async Task Invoke(HttpContext context)
        {
            int statusCode = 200;
            string msg = "";
            try
            {

            }
            catch (Exception ex)
            {
                statusCode = context.Response.StatusCode;
                if (ex is ArgumentException)
                {
                    statusCode = 200;
                }
                await HandleExceptionAsync(context, statusCode, ex.Message);
            }
            finally
            {
                

                await HandleExceptionAsync(context, statusCode, msg);
            } 
        }


        private Task HandleExceptionAsync(HttpContext context,int statusCode, string msg)
        {  
            var data = new
            {
                code = "1",
                is_success = true,
                msg = ""
            };
            var result = JsonConvert.SerializeObject(new { data = data });
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }
}
