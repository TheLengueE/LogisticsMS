using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsMS.Models
{
	public class ShippingOrders
	{
		[Key]
		public Guid Id { get; set; }

		[ForeignKey("ContainerCargo")]
		[Column("送货单id")]
		public Guid ContainerCargoId { get; set; }

		public ContainerCargo? ContainerCargo { get; set; }

		[Column("车牌号")]
		public string CarNumber { get; set; } = string.Empty;

		[Column("汽车类型")]
		public string CarType { get; set; } = string.Empty;

		[Column("送货人名字")]
		public string DeliveryPerson { get; set; } = string.Empty;

		[Column("送货人Id")]
		public int UserRoleId { get; set; }

		[Column("送货时间")]
		public DateTime DeliveryDate { get; set; }

		[Column("送货人的电话号码")]
		public string DeliveryPhone { get; set; } = string.Empty;

		[Column("订单状态")] //1:未指定派送司机  2:指定了派送司机单订单未完成   3,订单已完成
		public int state { get; set; }
	}
}
