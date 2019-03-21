using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.PostViewModel
{
    public class TreeViewModel
    {
        public string id { get; set; }
        public string pathRouter { get; set; }
        public string component { get; set; }
        public string meta_title { get; set; }
        public string meta_content { get; set; }
        public string meta_icon { get; set; }
    }

    public class TreeChildViewModel
    {
        public string id { get; set; }
        public string pathRouter { get; set; }
        public string name { get; set; }
        public string component { get; set; }
        public string meta_title { get; set; }
        public string meta_content { get; set; }
        public string meta_icon { get; set; }
        public List<TreeChildViewModel> treeChildren { get; set; }
    }
}
