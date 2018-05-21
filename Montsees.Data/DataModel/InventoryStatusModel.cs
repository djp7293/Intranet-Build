using Monsees.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montsees.Data.DataModel
{
	public class InventoryStatusModel : Inventory
	{
		public virtual int InvStatusID { get; set; }
		public virtual string InvStatus { get; set; }
	}

    public class InventoryModel : Inventory
    {
        
    }
}
