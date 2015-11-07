using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;

namespace User.Data.Model
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int? ParentId { get; set; }

		public virtual Category Category1 { get; set; }
		public virtual ICollection<Category> CategoryChidren { get; set; }

		public virtual ICollection<InventoryItem> InventoryItems { get; set; }
	}
}