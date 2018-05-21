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
	public class InspectionRepository : DapperRepositoryBase
	{
		public InspectionRepository(IUnitOfWork uw) : base(uw) { }

		public Customer GetCustomerByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<Customer>(@"SELECT * FROM Customer
													JOIN Job ON Job.CompanyID = Customer.CustomerID
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
			return this.Uw.Context.Query<DeliveryModel>(@"SELECT DISTINCT 
														DeliveryItem.DeliveryItemID, 
														Delivery.CurrDelivery, 
														DeliveryItem.Quantity, 
														[Purchase Order].PONumber, 
														Suspend,
														[DeliveryItem].[RTS] as ReadyToShip, 
														Delivery.ShipDate

														FROM DeliveryItem 
														LEFT JOIN Delivery 
														LEFT JOIN [PO Item] ON Delivery.POItemID = [PO Item].POItemID
														LEFT JOIN [Purchase Order] ON [PO Item].POID = [Purchase Order].POID
														ON DeliveryItem.DeliverID = Delivery.DeliveryID AND DeliveryItem.LotNumber=@JobItemId
														WHERE DeliveryItem.LotNumber=@JobItemId
														GROUP BY DeliveryItem.DeliveryItemID, 
														Delivery.CurrDelivery, 
														DeliveryItem.Quantity, 
														[Purchase Order].PONumber, 
														Suspend, 
														DeliveryItem.[RTS], 
														Delivery.ShipDate;",
														new { JobItemID = jobItemId }).ToList();
		}

		public JobDetailModel GetJobDetailModelByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<JobDetailModel>(@"SELECT 
										Customer.CompanyName, 
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
										LEFT OUTER JOIN Customer 
										RIGHT OUTER JOIN Detail 
											ON Customer.CustomerID = Detail.CompanyID 
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

		public List<LabelModel> GetLabelByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<LabelModel>(@"SELECT [Job Item].JobItemID,
											 Version.[Revision Number] As RevisionNumber, 
											 Detail.PartNumber, Detail.DrawingNumber, 
											 Job.JobNumber, 
											 Customer.CompanyName, 
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
											  LEFT JOIN Customer ON Detail.CompanyID = Customer.CustomerID
											   LEFT JOIN RTSInventory ON Version.RevisionID = RTSInventory.RevisionID
												ON [Job Item].[Active Version] = Version.RevisionID
												 LEFT JOIN InvStatus ON Inventory.StatusID = InvStatus.InvStatusID
											WHERE [JOb Item].JobItemID=@JobItemID   AND DeliveryItem.RTS=1
											GROUP BY [Job Item].JobItemID, Version.[Revision Number], Detail.PartNumber, Detail.DrawingNumber, Job.JobNumber, Customer.CompanyName, Inventory.InventoryID, Inventory.Status, Inventory.Quantity, Inventory.Location1, Inventory.Note1, RTSInventory.SumOfQuantity, InvStatus.Status
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

		public void MoveToInventory(int jobItemId, int qty, int status, string location, string notes)
		{
			JobDetailModel m =	this.GetJobDetailModelByJobItemId(jobItemId);
			JobItem jobItem = this.GetJobItemById(jobItemId);
			this.Uw.Context.Execute(@"INSERT INTO Inventory (RevisionID,LotNumber,PONumber,Location1,Note1,StatusID,Quantity)
									   VALUES(@RevisionID,@LotNumber,@PONumber,@Location,@Notes,@StatusID,@Quantity)", 
									new
									{
										RevisionID = m.RevisionID,
										LotNumber = jobItemId,
										JobNumber= m.JobNumber,
										PONumber = jobItem.PONumber,
										StatusID = status,
										Location = location,
										Notes = notes,
										Quantity = qty
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
