using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using Monsees.Controls;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;

namespace Monsees
{
    public partial class NestedLoginCtrl : DataCtrl
    {
        private string MonseesConnectionString;
        public List<OperationDetailedModel> Setups { get; set; }
        public List<SetupFixturesModel> SetupFixtures { get; set; }
        public List<SetupLogHistoryModel> SetupHistory { get; set; }
        public List<SetupLogModel> SetupLogs { get; set; }
        public List<SetupEntriesModel> SetupEntries { get; set; }
        public List<EmployeeModel> Employees { get; set; }
        public List<OperationListModel> OperationList { get; set; }       
        public List<MachineModel> MachineList { get; set; }
        public Int32 EmployeeID;
        private string itemID;
        public string JobItemID;
        public string lot
        {

            get { return itemID; }

            set { itemID = value; }

        }
        private Int32 index;

        protected void Page_Load(object sender, EventArgs e)
        {

            JobItemID = itemID;
                GetData();
            
                //MainOperationsGrid.DataSource = Setups;
                //MainOperationsGrid.DataBind();

        }

        public void SetJobItem(string theText)
        {
            lot = theText;
        }

        private void GetData()
        {
            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            ActiveJobsRepository JobItemRepository = new ActiveJobsRepository(UnitOfWork);
            MachineList = inspectionRepository.GetMachines();
            Employees = inspectionRepository.GetActiveEmployees();
            OperationList = inspectionRepository.GetOperations();
            Setups = JobItemRepository.GetDetailedOperationsByJobItemId(Convert.ToInt32(lot));
            this.UnitOfWork.End();
        }

        public void setJobItem(string id)
        {
            JobItemID = id;
        }

        public void GetRowData1(int jobsetupid, int setupid)
        {
            GetRowData(jobsetupid, setupid);
        }

        private void GetRowData(int jobsetupid, int setupid)
        {
            this.UnitOfWork.Begin();
            
            ActiveJobsRepository JobItemRepository = new ActiveJobsRepository(UnitOfWork);
            SetupLogs = JobItemRepository.GetSetupLogBySetupID(jobsetupid);
            SetupFixtures = JobItemRepository.GetSetupFixturesBySetupID(setupid);
            SetupHistory = JobItemRepository.GetSetupLogHistoryBySetupID(setupid);
            SetupEntries = JobItemRepository.GetSetupEntriesBySetupID(setupid);
            this.UnitOfWork.End();
        }

        protected void LoadThumbnail(HttpContext context)
        {
            string imageid = context.Request.QueryString["ImID"];
            if (imageid == null || imageid == "")
            {
                //Set a default imageID
                imageid = "1";
            }
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select SetupImage from SetupImage where ImageID=" + imageid, connection);
            SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            Stream str = new MemoryStream((Byte[])dr[0]);

            Bitmap loBMP = new Bitmap(str);
            Bitmap bmpOut = new Bitmap(100, 100);

            Graphics g = Graphics.FromImage(bmpOut);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(Brushes.White, 0, 0, 100, 100);
            g.DrawImage(loBMP, 0, 0, 100, 100);

            MemoryStream ms = new MemoryStream();
            bmpOut.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bmpBytes = ms.GetBuffer();
            bmpOut.Dispose();
            ms.Close();
            context.Response.BinaryWrite(bmpBytes);
            connection.Close();
            context.Response.End();

        }

        protected void MainOperationsGrid_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            //KeepExpanded(gvwChild, sender);

        }

        protected void MainOperationsGrid_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;

            Int32 HoursValue;
            Int32 MachineValue;
            Int32 EmployeeValue;
            Int32 QtyInValue;
            Int32 QtyOutValue;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            TextBox Hours = (TextBox)gvrow.FindControl("Hours");
            TextBox QtyIn = (TextBox)gvrow.FindControl("QtyIn");
            TextBox QtyOut = (TextBox)gvrow.FindControl("QtyOut");
            DropDownList EmplID = (DropDownList)gvrow.FindControl("Empl");
            CheckBox Checked = (CheckBox)gvrow.FindControl("Completed");
            DropDownList MachineID = (DropDownList)gvwChild.FindControl("MachineList");
            CheckBox Fix = (CheckBox)gvwChild.FindControl("FixAdd");
            TextBox Description = (TextBox)gvwChild.FindControl("DescAdd");

