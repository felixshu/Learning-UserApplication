using System.ComponentModel;

namespace User.Data.Model
{
	public class InventoryItem
	{
		public int InventoryItemId { get; set; }
		public string InventoryItemCode { get; set; }
		public string InventoryItemName { get; set; }
		public decimal UnitPrice { get; set; }

		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
	}
}