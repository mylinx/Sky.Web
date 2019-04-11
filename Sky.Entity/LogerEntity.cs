using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sky.Entity
{
    [Table("loger")]
    public class LogerEntity
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
        /// LogAction
        /// </summary>
        public string LogAction
        {
            get;
            set;
        }
        /// <summary>
        /// LogController
        /// </summary>
        public string LogController
        {
            get;
            set;
        }
        /// <summary>
        /// Loger
        /// </summary>
        public string Loger
        {
            get;
            set;
        }
        /// <summary>
        /// LogContents
        /// </summary>
        public string LogContents
        {
            get;
            set;
        }
        /// <summary>
        /// LogDate
        /// </summary>
        public string LogDate
        {
            get;
            set;
        }

    }
}
