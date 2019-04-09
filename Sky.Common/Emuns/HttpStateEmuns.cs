using System;
using System.Collections.Generic;
using System.Text;

namespace Sky.Common.Emuns
{
   public enum HttpStateEmuns
    {
        /// <summary>
        /// 请求失败
        /// </summary>
        Fail = 201,


        /// <summary>
        /// 请求成功
        /// </summary>
        Success = 200,


        /// <summary>
        /// 失效
        /// </summary>
        Invalid = 203,


        /// <summary>
        /// 不存在
        /// </summary>
        NotFound=404,

        /// <summary>
                 /// 服务器错误
                 /// </summary>
        ServceInvalid = 500,

        /// <summary>
        /// 危险请求
        /// </summary>
        Danger = 100

    }
}
