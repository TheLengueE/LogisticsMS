using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogisticsMS.Models
{
	[Table("ReimbursementForm")]
	public class ReimbursementForm
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("ContainerCargo")]
		[Column("送货单id")]
		public Guid ContainerCargoId { get; set; }

		public ContainerCargo? ContainerCargo { get; set; }

		[Column("申报人姓名")]
		public string Name { get; set; } = string.Empty;


		[Column("金额")]
		public string Amount { get; set; } = string.Empty;

		[DataType(DataType.Date)]
		[Column("申请时间")]
		public DateTime Date { get; set; }

		[Column("事由")]
		public string Reason { get; set; } = string.Empty;

		[Column("审查人名字")]
		public string ApproverName { get; set; } = string.Empty;

		[Column("状态")]  //1未审批,2,通过审批,3未通过审批
		public int stage { get; set; } = 1;
	}
}
