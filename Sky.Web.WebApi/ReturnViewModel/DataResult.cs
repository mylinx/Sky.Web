using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.Models
{
    public class DataResult
    {
        public bool Verifiaction { get; set; }
        public string Message { get; set; }
        public int? Statecode { get; set; }
        public object Rows
        {
            get;set;
        }
    }
}
