using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.ReturnViewModel
{
    public class DataResult
    {
        public bool verifiaction { get; set; }
        public string message { get; set; }

        public object rows
        {
            get;set;
        }
    }
}
