using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Sky.Entity
{
	[Table("menurole")]
	 	//menurole
		public class MenuroleEntity
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
		/// RoleID
        /// </summary>
        public  string RoleID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// MenuID
        /// </summary>
        public  string MenuID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// CreateTime
        /// </summary>
        public  string CreateTime
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