using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sky.Entity
{
    [Table("NormalRole")]
    public class NormalRoleEntity
    {
        public int ID { get; set; }
        public string ParentID { get; set; }
        public string Name { get; set; }
        public DateTime ? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Remark { get; set; }
    }
}
