using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Montsees.Data.DataModel
{
	public class OperationModel
	{
        public int JobItemID { get; set; }
        public int JobSetupID { get; set; }
		public int SetupID { get; set; }
		public int WorkcodeID { get; set; }
		public int ProcessOrder { get; set; }
		public string WorkCode { get; set; }
		public string Label { get; set; }
		public int SetupCost { get; set; }
		public int OperationCost { get; set; }
        public int Hours { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }
		public bool Completed { get; set; }
		public string OperationName { get; set; }
        public string OperationID { get; set; }
	
	}

    public class OperationDetailedModel
    {
        public int JobSetupID { get; set; }
        public int OperationID { get; set; }
        public int ProcessOrder { get; set; }
        public string label { get; set; }
        public int Setup_Cost { get; set; }
        public int Operation_Cost { get; set; }
        public string Label { get; set; }
        public int SetupCost { get; set; }
        public int OperationCost { get; set; }
        public bool Completed { get; set; }
        public string Name { get; set;}
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }
        public float Hours { get; set; }
        public int ID { get; set; }
        public string Comments { get; set; }
        public int JobItemID { get; set; }
        public int SetupID { get; set; }
        public int SetupImageID { get; set; }

    }

    public class OpenOperationLine
    {
        public int childindex { get; set; }
        public int parentindex { get; set; }
        public int EmployeeID { get; set; }
        public int QtyIn { get; set; }
        public int QtyOut { get; set; }
        public int Hours { get; set; }
        public bool check { get; set; }
    }

    public class SetupLogModel
    {
        public int ProcessID { get; set; }
        public int JobSetupID { get; set; }
        public string Name { get; set; }
        public float Hours { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOut { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Login { get; set; }
        public DateTime Logout { get; set; }
        public bool Fix { get; set; }
        public string Description { get; set; }
        public int MachineID { get; set; }
        public bool Completed { get; set; }
    }

    public class SetupFixturesModel
    {
        public string PartNumber {get; set;}
        public string DrawingNumber { get; set; }
        public int Quantity { get; set; }
        public string ContactName { get; set; }
        public string Location { get; set; }
        public int FixtureRevID { get; set; }
        public string Note { get; set; }
    }

    public class SetupLogHistoryModel
    {
        public int JobSetupID { get; set; }
        public int JobItemID { get; set; }
            public string JobNumber { get; set; }
            public string Name { get; set; }
            public string Machine { get; set; }
            public int Quantity { get; set; }
            public int QuantityIn { get; set; }
            public int QuantityOut {get;set;}
            public float Hours { get; set; }
            public DateTime Logout { get; set; }
    }

    public class SetupEntriesModel
    {
        public int SetupEntryID { get; set; }
        public int SetupID { get; set; }
        public string Name { get; set; }
        public string Entry { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class CorrectiveActionModel
    {
        public int CARID { get; set; }
        public int JobItemID { get; set; }
        public int RevisionID { get; set; }
        public string InitEmployee { get; set; }
        public string ImpEmployee { get; set; }
        public bool CustomerCAR { get; set; }
        public string CustomerCARNum { get; set; }
        public DateTime InitiationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Definition { get; set; }
        public string RootCause { get; set; }
        public string ImmediateCorrective { get; set; }
        public string PreventiveAction { get; set; }
        public bool Completed { get; set; }
        public bool Initiated { get; set; }
        public int AuditTimeframe { get; set; }
        public string Revision_Nmber { get; set; }
        public int DetailID { get; set; }
        public string PartNumber { get; set; }
        public string DrawingNumber { get; set; }
        public string CAbbr { get; set; }
    }

    public class DeptScheduleColumnsModel
    {
        string Column;
        string ColType;
         
    //    public DeptScheduleColumsnModel(string Column, string ColType)
    //    {
    //        public string Column { get; set; }
    //    public string ColType { get; set; }
    
    //}
    }

}
