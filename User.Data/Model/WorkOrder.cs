using System;

namespace User.Data.Model
{
	public class WorkOrder
	{
		public int WorkOrderId { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDateTime { get; set; }
		public DateTime? TargetDateTime { get; set; }
		public DateTime? DropDeadDateTime { get; set; }
		public string Description { get; set; }
		public WorkOrderStatus WorkOrderStatus { get; set; }
		public decimal TotalDue { get; set; }
		public string CertificationRequirement { get; set; }

		public virtual Customer Customer { get; set; }
	}
}