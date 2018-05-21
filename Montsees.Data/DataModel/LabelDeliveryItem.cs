using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montsees.Data.DataModel
{
	public class LabelDeliveryItem
	{
		public int LotNumber { get; set; }
		public int SumOfQuantity { get; set; }
		public DateTime CurrDelivery { get; set; }
		public string PONumber { get; set; }
		public bool Shipped { get; set; }
	}
}
