using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky.Web.WebApi.PostViewModel
{ 

    public class TreeChildViewModel
    {
        public string Id { get; set; }
        public string PId { get; set; }
        public string PathRouter { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public string Meta_title { get; set; }
        public string Meta_content { get; set; }
        public string Meta_icon { get; set; }
        public int ? Sorts { get; set; }
        public List<TreeChildViewModel> TreeChildren { get; set; }
    } 
}
