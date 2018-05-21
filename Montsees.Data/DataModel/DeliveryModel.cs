using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montsees.Data.DataModel
{
	public class DeliveryModel
	{
		public int DeliveryItemID {get;set;} 
		public DateTime CurrDelivery {get;set;}  
		public int Quantity {get;set;} 
		public string PONumber {get;set;} 
		public bool Suspend {get;set;} 
        public bool Shipped { get; set; }
		public bool ReadyToShip {get;set;}  
		public DateTime? ShipDate {get;set;} 
	}
}
