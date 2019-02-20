using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sky.Entity
{
    [Table("NormalUserRole")]
    public class NormalUserRoleEntity
    { 
        public string ID { get; set; }
        public string NormalUserID { get; set; }
        public int NormalRoleID { get; set; }
        public DateTime ? CreateDate { get; set; }
    }
}
