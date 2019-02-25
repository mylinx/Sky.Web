using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Sky.Entity
{
	[Table("rolesperssion")]
	 	//rolesperssion
		public class RolesperssionEntity
	{
	
      	/// <summary>
		/// ID
        /// </summary>
        public  string ID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// RolesID
        /// </summary>
        public  string RolesID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// PeID
        /// </summary>
        public  int? PeID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreateDate
        /// </summary>
        public  string CreateDate
        {
            get; 
            set; 
        }        
		/// <summary>
		/// Remark
        /// </summary>
        public  string Remark
        {
            get; 
            set; 
        }        
		   
	}
}