using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsMS.Models
{
	public class ContainerCargo
	{
		[Key]
		public Guid Id { get; set; }

		[Column("发货点")]
		public string Start { get; set; } = "待定义";         //发货点
		[Column("收货点")]
		public string End { get; set; } = "待定义";          //收货点
		[Column("发货城市")]
		public string StartCity { get; set; } = "待定义";  //发货城市
		[Column("收货城市")]
		public string EndCity { get; set; } = "待定义";   //收货城市

		[Column("托运人名字")]
		public string ShippersName { get; set; } = "待定义"; //托运人名字
		[Column("托运人电话")]
		public string ShipperPhone { get; set; } = "待定义"; //托运人电话
		[Column("托运人地址")]
		public string ShipperAddress { get; set; } = "待定义"; //托运人地址
		[Column("托运人邮箱")]
		public string ShipperEmail { get; set; } = "待定义";  //托运人邮箱

		[Column("收货人名字")]
		public string ConsigneeName { get; set; } = "待定义"; //收货人名字
		[Column("收货人电话")]
		public string ConsigneePhone { get; set; } = "待定义"; //收货电话
		[Column("收货人地址")]
		public string ConsigneeAddress { get; set; } = "待定义"; //收货人地址
		[Column("收货人邮箱")]
		public string ConsigneeEmail { get; set; } = "待定义";  //收货人邮箱

		[Column("运货名称")]
		public string ShipmentName { get; set; } = "待定义";  //货物名称
		[Column("集装箱类型")]
		public string ContainersType { get; set; } = "待定义";  //集装箱类型
		[Column("集装箱数量")]
		public string ContainersMany { get; set; } = "待定义"; //集装箱数量
		[Column("集装箱号码")]
		public string ContainersNumber { get; set; } = "待定义"; //集装箱号码
		[Column("施封号码")]
		public string ContainersClsoeNumber { get; set; } = "待定义"; //施封号码
		[Column("运输费用")]
		public string ContainersCost { get; set; } = "待定义";     //运输费用

		[Column("托用人记载")]
		public string? ShippersRemark { get; set; }         //托运人记载
		[Column("其它附件")]
		public byte[]? OtherFile { get; set; }                     //其它附件
		[Column("货物价格")]
		public string CargoPrices { get; set; } = "待定义";               //货物价格
		[Column("运输员备注")]
		public string? TransportersRemark { get; set; }  //运输员备注
	}
}
