using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


//Nhibernate Code Generation Template 1.0
//author:MythXin
//blog:www.cnblogs.com/MythXin
//Entity Code Generation Template
namespace Sky.Entity
{
	[Table("menuroleperssion")]
	 	//menuroleperssion
		public class MenuroleperssionEntity
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
		/// MenuRoleID
        /// </summary>
        public  string MenuRoleID
        {
            get; 
            set; 
        }        
		/// <summary>
		/// PerssionID
        /// </summary>
        public  string PerssionID
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