            gvwChild.EditIndex = -1;

            //KeepExpanded(gvwChild, sender);


            if (QtyIn.Text.Trim() != "")
            {
                QtyInValue = Convert.ToInt32(QtyIn.Text);
            }
            else
            {
                QtyInValue = 0;
            };

            if (QtyOut.Text.Trim() != "")
            {
                QtyOutValue = Convert.ToInt32(QtyOut.Text);
            }
            else
            {
                QtyOutValue = 0;
            };

            if (Hours.Text.Trim() != "")
            {
                HoursValue = Convert.ToInt32(Hours.Text);
            }
            else
            {
                HoursValue = 0;
            };

            if (MachineID.SelectedValue != "")
            {
                MachineValue = Convert.ToInt32(MachineID.SelectedValue);
            }
            else
            {
                MachineValue = 0;
            };

            if (EmplID.SelectedValue != "")
            {
                EmployeeValue = Convert.ToInt32(EmplID.SelectedValue);
            }
            else
            {
                EmployeeValue = 0;
            };

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("MoveProcess", con);
            comm2.CommandType = CommandType.StoredProcedure;
            comm2.Parameters.AddWithValue("@QuantityIn", QtyInValue);
            comm2.Parameters.AddWithValue("@QuantityOut", QtyOutValue);
            comm2.Parameters.AddWithValue("@Hours", HoursValue);
            comm2.Parameters.AddWithValue("@Logout", DateTime.Now);
            comm2.Parameters.AddWithValue("@JobItemID", 0);
            comm2.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmplID.SelectedValue));
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@ProgramNum", "");
            comm2.Parameters.AddWithValue("@CheckMoveOn", Convert.ToBoolean(Checked.Checked));
            comm2.Parameters.AddWithValue("@MachineID", MachineValue);
            comm2.Parameters.AddWithValue("@Fix", Convert.ToBoolean(Fix.Checked));
            comm2.Parameters.AddWithValue("@Description", Description.Text);
            try
            {
                result = comm2.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                gvwChild.DataBind();
            }
        }

        protected void MainOperationsGrid_RowEditing(Object sender, GridViewEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = e.NewEditIndex;
            //KeepExpanded(gvwChild, sender);
        }

        protected void MainOperationsGrid_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = e.RowIndex;
            //KeepExpanded(gvwChild, sender);
        }

        protected string PreventUnlistedValueError(DropDownList li, string val)
        {
            if (li.Items.FindByValue(val) == null)
            {
                if (val == "") val = "0";
                ListItem lit = new ListItem();
                lit.Text = val;
                lit.Value = val;
                li.Items.Insert(li.Items.Count, lit);
            }
            return val;
        }

        /*protected void KeepExpanded(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            
        }

        private void BindChildgvwChildView(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            MainOperationsGridSource.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID, SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";
            gvChild.DataSource = MainOperationsGridSource;
            gvChild.DataBind();
        }*/

        protected void MainOperationsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            string JobSetupID = "0";
            string SetupID = "0";
            
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            GridView LogHoursGrid = (GridView)Row.FindControl("LogHoursGrid");
            GridView SetupFixtureOrders = (GridView)Row.FindControl("SetupFixtureOrders");
            GridView SetupHistoryGrid = (GridView)Row.FindControl("SetupHistoryGrid");
            GridView SetupEntries = (GridView)Row.FindControl("SetupEntries");
            DropDownList EmployeeCommentDrop = (DropDownList)Row.FindControl("EmployeeCommentDrop");

            if (Row.RowIndex > -1)
            {
                JobSetupID = Grid.DataKeys[Row.RowIndex].Values[0].ToString();
                SetupID = Grid.DataKeys[Row.RowIndex].Values[2].ToString();
                EmployeeCommentDrop.DataSource = EmployeeList;
                EmployeeCommentDrop.DataBind();
                EmployeeCommentDrop.SelectedValue = EmployeeID.ToString();
                if (SetupID != "")
                {
                    GetRowData(Convert.ToInt32(JobSetupID), Convert.ToInt32(SetupID));
                    
                    LogHoursGrid.DataSource = SetupLogs;
                    LogHoursGrid.DataBind();
                    SetupFixtureOrders.DataSource = SetupFixtures;
                    SetupFixtureOrders.DataBind();
                    SetupHistoryGrid.DataSource = SetupHistory;
                    SetupHistoryGrid.DataBind();
                    SetupEntries.DataSource = SetupEntries;
                    SetupEntries.DataBind();
                }
            }
        }

        protected void LogHoursGrid_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedLog(gvwChild, sender);
        }

        protected void LogHoursGrid_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;
            double HoursValue;
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            GridView gvParent = (GridView)gvwChild.Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow gvrowParent = ((GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            TextBox Hours = (TextBox)gvrow.FindControl("Hours");
            TextBox QtyIn = (TextBox)gvrow.FindControl("QtyIn");
            TextBox QtyOut = (TextBox)gvrow.FindControl("QtyOut");
            DropDownList EmplID = (DropDownList)gvrow.FindControl("Empl");
            CheckBox Checked = (CheckBox)gvrow.FindControl("MoveOn");
            DropDownList MachineID = (DropDownList)gvrow.FindControl("Machine");
            CheckBox Fix = (CheckBox)gvrow.FindControl("Fix");
            TextBox Description = (TextBox)gvrow.FindControl("ProcDesc");

            gvwChild.EditIndex = -1;

            KeepExpandedLog(gvwChild, sender);

            if (Hours.Text.Trim() != "")
            {
                HoursValue = Convert.ToDouble(Hours.Text);
            }
            else
            {
                HoursValue = 0;
            };

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("EditProcess", con);
            comm2.CommandType = CommandType.StoredProcedure;
            comm2.Parameters.AddWithValue("@QuantityIn", Convert.ToInt32(QtyIn.Text));
            comm2.Parameters.AddWithValue("@QuantityOut", Convert.ToInt32(QtyOut.Text));
            comm2.Parameters.AddWithValue("@Hours", Convert.ToInt32(Hours.Text));
            comm2.Parameters.AddWithValue("@Logout", DateTime.Now);
            comm2.Parameters.AddWithValue("@JobItemID", 0);
            comm2.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmplID.SelectedValue));
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Values[0].ToString()));
            comm2.Parameters.AddWithValue("@ProgramNum", "");
            comm2.Parameters.AddWithValue("@CheckMoveOn", Convert.ToBoolean(Checked.Checked));
            comm2.Parameters.AddWithValue("@ProcessID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Values[1].ToString()));
            //comm2.Parameters.AddWithValue("@Fix", Convert.ToBoolean(Fix.Checked));
            //comm2.Parameters.AddWithValue("@MachineID", Convert.ToInt32(MachineID.SelectedValue));
            // comm2.Parameters.AddWithValue("@Description", Description.Text);

            try
            {
                result = comm2.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                gvwChild.DataBind();
            }

            //ProductionViewGrid.DataBind();
            //KeepExpandedLogSub(gvwChild, sender);
            //string divtxt = "div" + gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString();
            //BindChildgvwChildLog(gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString(), gvwChild);
            //if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
            //}
            Formatting();
        }

        protected void LogHoursGrid_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            GridView gvParent = (GridView)gvwChild.Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow gvrowParent = ((GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            gvwChild.EditIndex = e.NewEditIndex;

            string divtxt = "div" + gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString();
            //BindChildgvwChildLog(gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString(), gvwChild);
            //if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
            //}
            Formatting();
        }

        protected void LogHoursGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;

            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;

            if (i > -1)
            {

                dpl = (DropDownList)Row.FindControl("Empl");
                if (dpl != null)
                {
                    dpl.DataSource = EmployeeList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdEmpl")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);

                }
                dpl = (DropDownList)Row.FindControl("Machine");
                if (dpl != null)
                {
                    dpl.DataSource = MachineList;
                    dpl.DataBind();
                    dpl.Items.Insert(0, "None");
                    string val = ((HiddenField)Row.FindControl("hdMach")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);

                }
            }

        }

        protected void AddOp_Command(object sender, EventArgs e)
        {
           
           
            //if (DropDownList2 != null)
            //{
            //    DropDownList2.DataSource = OperationList;
                //DropDownList2.DataBind();
//
            //}
           
            //if (EmployeeAddList != null)
            //{
            //    EmployeeAddList.DataSource = EmployeeList;
            //    EmployeeAddList.DataBind();

           // }

           // opsmultiview.ActiveViewIndex = 2;

            //BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), MainOperationsGrid);
            //HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            //object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            //div.Visible = true;
        }


        protected void CancelAddOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView MainOperationsGrid = gvRowParent.FindControl("MainOperationsGrid") as GridView;

            //BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), MainOperationsGrid);
            //HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            //object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            //div.Visible = true;
        }

        protected void AddNowOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView MainOperationsGrid = gvRowParent.FindControl("MainOperationsGrid") as GridView;
            string JobItemID = gvRowParent.Cells[2].Text;
            DropDownList Operation = (DropDownList)gvRowParent.FindControl("DropDownList2");
            TextBox SetupCostBox = (TextBox)gvRowParent.FindControl("TextBox3");
            TextBox OperationCostBox = (TextBox)gvRowParent.FindControl("TextBox4");
            TextBox OpCommentBox = (TextBox)gvRowParent.FindControl("OpCommentBox");
            DropDownList EmployeeAddList = (DropDownList)gvRowParent.FindControl("EmployeeAddList");
            TextBox OrderBox = (TextBox)gvRowParent.FindControl("RequestedOrderBox");
            string OperationID = Operation.SelectedValue.ToString();
            string SetupCost = SetupCostBox.Text;
            string OperationCost = OperationCostBox.Text;
            string description = OpCommentBox.Text;
            string createemployee = EmployeeAddList.SelectedValue.ToString();
            string order = OrderBox.Text;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand com = new SqlCommand("AddOperationtoLot", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@jobitemID", Convert.ToInt32(JobItemID));
            com.Parameters.AddWithValue("@operationID", Convert.ToInt32(OperationID));
            com.Parameters.AddWithValue("@setupcost", SetupCost);
            com.Parameters.AddWithValue("@operationcost", OperationCost);
            com.Parameters.AddWithValue("@description", description);
            com.Parameters.AddWithValue("@employee", createemployee);
            com.Parameters.AddWithValue("@ProcessOrder", Convert.ToInt32(order));

            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            //BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), MainOperationsGrid);
            //ProductionViewGrid.DataBind();
            //HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            //object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            ///div.Visible = true;
        }

        protected void LogNewNow_Command(object sender, CommandEventArgs e)
        {
            string MonseesConnectionString;

            double HoursValue;
            Int32 MachineValue;
            Int32 EmployeeValue;
            Int32 QtyInValue;
            Int32 QtyOutValue;
            Double RuntimeValue;
            int index;
            System.Web.UI.WebControls.GridViewRow gvwChild = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView gvChild = (GridView)gvwChild.Parent.Parent;
            GridView gvChildChild = (GridView)gvwChild.FindControl("LogHoursGrid");
            TextBox Hours = (TextBox)gvwChild.FindControl("HoursAdd");
            TextBox QtyIn = (TextBox)gvwChild.FindControl("QtyInAdd");
            TextBox QtyOut = (TextBox)gvwChild.FindControl("QtyOutAdd");
            TextBox Runtime = (TextBox)gvwChild.FindControl("RuntimeAdd");

            DropDownList EmplID = (DropDownList)gvwChild.FindControl("EmployeeList2");

            CheckBox Checked = (CheckBox)gvwChild.FindControl("MoveOn");
            DropDownList MachineID = (DropDownList)gvwChild.FindControl("MachineList");
            CheckBox Fix = (CheckBox)gvwChild.FindControl("FixAdd");
            TextBox Description = (TextBox)gvwChild.FindControl("DescAdd");

            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            //string JobItemID = "26922";
            GridView MainOperationsGrid = gvRowParent.FindControl("MainOperationsGrid") as GridView;

            if (QtyIn.Text.Trim() != "")
            {
                QtyInValue = Convert.ToInt32(QtyIn.Text);
            }
            else
            {
                QtyInValue = 0;
            };

            if (QtyOut.Text.Trim() != "")
            {
                QtyOutValue = Convert.ToInt32(QtyOut.Text);
            }
            else
            {
                QtyOutValue = 0;
            };

            if (Hours.Text.Trim() != "")
            {
                HoursValue = Convert.ToDouble(Hours.Text);
            }
            else
            {
                HoursValue = 0;
            };

            if (MachineID.SelectedValue != "")
            {
                MachineValue = Convert.ToInt32(MachineID.SelectedValue);
            }
            else
            {
                MachineValue = 0;
            };

            if (EmplID.SelectedValue != "")
            {
                EmployeeValue = Convert.ToInt32(EmplID.SelectedValue);
            }
            else
            {
                EmployeeValue = 0;
            };

            if (Runtime.Text.Trim() != "")
            {
                RuntimeValue = Convert.ToDouble(Runtime.Text);
            }
            else
            {
                RuntimeValue = 0;
            };

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("MoveProcessNew", con);
            comm2.CommandType = CommandType.StoredProcedure;
            comm2.Parameters.AddWithValue("@QuantityIn", QtyInValue);
            comm2.Parameters.AddWithValue("@QuantityOut", QtyOutValue);
            comm2.Parameters.AddWithValue("@Hours", HoursValue);
            comm2.Parameters.AddWithValue("@Runtime", RuntimeValue);
            comm2.Parameters.AddWithValue("@Logout", DateTime.Now);
            comm2.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(JobItemID));
            comm2.Parameters.AddWithValue("@EmployeeID", EmployeeValue);
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvChild.DataKeys[gvwChild.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@ProgramNum", "");
            comm2.Parameters.AddWithValue("@CheckMoveOn", Convert.ToBoolean(Checked.Checked));
            comm2.Parameters.AddWithValue("@MachineID", MachineValue);
            comm2.Parameters.AddWithValue("@Fix", Convert.ToBoolean(Fix.Checked));
            comm2.Parameters.AddWithValue("@Description", Description.Text);
            try
            {
                result = comm2.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                gvChildChild.DataBind();
            }
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            //BindChildgvwChildLog(gvChild.DataKeys[gvwChild.RowIndex].Values[0].ToString(), gvChildChild);
            //ProductionViewGrid.DataBind();
            //foreach (GridViewRow gr in ProductionViewGrid.Rows)
            //{
            //    if (ProductionViewGrid.DataKeys[gr.RowIndex].Value.ToString() == JobItemID) index = Convert.ToInt32(gr.RowIndex.ToString());
            //}
            //KeepExpandedLogSub(gvChildChild, sender);
            //KeepExpandedSetup(MainOperationsGrid, sender);
        }

        protected void MainOperationsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView MainOperationsGrid = (GridView)sender;
            GridViewRow gvRow;
            string JobSetupID;
            string JobItemID;
            string SetupID;

            Int32 totrows = MainOperationsGrid.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            gvRow = MainOperationsGrid.Rows[index];
            JobSetupID = MainOperationsGrid.DataKeys[index].Values[0].ToString();
            JobItemID = MainOperationsGrid.DataKeys[index].Values[1].ToString();
            SetupID = MainOperationsGrid.DataKeys[index].Values[2].ToString();

            switch (e.CommandName)
            {

                case "OrderFixture":
                    Response.Write("<script type='text/javascript'>window.open('AddFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
                    Formatting();
                    //KeepExpanded(MainOperationsGrid, sender);
                    break;

                case "QuickFixture":
                    Response.Write("<script type='text/javascript'>window.open('QuickFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
                    Formatting();
                    //KeepExpanded(MainOperationsGrid, sender);
                    break;

                case "OpenSetupSheet":
                    Response.Write("<script type='text/javascript'>window.open('SetupSheet.aspx?JobItemID=" + JobItemID + "&JobSetupID=" + JobSetupID + "&EmpID=" + EmployeeID + "','_blank');</script>");
                    Formatting();
                    //KeepExpanded(MainOperationsGrid, sender);
                    break;

                default:
                    break;
            }
        }

        protected void LogHoursGrid_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            string MonseesConnectionString;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            GridView gvParent = (GridView)gvwChild.Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            GridViewRow gvrowParent = ((GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            gvwChild.EditIndex = -1;

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            string UpdateQuery = "DELETE FROM Process WHERE ProcessID=@ProcessID";
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);
            comm2.Parameters.AddWithValue("@ProcessID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Values[1].ToString()));

            try
            {
                result = comm2.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                gvwChild.DataBind();
            }
            string divtxt = "div" + gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString();
            //BindChildgvwChildLog(gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString(), gvwChild);
            //if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
            //}
            Formatting();
        }


        protected void LogNew_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;

            GridView MainOperationsGrid = gvRowParent.FindControl("MainOperationsGrid") as GridView;
            GridViewRow LogHoursGrid = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            DropDownList dpl = (DropDownList)LogHoursGrid.FindControl("EmployeeList2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }
            dpl = (DropDownList)LogHoursGrid.FindControl("MachineList");
            if (dpl != null)
            {
                dpl.DataSource = MachineList;
                dpl.DataBind();

            }
            KeepExpandedSetup(MainOperationsGrid, sender);
        }

        protected void CancelLog_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView MainOperationsGrid = gvRowParent.FindControl("MainOperationsGrid") as GridView;
            KeepExpandedSetup(MainOperationsGrid, sender);

        }

        private void Formatting()
        {
            //for (int i = 0; i < ProductionViewGrid.Rows.Count; i++)
            //{


            //    if (Convert.ToInt32(((HiddenField)ProductionViewGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
            //    {
            //        ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

            //    }

            //    if (Convert.ToInt32(((HiddenField)ProductionViewGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
            //    {
            //        ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

            //    }


            //    if (((HiddenField)ProductionViewGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
            //    {
            //        ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

            //    }

            //    if (((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value != "")
            //    {
            //        string hot = ((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value;
            //        if (Convert.ToBoolean(((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value))
            //        {
            //            ProductionViewGrid.Rows[i].Font.Bold = true;

            //        }


            //    }
            //}
        }

        protected void SetupFixtureOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            HtmlGenericControl locdiv;
            HtmlGenericControl orderdiv;
            locdiv = (HtmlGenericControl)Row.FindControl("loclabeldiv");
            orderdiv = (HtmlGenericControl)Row.FindControl("loctextdiv");

            if (Row.RowIndex > -1)
            {
                if (String.IsNullOrEmpty(Grid.DataKeys[Row.RowIndex].Value.ToString()))
                {
                    locdiv.Visible = false;
                    orderdiv.Visible = true;
                }
                else
                {
                    locdiv.Visible = true;
                    orderdiv.Visible = false;
                }

            }
        }

        protected void FixtureCloseButton_Click(object sender, EventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView gvParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent2 = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParent3 = gvRowParent2.Parent.Parent.Parent.Parent as GridViewRow;


            string RevID = gvParent.DataKeys[gvRowParent.RowIndex].Values[1].ToString();
            TextBox Fixtloc = (TextBox)gvRowParent.FindControl("fixloctext");
            TextBox FixtNote = (TextBox)gvRowParent.FindControl("fixnotetext");
            string location = Fixtloc.Text;
            string sqlstring = "INSERT INTO FixtureInventory (RevisionID, Location, Note) VALUES (@FixtRevID, @location, @note)";

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand(sqlstring, con);

            com.Parameters.AddWithValue("@FixtRevID", Convert.ToInt32(RevID));
            com.Parameters.AddWithValue("@location", Fixtloc.Text);

            com.Parameters.AddWithValue("@note", FixtNote);

            con.Open();
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();

            con.Close();

            int indexparent = gvRowParent3.RowIndex;


            //HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
            //object button = (object)ProductionViewGrid.Rows[indexparent].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            //div.Visible = true;

            Formatting();
        }

        protected void LogHoursGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }



        protected void KeepExpandedLog(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = (gvwChild).Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParentParent = (gvwChild).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            int indexparent = gvRowParentParent.RowIndex;
            index = gvRowParent.RowIndex;
            //BindChildgvwChildLog(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            //HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
            //object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            //div.Visible = true;
            Formatting();

        }

        protected void KeepExpandedLogSub(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = (gvwChild).Parent.Parent.Parent.Parent as GridViewRow;

            GridViewRow gvRowParentParent = (gvwChild).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            int indexparent = gvRowParentParent.RowIndex;
            index = gvRowParentParent.RowIndex;
            //BindChildgvwChildLog(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            //HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
            //object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            //div.Visible = true;
            Formatting();

        }

        protected void KeepExpandedSetup(System.Web.UI.WebControls.GridView gvParent, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            string divtxt = "div" + gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString();
            //BindChildgvwChildView(MainOperationsGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            //if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
            //}
            Formatting();
        }

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }

    }
}