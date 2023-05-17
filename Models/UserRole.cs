using System.ComponentModel.DataAnnotations;

namespace LogisticsMS.Models
{
	public class UserRole
	{
		public UserRole()
		{
			Name = "待定义";
			Email = "待定义";
			Phone = "待定义";
			RoleName = "待定义";
		}

		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string RoleName { get; set; }
	}
}
