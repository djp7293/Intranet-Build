using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montsees.Data.DataModel
{
	public class JobDetailbase
	{
		public int JobItemID { get; set; }
		public string CompanyName { get; set; }
		public string RevisionNumber { get; set; }
		public string DrawingNumber { get; set; }
		public string JobNumber { get; set; }
		public string PartNumber { get; set; }
        
	}

	public class LabelModel : JobDetailbase
	{
		public int InventoryID { get; set; }
		public string Status { get; set; }
		public int Quantity { get; set; }
		public string Location1 { get; set; }
		public string Note1 { get; set; }
		public int SumOfQuantity { get; set; }
		public int RTSQuantity { get; set; }
		public string InvStatus { get; set; }
        public string CAbbr { get; set; }
        public bool ITAR { get; set; }
        

	}

    public class FileList : JobDetailbase
    {
        public int ID { get; set; }
        public int revisionID { get; set; }
        public string revision { get; set; }
        public string part { get; set; }        
        public string filetype { get; set; }
    }

    public class MatCertList : JobDetailbase
    {
        public string ID { get; set; }
        public int MatPriceID { get; set; }
        public int SerialNumber { get; set; }        
        public string filetype { get; set; }
    }

    public class ViewJobItem : JobDetailbase
    {
        public string CompanyName {get;set;}
        public string PartNumber {get;set;}
        public string DrawingNumber {get;set;}
        public string HeatTreat {get;set;}
        public string Plating {get;set;}
        public int EstimatedTotalHours {get;set;}
        public string Notes {get;set;}
        public int Quantity {get;set;}
        public bool IsOpen {get;set;}
        public int JobItemID {get;set;}
        public int Expr1 {get;set;}
        public int DetailID {get;set;}
        public string JobNumber {get;set;}
        public double PriceEach {get;set;}
        public string RevisionNumber {get;set;}
        public int ActiveVersion {get;set;}
        public string Comments {get;set;}
        public string PONumber {get;set;}
        public int EstimatedHours {get;set;}
        public int LoggedHours {get;set;}
        public bool IsAssembly {get;set;}
        public int CustomerID {get;set;}
        public int JobID {get;set;}
        public string PlatingLabel {get;set;}
        public string HeatTreatLabel {get;set;}
        public string SubcontractLabel {get;set;}
        public string Subcontract2Label {get;set;}
        public double StockCut {get;set;}
        public int PartsPerCut {get;set;}
        public bool PurchaseCut {get;set;}
        public bool Drill {get;set;}
        public double DrillSize {get;set;}
        public string Material {get;set;}
        public string Dimension {get;set;}
        public string MaterialSize {get;set;}
        public int PlatingID {get;set;}
        public int HeatTreatID {get;set;}
        public int SubcontractID {get;set;}
        public int SubcontractID2 {get;set;}
        public int MaterialID {get;set;}
        public int MaterialDimID {get;set;}
        public int MaterialSizeID {get;set;}
        public double LengthperPart {get;set;}
        public double ScrapRate {get;set;}

    }

	public class JobDetailModel : JobDetailbase
	{

		public int RevisionID { get; set; }
		public string HeatTreat { get; set; }
		public string Plating { get; set; }
		public string Quantity { get; set; }
		public bool IsOpen { get; set; }
		public int ActiveVersion { get; set; }
		public string Comments { get; set; }
        public int DetailID { get; set; }
        public int OperationID { get; set; }

		

	}

    public class CertificationSummary : JobDetailbase
    {
        public int LotNumber { get; set; }
        public bool PCert { get; set; }
        public bool MCert { get; set; }
        public bool CertCompReqd { get; set; }
        public bool PlateCertReqd { get; set; }
        public bool MatlCertReqd { get; set; }
        public bool SerializationReqd { get; set; }
           
    }

    public class POHeaderModel : JobDetailbase
    {
        public string VendorName { get; set; }
        public string PONumber { get; set; }
        public string ContactName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Total { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }

    public class FixtureDetailModel : JobDetailbase
    {

        public int FixtureRevID { get;  set;}
        public int SetupID { get; set; }
        public int OperationID { get; set; }
        public string OperationName { get; set; }

        
    }

    public class MatlQuoteModel
    {
        public string MaterialName { get; set; }
        public string Dimension { get; set; }
        public float Diameter { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Length { get; set; }
        public int Quantity { get; set; }
        public bool Cut { get; set; }
        public bool OrderPending { get; set; }

    }

    public class MatlOrderModel
    {
        public string MaterialName { get; set; }
        public string Dimension { get; set; }
        public float D { get; set; }
        public float H { get; set; }
        public float W { get; set; }
        public float L { get; set; }
        public int Qty { get; set; }
        public bool Cut { get; set; }
        public bool received { get; set; }
        public bool Prepared { get; set; }
        public string Location { get; set; }
        public string MaterialSource { get; set; }
        public int MatPriceID { get; set; }
        public float pct { get; set; }
        public int MatlAllocationID { get; set; }
    }

    public class AssyPurchasedCompModel
    {
        public string DrawingNumber { get; set; }
        public int PerAssy { get; set; }
        public string ItemNumber { get; set; }
        public string VendorName { get; set; }
        public float Each { get; set; }
        public string Weblink { get; set; }
    }

    public class AssyMachinedCompModel
    {
        public string PartNumber { get; set; }
        public string Revision_Number { get; set; }
        public string DrawingNumber { get; set; }
        public int PerAssembly { get; set; }
        public string NextOp { get; set; }
    }

    public class SubcontractLineModel
    {
        public int SubcontractID { get; set; }
        public string WorkCode { get; set; }
        public int Quantity { get; set; }
        public DateTime DueDate { get; set; }
        public bool Received { get; set; }
    }

    public class FixtureJobItemModel
    {
        public string PartNumber { get; set; }
        public string DrawingNumber { get; set; }
        public int Quantity { get; set; }
        public string ContactName { get; set; }
        public string Location { get; set; }
        public string Note { get; set; }
        public string OperationName { get; set; }
        public int FixtureRevID { get; set; }
    }

    public class FixtureSetupSheetModel
    {
                   
        
        public string PartNumber { get; set; }
        public string DrawingNumber { get; set; }
        
        public string Location { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }

    }

    public class ToolingDetailModel : JobDetailbase
    {
        public int ToolingID { get; set; }
        public string ItemNum { get; set; }
        public int SubcontractID { get; set; }
        public string VendorName { get; set; }
        public int MFGID { get; set; }
        public string MFG { get; set; }
        public string SerialNum { get; set; }
        public string Description { get; set; }
    }

    public class SetupDetailModel
    {
        public int JobSetupID { get; set; }
        public int SetupID { get; set; }
        public int JobItemID { get; set; }
        public string JobNumber { get; set; }
        public int OperationID { get; set; }        
        public string OperationName { get; set; }        
        public string SetupCost { get; set; }
        public string OperationCost { get; set; }
        public string Description { get; set; }
        public string PartNumber { get; set; }
        public string RevisionNumber { get; set; }
        public string DrawingNumber { get; set; }
        public string ProcessOrder { get; set; }
        public string Comments { get; set; }
        public int Quantity { get; set; }
    }

    public class ProcessDetailModel
    {
        public int ProcessID { get; set; }
        public int JobItemID { get; set; }
        public int MachineID { get; set; }
        public int EmployeeID { get; set; }
        public int SetupID { get; set; }
        public string Description { get; set; }
        public DateTime Login { get; set; }
        public int QuantityIn { get; set; }
        public DateTime LateStartAtLogin { get; set; }
        public string Name { get; set; }
        public string Machine { get; set; }
    }

    public class MaterialTagModel : JobDetailbase
    {
        public int MatPriceID { get; set; }
        public string Material { get; set; }
        public string VendorName { get; set; }
        public float Length { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }

    public class DeptSchedule
    {
        public string JobNumber { get; set; }
        public int JobItemID { get; set; }
        public string PartNumber { get; set; }        
        public string RevisionNumber { get; set; }
        public string DrawingNumber { get; set; }
        public string CompanyName { get; set; }
        public string NextDelivery { get; set; }
        public int RevisionID { get; set; }
        public string CustCode { get; set; }
        public string PM { get; set; }
        public int qty { get; set; }
        public string LateStart { get; set; }
        public string MatlReady { get; set; }
        public string Available { get; set; }
        public bool AreFixtures { get; set; }
        public bool ITAR { get; set; }
        public bool Hot { get; set; }
        public int NewRenew { get; set; }
        public int NewPart { get; set; }
        public string filter1 { get; set; }
        public string filter2 { get; set; }
        public string filter3 { get; set; }
        public string filter4 { get; set; }
        public string filter5 { get; set; }
    }

}
