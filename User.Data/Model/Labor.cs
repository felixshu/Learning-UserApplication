using System.Data.Entity.Migrations.Model;

namespace User.Data.Model
{
	public class Labor
	{
		public int LaborId { get; set; }
		public int WorkOrderId { get; set; }
		public string ServiceItemCode { get; set; }
		public string ServiceItemName { get; set; }
		public decimal LaborHours { get; set; }
		public decimal Rate { get; set; }
		public decimal Total { get; set; }
		public string Notes{ get; set; }


		public virtual WorkOrder WorkOrder { get; set; }
	}
}