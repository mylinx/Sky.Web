using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sky.Entity
{
    [Table("roles_routers")]
    public class Roles_routersEntity
    {

        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// RolesID
        /// </summary>
        public string RolesID
        {
            get;
            set;
        }
        /// <summary>
        /// RoutersID
        /// </summary>
        public string RoutersID
        {
            get;
            set;
        }

    }
}
