namespace User.Data.Model
{
	public class Part
	{
		//PK
		public int PartId { get; set; } 

		//FK
		public int WorkOrderId { get; set; }

		public string InventoryItemCode { get; set; }
		public string InventroyItemName { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal Total { get; set; }
		public string Notes { get; set; }
		public bool IsInstalled { get; set; }

		public virtual WorkOrder WorkOrder { get; set; }
	}
}