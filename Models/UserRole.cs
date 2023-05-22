using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsMS.Models
{
	public class UserRole
	{
		[Key]
		public int Id { get; set; }
		[Column("账号")]
		public string account { get; set; } = "待定义";
		[Column("密码")]
		public string password { get; set; } = "待定义";
		[Column("姓名")]
		public string Name { get; set; } = "待定义";
		[Column("电话号码")]
		public string Phone { get; set; } = "待定义";
		[Column("邮箱")]
		public string Email { get; set; } = "待定义";
		[Column("用户角色")]
		public string RoleName { get; set; } = "待定义";
	}
}
