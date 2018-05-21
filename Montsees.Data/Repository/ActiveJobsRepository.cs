using HardingeTaiwan.Repository;
using Monsees.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Monsees.Database;
using Montsees.Data.DataModel;
using Monsees.Data;

namespace Montsees.Data.Repository
{
	public class ActiveJobsRepository : DapperRepositoryBase
	{
		public ActiveJobsRepository(IUnitOfWork uw) : base(uw) { }

		public Customer GetCustomerByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<Customer>(@"SELECT * FROM CustomerDB
													JOIN Job ON Job.CompanyID = CustomerDB.CustomerID
													JOIN JobItem ON Job.JobID = JobItem.JobID AND JobItemID=@JobItemID",
													new { JobItemID = jobItemId }).SingleOrDefault();

		}

		public Detail GetDetailByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<Detail>(@"SELECT * FROM Detail 
													JOIN [Job Item] ON Detail.DetailID = [Job Item].DetailID AND [Job Item].JobItemID=@JobItemID",
													new { JobItemID = jobItemId }).SingleOrDefault();
		}

		public List<DeliveryModel> GetDelivery(int jobItemId)
		{
			return this.Uw.Context.Query<DeliveryModel>(@"SELECT DISTINCT DeliveryItem.DeliveryItemID, 
														Delivery.CurrDelivery, 
														DeliveryItem.Quantity, 
														[Purchase Order].PONumber, 
														[Suspend],
														[DeliveryItem].[RTS] as ReadyToShip, 
														Delivery.ShipDate,
                                                        Delivery.Shipped

														FROM DeliveryItem 
														LEFT JOIN Delivery 
														LEFT JOIN [PO Item] ON Delivery.POItemID = [PO Item].POItemID
														LEFT JOIN [Purchase Order] ON [PO Item].POID = [Purchase Order].POID
														ON DeliveryItem.DeliverID = Delivery.DeliveryID AND DeliveryItem.LotNumber=@JobItemId
														WHERE DeliveryItem.LotNumber=0
														GROUP BY DeliveryItem.DeliveryItemID, 
														Delivery.CurrDelivery, 
														DeliveryItem.Quantity, 
														[Purchase Order].PONumber, 
														Suspend, 
														DeliveryItem.[RTS], 
														Delivery.ShipDate, Delivery.Shipped",
														new { JobItemID = jobItemId }).ToList();
		}

		public JobDetailModel GetJobDetailModelByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<JobDetailModel>(@"SELECT 
										CustomerDB.CompanyName, 
										Detail.PartNumber, 
										Detail.DrawingNumber, 
										Detail.[Heat Treat] As HeatTreat, 
										Detail.Plating, 
										[Job Item].Notes, 
										[Job Item].Quantity, 
										[Job Item].IsOpen, 
										[Job Item].JobItemID, 
										Detail.DetailID, 
										Job.JobNumber, 
										Version.[Revision Number] as RevisionNumber, 
										Version.RevisionID,
										[Job Item].[Active Version] As ActiveVersion, 
										[Job Item].Comments 
										FROM Job 
										RIGHT OUTER JOIN [Job Item] 
										LEFT OUTER JOIN CustomerDB 
										RIGHT OUTER JOIN Detail 
											ON CustomerDB.CustomerID = Detail.CompanyID 
											ON [Job Item].DetailID = Detail.DetailID 
											ON Job.JobID = [Job Item].JobID 
										LEFT OUTER JOIN Version ON [Job Item].[Active Version] = Version.RevisionID
										WHERE [Job Item].JobItemID=@JobItemID",
										new { JobItemID = jobItemId }).SingleOrDefault();
		}

		public List<OperationModel> GetOperationsByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<OperationModel>(@"
									SELECT 
									JobSetup.JobSetupID,
									 Setup.SetupID, 
									 JobSetup.WorkcodeID, 
									 JobSetup.ProcessOrder, 
									 [JobSetup].[SetupID],
									 [WorkCode].[WorkCode],
									 CASE WHEN JobSetup.SetupID IS NULL THEN 
										WorkCode.WorkCode
									ELSE Operation.OperationName
									END AS Label, 
									 Setup.[Setup Cost] As SetupCost,
									  Setup.[Operation Cost] As OperationCost,
									  JobSetup.Completed, 
									  Operation.OperationName 
									FROM Setup LEFT JOIN Operation ON Setup.OperationID = Operation.OperationID
									RIGHT JOIN JobSetup ON Setup.SetupID = JobSetup.SetupID
									LEFT JOIN WorkCode ON JobSetup.WorkcodeID = WorkCode.WorkCodeID
									WHERE JobSetup.JobItemID=@JobItemId
									ORDER BY JobSetup.ProcessOrder",
			new { JobItemID = jobItemId }).ToList();



		}

        public List<OperationDetailedModel> GetDetailedOperationsByJobItemId(int jobItemId)
        {
            return this.Uw.Context.Query<OperationDetailedModel>(@"
                                    SELECT[JobSetupID], 
                                    [OperationID], 
                                    [ProcessOrder], 
                                    [Label], 
                                    [Setup Cost] AS Setup_Cost, 
                                    [Operation Cost] AS Operation_Cost, 
                                    [Completed], 
                                    Name, 
                                    QtyIn, 
                                    QtyOut, 
                                    Hours, 
                                    [ID], 
                                    [Comments], 
                                    JobItemID, 
                                    SetupID, 
                                    SetupImageID 
                                    FROM[JobItemSetupSummary] 
                                    WHERE[JobItemID] = @JobItemID 
                                    ORDER BY[ProcessOrder]",
            new { JobItemID = jobItemId }).ToList();
        }

        public List<SetupLogModel> GetSetupLogBySetupID(int jobsetupid)
        {
            return this.Uw.Context.Query<SetupLogModel>(@"
                                    SELECT ProcessID, 
                                    JobSetupID, 
                                    Name, 
                                    Hours, 
                                    QuantityIn, 
                                    QuantityOut, 
                                    EmployeeID, 
                                    Login, 
                                    Logout, 
                                    Fix, 
                                    Description, 
                                    MachineID, 
                                    Completed 
                                    FROM LoggedHoursSummary 
                                    WHERE JobSetupID = @JobSetupID"
                                    , new { JobSetupID = jobsetupid }).ToList();
        }

        public List<SetupFixturesModel> GetSetupFixturesBySetupID(int setupid)
        {
            return this.Uw.Context.Query<SetupFixturesModel>(@"SELECT 
                                    [PartNumber], 
                                    [DrawingNumber], 
                                    [Quantity], 
                                    [ContactName], 
                                    Location, 
                                    FixtureRevID, 
                                    Note 
                                    FROM [FixtureOrdersbySetup] 
                                    WHERE [SetupUsingID] = @SetupID"
                                    , new { SetupID = setupid }).ToList();
        }
        public List<SetupLogHistoryModel> GetSetupLogHistoryBySetupID(int setupid)
        {
            return this.Uw.Context.Query<SetupLogHistoryModel>(@"
                                    SELECT 
                                    JobSetupID, 
                                    JobItemID, 
                                    JobNumber, 
                                    Name, 
                                    Machine, 
                                    Quantity, 
                                    QuantityIn, 
                                    QuantityOut, 
                                    Hours, 
                                    Logout 
                                    FROM SetupHistory 
                                    WHERE Completed = 1 And SetupID = @SetupID 
                                    ORDER BY JobItemID DESC"
                                    , new { SetupID = setupid }).ToList();
        }

        public List<SetupEntriesModel> GetSetupEntriesBySetupID(int setupid)
        {
            return this.Uw.Context.Query<SetupEntriesModel>(@"
                                    SELECT SetupEntryID, 
                                    SetupID, 
                                    Name, 
                                    Entry, 
                                    Timestamp 
                                    FROM SetupEntries LEFT OUTER JOIN Employees ON SetupEntries.EmployeeID = Employees.EmployeeID 
                                    WHERE SetupID = @SetupID"
                                    , new { SetupID = setupid }).ToList();
        }

        public void SetCompletionStatus(int jobSetupId, bool status)
		{
			this.Uw.Context.Execute(@"UPDATE JobSetup SET Completed=@Status WHERE JobSetupID=@JobSetupId",
				new { JobSetupID = jobSetupId, Status = status });
		}

		public void SetShipStatus(int deliveryItemId, bool readyToShip, bool suspended)
		{
			this.Uw.Context.Execute(@"UPDATE DeliveryItem SET RTS=@RTS,Suspend=@Suspended WHERE DeliveryItemId=@DeliveryItemID",
				new { DeliveryItemId = deliveryItemId, RTS = readyToShip, Suspended = suspended });
		}

		public JobItem GetJobItemById(int jobItemId)
		{
			return this.Uw.Context.Query<JobItem>("SELECT * FROM [Job Item] WHERE JobItemID=@JobItemID", new { JobItemID = jobItemId }).SingleOrDefault();
		}

        public List<MatlQuoteModel> GetMatlQuotesByJobItemID(int jobItemId)
        {
            return this.Uw.Context.Query<MatlQuoteModel>(@"SELECT [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending] FROM [MatQuoteQueue] WHERE [JobItemID] =@JobItemID", new { JobItemID = jobItemId }).ToList();
        }

        public List<MatlOrderModel> GetMatlOrdersByJobItemID(int jobItemId)
        {
            return this.Uw.Context.Query<MatlOrderModel>(@"SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], [MatPriceID], pct, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =@JobItemID", new { JobItemID = jobItemId }).ToList();
        }

        public List<FixtureJobItemModel> GetFixturesByJobItemID(int jobItemId)
        {
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + jobItemId + ";";
            string DetailID = "0";
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            System.Data.SqlClient.SqlDataReader reader;
            con.Open();

            reader = comm.ExecuteReader();

            while (reader.Read())
            {
                DetailID = reader["DetailID"].ToString();
                
            }

            con.Close();
            return this.Uw.Context.Query<FixtureJobItemModel>(@"SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, Note, OperationName, FixtureRevID FROM [FixtureOrders] WHERE [DetailUsingID] =@DetailID", new { DetailID = DetailID }).ToList();
        }

        public List<AssyMachinedCompModel> GetMachinedComponentsByJobItemID(int jobItemId)
        {
            return this.Uw.Context.Query<AssyMachinedCompModel>(@"SELECT [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [PerAssembly], [NextOp] FROM [AssemblyItemsSummary] WHERE [AssemblyLot] = @JobItemID", new { JobItemID = jobItemId }).ToList();
        }

        public ViewJobItem GetJobItemSummaryByJobItemID(int jobItemId)
        {
            return this.Uw.Context.Query<ViewJobItem>(@"SELECT * FROM [ViewJobItem] WHERE [JobItemID] = @JobItemID", new { JobItemID = jobItemId }).SingleOrDefault();
        }

        public List<CertificationSummary> GetCertificationSummaryByJobItemID(int jobItemId)
        {
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + jobItemId + ";";
            string RevisionID = "0";
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            System.Data.SqlClient.SqlDataReader reader;
            con.Open();

            reader = comm.ExecuteReader();

            while (reader.Read())
            {

                RevisionID = reader["Active Version"].ToString();
            }

            con.Close(); return this.Uw.Context.Query<CertificationSummary>(@"SELECT [CertCompReqd], [MatlCertReqd], [PlateCertReqd], [SerializationReqd] FROM Version WHERE RevisionID = @RevisionID", new { RevisionID = RevisionID }).ToList();
        }

        public List<CorrectiveActionModel> GetCorrectiveActionsByJobItemID(int jobItemId)
        {
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + jobItemId + ";";
            string DetailID = "0";
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            System.Data.SqlClient.SqlDataReader reader;
            con.Open();

            reader = comm.ExecuteReader();

            while (reader.Read())
            {
                DetailID = reader["DetailID"].ToString();

            }

            con.Close();
            return this.Uw.Context.Query<CorrectiveActionModel>(@"SELECT * FROM CorrectiveActionView WHERE[DetailID] = @DetailID", new { DetailID = DetailID }).ToList();
        }

        public List<AssyPurchasedCompModel> GetPurchasedComponentsByJObItemID(int jobItemId)
        {
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + jobItemId + ";";
            string RevisionID = "0";
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            System.Data.SqlClient.SqlDataReader reader;
            con.Open();

            reader = comm.ExecuteReader();

            while (reader.Read())
            {
                
                RevisionID = reader["Active Version"].ToString();
            }

            con.Close();

            return this.Uw.Context.Query<AssyPurchasedCompModel>(@"SELECT [DrawingNumber], [PerAssy], [ItemNumber], [VendorName], [Each], [Weblink] FROM [BOMItemSummary] WHERE [AssyRevisionID] = @RevisionID", new { RevisionID = RevisionID }).ToList();
        }

        public List<SubcontractLineModel> GetSubcontractLinesByJobItemID(int jobItemId)
        {
            return this.Uw.Context.Query<SubcontractLineModel>(@"SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = @JobItemID", new { JobItemID = jobItemId }).ToList();
        }

		public List<LabelModel> GetLabelByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<LabelModel>(@"SELECT [Job Item].JobItemID,
											 Version.[Revision Number] As RevisionNumber, 
											 Detail.PartNumber, Detail.DrawingNumber, 
											 Job.JobNumber, 
											 CustomerDB.CompanyName, 
											 Inventory.InventoryID, 
											 Inventory.Status, 
											 Inventory.Quantity, 
											 Inventory.Location1, 
											 Inventory.Note1,
											  Sum(DeliveryItem.Quantity) AS SumOfQuantity,
											   RTSInventory.SumOfQuantity AS RTSQuantity, InvStatus.Status As InvStatus
											FROM Inventory 
											RIGHT JOIN [Job Item] ON Inventory.LotNumber = [Job Item].JobItemID
											LEFT JOIN Job ON [Job Item].JobID = Job.JobID 
											LEFT JOIN DeliveryItem ON [Job Item].JobItemID = DeliveryItem.LotNumber AND DeliveryItem.RTS=1
											 LEFT JOIN 
											 Version LEFT JOIN Detail ON Version.DetailID = Detail.DetailID
											  LEFT JOIN CustomerDB ON Detail.CompanyID = CustomerDB.CustomerID
											   LEFT JOIN RTSInventory ON Version.RevisionID = RTSInventory.RevisionID
												ON [Job Item].[Active Version] = Version.RevisionID
												 LEFT JOIN InvStatus ON Inventory.StatusID = InvStatus.InvStatusID
											WHERE [JOb Item].JobItemID=@JobItemID   AND DeliveryItem.RTS=1
											GROUP BY [Job Item].JobItemID, Version.[Revision Number], Detail.PartNumber, Detail.DrawingNumber, Job.JobNumber, CustomerDB.CompanyName, Inventory.InventoryID, Inventory.Status, Inventory.Quantity, Inventory.Location1, Inventory.Note1, RTSInventory.SumOfQuantity, InvStatus.Status
											HAVING RTSInventory.SumOfQuantity Is Not Null;	",
				new { JobItemID = jobItemId }).ToList();
		}

		public List<LabelDeliveryItem> GetDeliveryItemForLabel(int jobItemId)
		{
			return this.Uw.Context.Query<LabelDeliveryItem>(@"SELECT 
											DeliveryItem.LotNumber, 
											Sum(DeliveryItem.Quantity) AS SumOfQuantity, 
											Delivery.CurrDelivery, 
											[Purchase Order].PONumber, 
											Delivery.Shipped
											FROM DeliveryItem 
											LEFT JOIN ((Delivery LEFT JOIN [PO Item] ON Delivery.POItemID = [PO Item].POItemID) 
											LEFT JOIN [Purchase Order] ON [PO Item].POID = [Purchase Order].POID) ON DeliveryItem.DeliverID = Delivery.DeliveryID
											WHERE DeliveryItem.LotNumber=@JobItemId
											GROUP BY DeliveryItem.LotNumber, Delivery.CurrDelivery, [Purchase Order].PONumber, Delivery.Shipped;",
				new { JobItemID = jobItemId }).ToList();
		}

		public List<InventoryStatusModel> GetInventoryStatusByRevisionID(int revisionId)
		{
			return this.Uw.Context.Query<InventoryStatusModel>(@"SELECT Inventory.*, InvStatus.Status as InvStatus FROM Inventory
																LEFT JOIN InvStatus ON Inventory.StatusID = InvStatus.InvStatusID 
																WHERE Inventory.RevisionID=@RevisionID",
																new { RevisionID = revisionId }).ToList();
		}

		public void CloseLot(int jobItemId)
		{
			this.Uw.Context.Execute(@"UPDATE DeliveryItem SET RTS=1 WHERE LotNumber=@JobItemID",
					new  { JobItemID = jobItemId });
            this.Uw.Context.Execute(@"UPDATE [Job Item] SET IsOpen=0 WHERE JobItemID=@JobItemID",
                    new { JobItemID = jobItemId });
		}

		public void MoveToFixtureInventory(int jobItemId, int qty, string location, string note)
		{
            
            JobDetailModel m =	this.GetJobDetailModelByJobItemId(jobItemId);
			            
            
			this.Uw.Context.Execute(@"INSERT INTO FixtureInventory (RevisionID,Location, Note, Quantity)
									   VALUES(@RevisionID,@Location,@Note, @qty)", 
									new
									{
										RevisionID = m.RevisionID,		
										Location = location,
										Note = note,	
									    qty = qty,
									});
		}


		public List<InvStatus> GetAllInventoryStatusItems()
		{
			return this.Uw.Context.Query<InvStatus>(@"SELECT * FROM InvStatus", null).ToList();
		}

		public LotPartTotals GetLotPartTotals(int jobItemId)
		{
			LotPartTotals totals = new LotPartTotals();
			totals.Inventoried = this.Uw.Context.Query< Scalar<int> >(@"SELECT SUM(Quantity) as Value from Inventory WHERE LotNumber=@JobItemID",
				   new { JobItemID = jobItemId }).SingleOrDefault().Value;

			totals.Delivered = this.Uw.Context.Query<Scalar<int>>(@"SELECT SUM(Quantity) as Value from DeliveryItem WHERE LotNumber=@JobItemID",
			new { JobItemID = jobItemId }).SingleOrDefault().Value;


			return totals;

		}

        public void UpdateInspectionMeasurement(int jobItemId, int dimension, string m1, string m2, string m3, string final, string remarks)
        {

            string sql = @"UPDATE [InspectionItems] 
                        SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                            [Remarks] = @Remarks WHERE [JobItemID] = @JobItemId AND [Dimension] = @Dimension";

            this.Uw.Connection.Execute(sql,new {JobItemId=jobItemId,
                                                Dimension=dimension,
                                                M1=m1,
                                                M2=m2,
                                                M3=m3,
                                                Final=final,
                                                Remarks=remarks});
        }
		
	}
}
