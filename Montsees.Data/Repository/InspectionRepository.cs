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
			return this.Uw.Context.Query<Customer>(@"SELECT * FROM CustomerDB
													JOIN Job ON Job.CompanyID = CustomerDB.CustomerID
													JOIN JobItem ON Job.JobID = JobItem.JobID AND JobItemID=@JobItemID",
													new { JobItemID = jobItemId }).SingleOrDefault();

		}

        public List<SetupListModel> GetSetupList(int jobItemId)
        {

            return this.Uw.Context.Query<SetupListModel>(@"SELECT JobSetupID, OperationName FROM Operation RIGHT JOIN (JobSetup LEFT JOIN Setup ON JobSetup.SetupID = Setup.SetupID) ON Operation.OperationID = Setup.OperationID WHERE [JobSetup].[JobItemID]=@JobItemID", 
                                                    new { JobItemID = jobItemId }).ToList();

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

        public MaterialTagModel GetMaterialTagByMatPriceID(int MatPriceID)
        {
            return this.Uw.Context.Query<MaterialTagModel>(@"SELECT MatPriceID, VendorName, Material, Length, Size, Quantity FROM MaterialTagData WHERE MatPriceID = @MatPrice", new { MatPrice = MatPriceID }).SingleOrDefault();
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

        public CorrectiveActionModel GetCorrectiveActionbyCARID(int carId)
        {
            return this.Uw.Context.Query<CorrectiveActionModel>(@"SELECT 
										CARID, JobItemID, InitEmployee, ImpEmployee, CustomerCAR, CustomerCARNum, InitiationDate, DueDate, Definition, RootCause, ImmediateCorrective, PreventiveAction, Completed, Initiated, AuditTimeframe  
										FROM CorrectiveActionView
										WHERE CARID=@CARID",
                                        new { CARID = carId }).SingleOrDefault();
        }

        public List<ContactModel> GetContactsbyCustomerId (int companyid)
        {
            return this.Uw.Context.Query<ContactModel>(@"SELECT ContactID, ContactName FROM Contact WHERE CustomerID=@CustomerID",
                                                    new { CustomerID = companyid }).ToList();
        }

        public List<ContactModel> GetContactsbySupplierId(int companyid)
        {
            return this.Uw.Context.Query<ContactModel>(@"SELECT ContactID, ContactName FROM Contact WHERE VendorID=@VendorID",
                                                    new { VendorID = companyid }).ToList();
        }


        public List<EmployeeModel> GetActiveEmployees()
        {
            int companyid = 0;
            return this.Uw.Context.Query<EmployeeModel>(@"SELECT EmployeeID, Name FROM Employees WHERE Active=1 ORDER BY Name", new { CustomerID = companyid }
                                                    ).ToList();
        }
        public List<DecommissionModel> GetDecommissionList()
        {
            int companyid = 0;
            return this.Uw.Context.Query<DecommissionModel>(@"SELECT DecommCodeID, DecommissionCode FROM DecommissionCodes ORDER BY DecommissionCode", new { CustomerID = companyid }
                                                    ).ToList();
        }


        public List<MaterialModel> GetMaterials()
        {
            return this.Uw.Context.Query<MaterialModel>(@"SELECT *, MaterialName + ' ' + Type As Material FROM Material WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<InvStatusModel> GetInvStatus()
        {
            return this.Uw.Context.Query<InvStatusModel>(@"SELECT * FROM InvStatus WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<VendorListModel> GetVendors()
        {
            return this.Uw.Context.Query<VendorListModel>(@"SELECT SubcontractID, VendorName FROM Subcontractors WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<MaterialSizeModel> GetMaterialSizes(int materialdim = 0)
        {
            if (materialdim == 0)
            {
                return this.Uw.Context.Query<MaterialSizeModel>(@"SELECT *, CASE WHEN [MaterialDimID] = 1 THEN CAST([Diameter] As nVarChar(10)) WHEN [MaterialDimID] = 4 THEN CAST([Diameter] As nVarChar(10)) ELSE CAST([Height] As nVarChar(10)) + ' x ' + CAST([Width] As nVarChar(10)) END As Size FROM [Material Sizes] WHERE 1=1 ORDER BY [Diameter], [Height]",
                                                         new { CustomerID = 988 }).ToList();
            }
            else
            {
                return this.Uw.Context.Query<MaterialSizeModel>(@"SELECT *, CASE WHEN [MaterialDimID] = 1 THEN CAST([Diameter] As nVarChar(10)) WHEN [MaterialDimID] = 4 THEN CAST([Diameter] As nVarChar(10)) ELSE CAST([Height] As nVarChar(10)) + ' x ' + CAST([Width] As nVarChar(10)) END As Size FROM [Material Sizes] WHERE [MaterialDimID] = @matdimid ORDER BY [Diameter], [Height]",
                                                        new { matdimid = materialdim }).ToList();
            }
        }

        public List<DimensionModel> GetDimensions()
        {
            return this.Uw.Context.Query<DimensionModel>(@"SELECT * FROM [Material Dimension] WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<WorkcodeModel> GetWorkcodes()
        {
            return this.Uw.Context.Query<WorkcodeModel>(@"SELECT * FROM [Workcode] WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<LotListModel> GetLotDescription()
        {
            return this.Uw.Context.Query<LotListModel>(@"SELECT JobItemID, CAST([JobItemID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [JobItem] WHERE IsOpen = @open",
                                                     new { open = 1 }).ToList();
        }

        public List<OperationListModel> GetOperations()
        {
            return this.Uw.Context.Query<OperationListModel>(@"SELECT OperationID, OperationName FROM [Operation] WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<MachineModel> GetMachines()
        {
            return this.Uw.Context.Query<MachineModel>(@"SELECT MachineID, Machine, OperationID FROM [Machines] WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<GaugeTypeModel> GetGaugeTypes()
        {
            return this.Uw.Context.Query<GaugeTypeModel>(@"SELECT GageTypeID, Description FROM [InspGaugeType] WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<QBAccountListModel> GetQBAccounts()
        {
            return this.Uw.Context.Query<QBAccountListModel>(@"SELECT ListID, Account FROM [QBAccountLink] WHERE 1=1",
                                                     new { CustomerID = 988 }).ToList();
        }

        public List<ActiveJobListModel> GetActiveJobs()
        {
            return this.Uw.Context.Query<ActiveJobListModel>(@"SELECT JobID, JobNumber FROM [Job] WHERE IsOpen=@open",
                                                     new { open=1 }).ToList();
        }

        public List<ShipMethod> GetShipMethods()
        {
            return this.Uw.Context.Query<ShipMethod>(@"SELECT ShipMethodID, Name FROM [ShipMethod]",
                                                     new { open = 1 }).ToList();
        }

        public FixtureDetailModel GetFixtureDetailModelByJobItemId(int jobitemid)
        {
            return this.Uw.Context.Query<FixtureDetailModel>(@"SELECT     dbo.JobSetup.JobSetupID, dbo.Setup.SetupID, dbo.Operation.OperationID, dbo.Operation.OperationName
FROM         dbo.Setup LEFT OUTER JOIN
                      dbo.Operation ON dbo.Setup.OperationID = dbo.Operation.OperationID RIGHT OUTER JOIN
                      dbo.JobSetup ON dbo.Setup.SetupID = dbo.JobSetup.SetupID RIGHT OUTER JOIN
                      dbo.FixtureMapping ON dbo.JobSetup.JobSetupID = dbo.FixtureMapping.FirstSetupID
										WHERE FixtureMapping.FixtureJobItemID=@JobItemID",
                                        new { JobItemID = jobitemid }).SingleOrDefault();
        }

		public List<OperationModel> GetOperationsByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<OperationModel>(@"
									SELECT     JobSetup.JobItemID, JobSetup.JobSetupID, Setup.SetupID, JobSetup.WorkcodeID, JobSetup.ProcessOrder, 
                      JobSetup.SetupID, WorkCode.WorkCode, CASE WHEN JobSetup.SetupID IS NULL 
                      THEN WorkCode.WorkCode ELSE Operation.OperationName END AS Label, Setup.[Setup Cost] AS SetupCost, Setup.[Operation Cost] AS OperationCost, 
                      JobSetup.Completed, Operation.OperationID, Operation.OperationName, SUM(Process.Hours) AS Hours, MAX(Process.QuantityIn) AS QuantityIn, 
                      MIN(Process.QuantityOut) AS QuantityOut
FROM         Process RIGHT OUTER JOIN
                      JobSetup ON Process.SetupID = JobSetup.JobSetupID LEFT OUTER JOIN
                      Setup LEFT OUTER JOIN
                      Operation ON Setup.OperationID = Operation.OperationID ON JobSetup.SetupID = Setup.SetupID LEFT OUTER JOIN
                      WorkCode ON JobSetup.WorkcodeID = WorkCode.WorkCodeID
GROUP BY JobSetup.JobItemID, JobSetup.JobSetupID, Setup.SetupID, JobSetup.WorkcodeID, JobSetup.ProcessOrder, JobSetup.SetupID, 
                      WorkCode.WorkCode, Setup.[Setup Cost], Setup.[Operation Cost], Operation.OperationID, Operation.OperationName, 
                      CASE WHEN JobSetup.SetupID IS NULL THEN WorkCode.WorkCode ELSE Operation.OperationName END, JobSetup.Completed

HAVING      (dbo.JobSetup.JobItemID = @JobItemID)
ORDER BY JobSetup.ProcessOrder",
			new { JobItemID = jobItemId }).ToList();

		}

		public void SetCompletionStatus(int jobSetupId, bool status)
		{
			this.Uw.Context.Execute(@"UPDATE JobSetup SET Completed=@Status WHERE JobSetupID=@JobSetupId",
				new { JobSetupID = jobSetupId, Status = status });
		}

        public void CreateProcessRecord(int jobSetupId, int jobItemId, int employeeId, int hours, int QtyIn, int QtyOut)
        {

            this.Uw.Context.Execute(@"INSERT INTO Process (JobItemID, EmployeeID, SetupID, Hours, QuantityIn, QuantityOut) VALUES (@JobItemID, @EmployeeID, @jobSetupId, @hours, @QtyIn, @QtyOut)",
                new { JobItemID = jobItemId, EmployeeID = employeeId, JobSetupID = jobSetupId, hours = hours, QtyIn = QtyIn, QtyOut = QtyOut });

        }

        public List<POAuditReportView> GetPOAudutReportData(int jobitemid)
        {
            return this.Uw.Context.Query<POAuditReportView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT * From POAudit",
                                                new { JobItemID = jobitemid }).ToList();
        }

        public List<InspectionReportView> GetInspectionReportData(int jobitemid)
        {
            return this.Uw.Context.Query<InspectionReportView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT *,[Revision Number] as Revision_Number From InspectionReport WHERE JobItemID=@JobItemID ORDER BY DimensionNumber",
                                                new { JobItemID = jobitemid }).ToList();
        }

        public List<CertificationSummary> GetCertificationSummary(int jobitemid)
        {
            return this.Uw.Context.Query<CertificationSummary>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT * From LotCertificationSummary WHERE LotNumber=@JobItemID",
                                                new { JobItemID = jobitemid }).ToList();
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

        public SetupDetailModel GetSetupDetailsbyJobSetupID(int JobSetupID)
        {
            
            return this.Uw.Context.Query<SetupDetailModel>("SELECT JobSetupID, SetupID, JobItemID, JobNumber, OperationID, [Setup Cost] As SetupCost, [Operation Cost] As OperationCost, OperationName, Description, PartNumber, [Revision Number] As RevisionNumber, DrawingNumber, ProcessOrder, Comments, Quantity FROM [SetupSheetSummary] WHERE JobSetupID=@JobSetupID", new { JobSetupID = JobSetupID }).SingleOrDefault();
        }

        public ProcessDetailModel GetProcessDetailsbyProcessID(int ProcessID)
        {

            return this.Uw.Context.Query<ProcessDetailModel>("SELECT ProcessID, JobItemID, Process.EmployeeID, Process.MachineID, SetupID, Description, Login, QuantityIn, LateStartAtLogin, Name, Machine FROM dbo.Process LEFT OUTER JOIN dbo.Employees ON dbo.Process.EmployeeID = dbo.Employees.EmployeeID LEFT OUTER JOIN dbo.Machines ON dbo.Process.MachineID = dbo.Machines.MachineID WHERE ProcessID=@ProcessID", new { ProcessID = ProcessID }).SingleOrDefault();
        }

        public List<LabelModel> GetLabelByJobItemId(int jobItemId)
		{
			return this.Uw.Context.Query<LabelModel>(@"SELECT     dbo.[Job Item].JobItemID, dbo.Version.[Revision Number] As RevisionNumber, dbo.Detail.PartNumber, dbo.Detail.DrawingNumber, dbo.Job.JobNumber, dbo.CustomerDB.CompanyName, 
                      dbo.Inventory.InventoryID, dbo.Inventory.Quantity, dbo.Inventory.Location1, dbo.Inventory.Note1, SUM(dbo.DeliveryItem.Quantity) AS SumOfQuantity, 
                      dbo.RTSInventory.SumOfQuantity AS RTSQuantity, dbo.InvStatus.Status, dbo.Delivery.Shipped, dbo.CustomerDB.CAbbr, Detail.ITAR
FROM         dbo.Delivery RIGHT OUTER JOIN
                      dbo.DeliveryItem ON dbo.Delivery.DeliveryID = dbo.DeliveryItem.DeliverID RIGHT OUTER JOIN
                      dbo.Inventory RIGHT OUTER JOIN
                      dbo.[Job Item] ON dbo.Inventory.LotNumber = dbo.[Job Item].JobItemID LEFT OUTER JOIN
                      dbo.Job ON dbo.[Job Item].JobID = dbo.Job.JobID ON dbo.DeliveryItem.LotNumber = dbo.[Job Item].JobItemID LEFT OUTER JOIN
                      dbo.Version LEFT OUTER JOIN
                      dbo.Detail ON dbo.Version.DetailID = dbo.Detail.DetailID LEFT OUTER JOIN
                      dbo.CustomerDB ON dbo.Detail.CompanyID = dbo.CustomerDB.CustomerID LEFT OUTER JOIN
                      dbo.RTSInventory ON dbo.Version.RevisionID = dbo.RTSInventory.RevisionID ON dbo.[Job Item].[Active Version] = dbo.Version.RevisionID LEFT OUTER JOIN
                      dbo.InvStatus ON dbo.Inventory.StatusID = dbo.InvStatus.InvStatusID
GROUP BY dbo.[Job Item].JobItemID, dbo.Version.[Revision Number], dbo.Detail.PartNumber, dbo.Detail.DrawingNumber, dbo.Job.JobNumber, dbo.CustomerDB.CompanyName, 
                      dbo.Inventory.InventoryID, dbo.Inventory.Quantity, dbo.Inventory.Location1, dbo.Inventory.Note1, dbo.RTSInventory.SumOfQuantity, dbo.InvStatus.Status, 
                      dbo.Delivery.Shipped, dbo.CustomerDB.CAbbr, Detail.ITAR
HAVING      (dbo.RTSInventory.SumOfQuantity IS NOT NULL) AND (dbo.Delivery.Shipped = 0) AND (dbo.[Job Item].JobItemID = @JobItemID)",
				new { JobItemID = jobItemId }).ToList();
		}

        public List<FixtureSetupSheetModel> GetFixtureDetailModelBySetupID(int SetupID)
        {
            return this.Uw.Context.Query<FixtureSetupSheetModel>(@"SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, FixtureRevID, Note FROM [FixtureOrdersbySetup] WHERE [SetupUsingID]=@SetupID", new { SetupID = SetupID }).ToList(); 
        }

        public List<ToolingDetailModel> GetSpecialToolDetailBySetupID(int SetupID)
        {
            return this.Uw.Context.Query<ToolingDetailModel>(@"SELECT ToolingID, ItemNum, SubcontractID, VendorName, MFGID, MFG, SerialNumID FROM ToolingSetupSheetSummary WHERE SetupID=@SetupID", new { SetupID = SetupID }).ToList();
        }

        public POHeaderModel GetHeaderByPOIDAndType(string POID, int type)
        {
            return this.Uw.Context.Query<POHeaderModel>(@"SELECT     dbo.Subcontractors.VendorName, dbo.SupplyOrders.SuppliesPONum AS PONumber, dbo.Contact.ContactName, dbo.SupplyOrders.Date AS IssueDate, 
                      dbo.SupplyOrders.DueDate, dbo.SupplyOrders.Cost AS Total, dbo.SupplyOrders.Description, dbo.SupplyOrders.Notes AS Note
FROM         dbo.SupplyOrders LEFT OUTER JOIN
                      dbo.Contact ON dbo.SupplyOrders.Contact = dbo.Contact.ContactID LEFT OUTER JOIN
                      dbo.Subcontractors ON dbo.SupplyOrders.VendorID = dbo.Subcontractors.SubcontractID WHERE dbo.SupplyOrders.SuppliesPONum = @poid", new { poid = POID }).SingleOrDefault();
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

            JobDetailModel m = this.GetJobDetailModelByJobItemId(jobItemId);
            FixtureDetailModel n = this.GetFixtureDetailModelByJobItemId(jobItemId);


            this.Uw.Context.Execute(@"INSERT INTO FixtureInventory (RevisionID,Location, OperationID, Note, Quantity, MasterDetailID)
									   VALUES(@RevisionID,@Location, @OperationID, @Note, @qty, @DetailID)",
                                    new
                                    {
                                        RevisionID = m.RevisionID,
                                        Location = location,
                                        OperationID = n.OperationID,
                                        Note = note,
                                        qty = qty,
                                        DetailID = m.DetailID,
                                    });
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

        public void UpdateJobSetupInfo(int JobSetupID, string Comments)
        {
            string sql;

            sql = @"UPDATE [JobSetup] 
                                    SET Comments = @Comments WHERE [JobSetupID] = @JobSetupID";

            this.Uw.Connection.Execute(sql, new
            {
                JobSetupID = JobSetupID,
                Comments = Comments

            });
        }

        public void UpdateToolDetails(int ProcessID, int SetupWorksheetItemID, string column, int ToolNumber, string ToolName, string ToolDetails)
        {
            string sql;
            switch (column)
            {
                case "toolnumber":
                    sql = @"UPDATE [SetupWorksheetItems] 
                                    SET [ToolNumber] = @ToolNumber, [ToolName] = @ToolName, [ToolDetails] = @ToolDetails WHERE [ProcessID] = @ProcessID AND [SetupWorksheetItemID] = @SetupWorksheetItemID";

                    this.Uw.Connection.Execute(sql, new
                    {
                        ProcessID = ProcessID,
                        SetupWorksheetItemID = SetupWorksheetItemID,
                        ToolNumber = ToolNumber,
                        ToolName = ToolName,
                        ToolDetails = ToolDetails
                        
                    });
                    break;
                case "toolname":
                    sql = @"UPDATE [SetupWorksheetItems] 
                                    SET [ToolNumber] = @ToolNumber, [ToolName] = @ToolName, [ToolDetails] = @ToolDetails WHERE [ProcessID] = @ProcessID AND [SetupWorksheetItemID] = @SetupWorksheetItemID";

                    this.Uw.Connection.Execute(sql, new
                    {
                        ProcessID = ProcessID,
                        SetupWorksheetItemID = SetupWorksheetItemID,
                        ToolNumber = ToolNumber,
                        ToolName = ToolName,
                        ToolDetails = ToolDetails

                    });
                    break;
                case "tooldetails":
                    sql = @"UPDATE [SetupWorksheetItems] 
                                    SET [ToolNumber] = @ToolNumber, [ToolName] = @ToolName, [ToolDetails] = @ToolDetails WHERE [ProcessID] = @ProcessID AND [SetupWorksheetItemID] = @SetupWorksheetItemID";

                    this.Uw.Connection.Execute(sql, new
                    {
                        ProcessID = ProcessID,
                        SetupWorksheetItemID = SetupWorksheetItemID,
                        ToolNumber = ToolNumber,
                        ToolName = ToolName,
                        ToolDetails = ToolDetails

                    });
                    break;
            }
        }

        public void UpdateInspectionMeasurement(int jobItemId, int serialNumId, int dimension, string column, string m1, string m2, string m3, string final, string remarks, string critical, string employeelogin)
        {
            string sql;
            int employeeabbr = this.Uw.Context.Query<Scalar<int>>(@"SELECT EmployeeID As Value FROM Employees WHERE WindowsAuthLogin = @employeelogin", new { employeelogin = employeelogin }).SingleOrDefault().Value;
            int revisionID = this.Uw.Context.Query<Scalar<int>>(@"SELECT [Active Version] As Value FROM [Job Item] WHERE [JobItemID] = @jobitemid", new { jobitemid = jobItemId }).SingleOrDefault().Value;
            Int32 crit = (critical == "checked") ? 0 : 1;
            if (serialNumId == 0)
            {
                switch (column)
                {
                    case "measurement1":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [M1Empl] = @Employee, [M1Date] = @Date WHERE [JobItemID] = @JobItemId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            JobItemId = jobItemId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "measurement2":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [M2Empl] = @Employee, [M2Date] = @Date WHERE [JobItemID] = @JobItemId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            JobItemId = jobItemId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "measurement3":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [M3Empl] = @Employee, [M3Date] = @Date WHERE [JobItemID] = @JobItemId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            JobItemId = jobItemId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "final":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [FEmpl] = @Employee, [FDate] = @Date WHERE [JobItemID] = @JobItemId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            JobItemId = jobItemId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "remarks":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks WHERE [JobItemID] = @JobItemId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            JobItemId = jobItemId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,


                        });
                        break;

                    case "critical":
                        sql = @"UPDATE InspRecordsNew SET Critical=@Critical WHERE RevisionID=@revision AND DimNumber=@dimension";

                        this.Uw.Connection.Execute(sql, new { revision = revisionID, Dimension = dimension, Critical = crit });
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (column)
                {
                    case "measurement1":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [M1Empl] = @Employee, [M1Date] = @Date WHERE [SerialNumID] = @SerialNumId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            SerialNumId = serialNumId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "measurement2":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [M2Empl] = @Employee, [M2Date] = @Date WHERE [SerialNumID] = @SerialNumId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            SerialNumId = serialNumId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "measurement3":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [M3Empl] = @Employee, [M3Date] = @Date WHERE [SerialNumID] = @SerialNumId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            SerialNumId = serialNumId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "final":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks, [FEmpl] = @Employee, [FDate] = @Date WHERE [SerialNumID] = @SerialNumId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            SerialNumId = serialNumId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,
                            Employee = employeeabbr,
                            Date = DateTime.Now
                        });
                        break;

                    case "remarks":
                        sql = @"UPDATE [InspectionItems] 
                                    SET [Measurement1] = @M1, [Measurement2] = @M2, [Measurement3] = @M3, [FinalMeasurement] = @Final, 
                                        [Remarks] = @Remarks WHERE [SerialNumID] = @SerialNumId AND [Dimension] = @Dimension";

                        this.Uw.Connection.Execute(sql, new
                        {
                            SerialNumId = serialNumId,
                            Dimension = dimension,
                            M1 = m1,
                            M2 = m2,
                            M3 = m3,
                            Final = final,
                            Remarks = remarks,


                        });
                        break;

                    case "critical":
                        sql = @"UPDATE InspRecordsNew SET Critical=@Critical WHERE RevisionID=@revision AND DimNumber=@dimension";

                        this.Uw.Connection.Execute(sql, new { revision = revisionID, Dimension = dimension, Critical = crit });
                        break;

                    default:
                        break;
                }
            }
            
        }
		
	}
}
