using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsMS.Models
{
	public class Statistics
	{
		//业务单的Id
		public Guid ContainerCargoId { get; set; }

		//送货人名字
		public string DeliveryPerson { get; set; } = string.Empty;

		//业务员名字
		public string ShippersName { get; set; } = string.Empty;

		//货物名称
		public string ShipmentName { get; set; } = string.Empty;  //货物名称

		//送货时间
		public DateTime DeliveryDate { get; set; }

		//收益
		public string ContainersCost { get; set; } = string.Empty;

		//报销总费用
		public string AllCost { get; set; } = string.Empty;

		//最终利润
		public string Profit { get; set; } = string.Empty;
	}
}
