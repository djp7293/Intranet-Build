using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montsees.Data.DataModel
{
	public class MaterialModel
	{
		public int MaterialID {get;set;} 
		public string MaterialName {get;set;}
        public string Type { get; set; }
        public string Material { get; set; }
        public string Description { get; set; } 

	}

    public class MaterialSizeModel
    {
        public int MaterialSizeID { get; set; }
        public int MaterialDimID { get; set; }
        public int Diameter { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Size { get; set; }
        

    }

    public class GaugeTypeModel
    {
        public int GageTypeID { get; set; }
        public string Description { get; set; }
    }

    public class ContactModel
    {
        public int ContactID { get; set; }               
        public string ContactName { get; set; }

    }

    public class InvStatusModel
    {
        public int InvStatusID { get; set; }
        public string Status { get; set; }
    }

    public class VendorListModel
    {
        public int SubcontractID { get; set; }
        public string VendorName { get; set; }
    }

    public class EmployeeModel
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
    }

    public class MachineModel
    {
        public int MachineID { get; set; }
        public string Machine { get; set; }
        public int OperationID { get; set; }
    }

    public class DecommissionModel
    {
        public int DecommCodeID { get; set; }
        public string DecommissionCode { get; set; }
    }

    public class DimensionModel
    {
        public int MaterialDimID { get; set; }        
        public string Dimension { get; set; }
    }

    public class WorkcodeModel
    {
        public int WorkcodeID { get; set; }
        public string Workcode { get; set; }

    }

    public class SetupListModel
    {
        public int JobSetupID { get; set; }
        public string OperationName { get; set; }

    }

    public class OperationListModel
    {
        public int OperationID { get; set; }
        public string OperationName { get; set; }

    }

    public class LotListModel
    {
        public int JobItemID { get; set; }
        public string LotDescription { get; set; }

    }

    public class QBAccountListModel
    {
        public string ListID { get; set; }
        public string Account { get; set; }

    }

    public class ActiveJobListModel
    {
        public int JobID { get; set; }
        public string JobNumber { get; set; }

    }
}
