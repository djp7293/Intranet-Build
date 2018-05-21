using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BasicFrame.WebControls;
using Monsees.Security;
using Monsees.DataModel;
using Monsees.Database;
using Dapper;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using Monsees.Data;
using Monsees.Pages;

namespace Monsees
{
	public partial class ToolingSupplyPO : DataPage
	{
        public Int32 POID = 0;
        public List<QBAccountListModel> QBAccountList { get; set; }
        public List<LotListModel> LotList { get; set; }
        public List<ActiveJobListModel> JobList { get; set; }
        public List<EmployeeModel> EmployeeList { get; set; }
        public List<VendorListModel> VendorList { get; set; }
        public List<ShipMethod> ShipMethods { get; set; }
        bool posted = false;

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                Int32.TryParse(Request["POID"], out POID);
                ViewState["POID"] = POID;
            }
            GetData();
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            if ((int)ViewState["POID"] == 0)
            {
                CreateItemsViewMulti.ActiveViewIndex = 1;
                CreateViewMulti.ActiveViewIndex = 1;
                int[] nos = new int[1];
                for (int i = 0; i < nos.Length; i++)
                    nos[i] = i + 1;
                if (!IsPostBack)
                {
                    FormView1.DataSource = nos;
                    FormView1.DataBind();
                }

            }
            else
            {
                string sqlstring = "SELECT PostedToQB FROM SupplyOrders WHERE SuppliesPONum =  " + (int)ViewState["POID"] + ";";
                // create a connection with sqldatabase 
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                // create a sql command which will user connection string and your select statement string
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                System.Data.SqlClient.SqlDataReader reader;
                // open a connection with sqldatabase
                con.Open();
               
                // execute sql command and store a return values in reade
                reader = comm.ExecuteReader();

                while (reader.Read())
                {

                    posted = Convert.ToBoolean(reader["PostedToQB"].ToString());


                }

                if (posted) GridView1.AutoGenerateEditButton = false;

                con.Close();
                // Check if the user is already logged in or not
                if (!IsPostBack)
                {
                    ToolingPurchOrder.SelectCommand = "SELECT * FROM [SupplyOrderPOs] WHERE SuppliesPONum=" + (int)ViewState["POID"];
                    ListView2.DataSource = ToolingPurchOrder;
                    ListView2.DataBind();
                    ToolingPOLineItems.SelectCommand = "SELECT *, CAST([LotID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [ToolingSupplyItems] WHERE SuppliesPONum = " + (int)ViewState["POID"];
                    GridView1.DataSource = ToolingPOLineItems;
                    GridView1.DataBind();
                }
            }
            

		}

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            QBAccountList = inspectionRepository.GetQBAccounts();
            LotList = inspectionRepository.GetLotDescription();
            JobList = inspectionRepository.GetActiveJobs();
            ShipMethods = inspectionRepository.GetShipMethods();
            EmployeeList = inspectionRepository.GetActiveEmployees();
            VendorList = inspectionRepository.GetVendors();
            this.UnitOfWork.End();
        }

        protected string PreventUnlistedValueError(DropDownList li, string val)
        {
            if (li.Items.FindByValue(val) == null)
            {
                //option 1: add the value to the list and display
                if (val == "") val = "0";
                ListItem lit = new ListItem();
                lit.Text = val;
                lit.Value = val;
                li.Items.Insert(li.Items.Count, lit);
                //option 2: set a default e.g.
                //val="";
            }
            return val;
        }
        
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            ToolingPOLineItems.SelectCommand = "SELECT *, CAST([LotID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [ToolingSupplyItems] WHERE SuppliesPONum = " + (int)ViewState["POID"];
            GridView1.DataSource = ToolingPOLineItems;
            gvwChild.DataBind();

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = -1;

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            DropDownList AccountList = (DropDownList)gvrow.FindControl("AccountList");
            DropDownList LotList = (DropDownList)gvrow.FindControl("LotList");
            DropDownList JobList = (DropDownList)gvrow.FindControl("JobList");
            TextBox LineItemBox = (TextBox)gvrow.FindControl("LineItemBox");
            TextBox EachBox = (TextBox)gvrow.FindControl("EachBox"); 
            TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");

            TextBox TotalBox = (TextBox)gvrow.FindControl("TotalBox");
            TextBox ItemBox = (TextBox)gvrow.FindControl("ItemNumBox");
            TextBox NotesBox = (TextBox)gvrow.FindControl("NotesBox");
            TextBox DescriptionBox = (TextBox)gvrow.FindControl("DescriptionBox");
            BDPLite DueDateBox = (BDPLite)gvrow.FindControl("DueDateBox");
            string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);
            
                

            con.Open();
            int result;
            string UpdateQuery = "UPDATE SupplyOrderItems SET JobID=@JobID, LotID=@LotID, LineItem=@LineItem, ItemNum=@Item, Description=@Desc, Notes=@Notes, DueDate=@DueDate, PrEach=@Each, Quantity=@Qty, Total=@Total, QBAccount=@Account WHERE SuppliesPOItemID = @SuppliesPOItemID";
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);

            comm2.Parameters.AddWithValue("@JobID", Convert.ToInt32(JobList.SelectedValue));
            comm2.Parameters.AddWithValue("@LotID", Convert.ToInt32(LotList.SelectedValue));
            comm2.Parameters.AddWithValue("@LineItem", LineItemBox.Text.Trim());
            comm2.Parameters.AddWithValue("@Desc", DescriptionBox.Text.Trim());
            comm2.Parameters.AddWithValue("@Qty", Convert.ToInt32(QtyBox.Text));
            comm2.Parameters.AddWithValue("@Each", Convert.ToDouble(EachBox.Text));
            comm2.Parameters.AddWithValue("@SuppliesPOItemID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate));
            comm2.Parameters.AddWithValue("@Item", ItemBox.Text);
            comm2.Parameters.AddWithValue("@Notes", NotesBox.Text.Trim());
            comm2.Parameters.AddWithValue("@Total", Convert.ToDouble(TotalBox.Text));
            comm2.Parameters.AddWithValue("@Account", AccountList.SelectedValue.ToString().Trim());

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


            ToolingPOLineItems.SelectCommand = "SELECT *, CAST([LotID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [ToolingSupplyItems] WHERE SuppliesPONum = " + (int)ViewState["POID"];
            GridView1.DataSource = ToolingPOLineItems;
            gvwChild.DataBind();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = -1;


            ToolingPOLineItems.SelectCommand = "SELECT *, CAST([LotID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [ToolingSupplyItems] WHERE SuppliesPONum = " + (int)ViewState["POID"];
            GridView1.DataSource = ToolingPOLineItems;
            gvwChild.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            if (i > -1)
            {


                dpl = (DropDownList)Row.FindControl("AccountList");
                BDPLite DueBox = (BDPLite)Row.FindControl("DueDateBox");
                if (dpl != null)
                {
                    dpl.DataSource = QBAccountList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hfaccount")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)Row.FindControl("LotList");
                if (dpl != null)
                {
                    dpl.DataSource = LotList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hflot")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)Row.FindControl("JobList");
                if (dpl != null)
                {
                    dpl.DataSource = JobList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hfjob")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                if (DueBox != null)
                {
                    string DueDate = string.IsNullOrEmpty(((HiddenField)Row.FindControl("hfDate")).Value.Trim()) ? Convert.ToString(DateTime.Today) : ((HiddenField)Row.FindControl("hfDate")).Value.Trim();
                    DueBox.SelectedDate = Convert.ToDateTime(DueDate);
                }

            }

        }

        protected void InitiateOrder_Click(object sender, EventArgs e)
        {
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            int[] nos = new int[Convert.ToInt32(txtNo.Text)];
            for (int i = 0; i < nos.Length; i++)
                nos[i] = i + 1;
            string sqlstring = "INSERT INTO Subcontract (ContractorID, IssueDate, ShipMethodID, EmployeeID, Description, Note, DueDate) VALUES (@VendorID, @IssueDate, @ShipMethod, @Employee, @Description, @Note, @DueDate);";

            DropDownList VendorSelectList = (DropDownList)FormView1.FindControl("VendorNameList");
            DropDownList ShipMethodList = (DropDownList)FormView1.FindControl("ShipMethodList");
            DropDownList EmployeeSelectList = (DropDownList)FormView1.FindControl("NameList");
            TextBox DescriptionBox = (TextBox)FormView1.FindControl("DescriptionBox");
            TextBox NoteBox = (TextBox)FormView1.FindControl("NotesBox");
            TextBox ConfirmationBox = (TextBox)FormView1.FindControl("ConfirmationBox");
            BDPLite DueDateBox = (BDPLite)FormView1.FindControl("DueDateBox");
            string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);



            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("AddToolingOrder", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@VendorID", Convert.ToInt32(VendorSelectList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@IssueDate", DateTime.Today);
            comm.Parameters.AddWithValue("@ShipMethod", Convert.ToInt32(ShipMethodList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@Employee", Convert.ToInt32(EmployeeSelectList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate));
            comm.Parameters.AddWithValue("@Description", DescriptionBox.Text);
            comm.Parameters.AddWithValue("@Note", NoteBox.Text);
            comm.Parameters.AddWithValue("@Confirmation", ConfirmationBox.Text);
            SqlParameter returnval = comm.Parameters.Add("@returnval", SqlDbType.Int);
            returnval.Direction = ParameterDirection.ReturnValue;

            con.Open();
            comm.ExecuteNonQuery();
            POID = (int)returnval.Value;
            con.Close();
            ViewState["POID"] = POID;
            CreateViewMulti.ActiveViewIndex = 0;
            Button1.Visible = true;
            ToolingPurchOrder.SelectCommand = "SELECT * FROM [SupplyOrderPOs] WHERE SuppliesPONum=" +(int)ViewState["POID"];
            ListView2.DataSource = ToolingPurchOrder;
            ListView2.DataBind();
            GridView2.DataSource = nos;
            GridView2.DataBind();

        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            FormView Form = (FormView)sender;
            DropDownList dpl;




            dpl = (DropDownList)Form.Row.FindControl("VendorNameList");
            BDPLite DueDateBox = (BDPLite)Form.Row.FindControl("DueDateBox");


            if (dpl != null)
            {
                dpl.DataSource = VendorList;
                dpl.DataBind();

            }

            dpl = (DropDownList)Form.Row.FindControl("NameList");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }

            dpl = (DropDownList)Form.Row.FindControl("ShipMethodList");
            if (dpl != null)
            {
                dpl.DataSource = ShipMethods;
                dpl.DataBind();

            }

            if (DueDateBox != null)
            {
                DueDateBox.SelectedDate = DateTime.Today;
            }



        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
           
            GridViewRow Row = e.Row;

                dpl = (DropDownList)Row.FindControl("AccountList");
                BDPLite DueBox = (BDPLite)Row.FindControl("DueDateBox");
                if (dpl != null)
                {
                    dpl.DataSource = QBAccountList;
                    dpl.DataBind();
                   
                }

                dpl = (DropDownList)Row.FindControl("LotList");
                if (dpl != null)
                {
                    dpl.DataSource = LotList;
                    dpl.DataBind();
                   
                }

                dpl = (DropDownList)Row.FindControl("JobList");
                if (dpl != null)
                {
                    dpl.DataSource = JobList;
                    dpl.DataBind();
                    
                }

                if (DueBox != null)
                {
                     DueBox.SelectedDate = DateTime.Today;
                }
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            foreach (GridViewRow gvrow in GridView2.Rows)
            {
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                DropDownList AccountList = (DropDownList)gvrow.FindControl("AccountList");
                DropDownList LotList = (DropDownList)gvrow.FindControl("LotList");
                DropDownList JobList = (DropDownList)gvrow.FindControl("JobList");
                TextBox LineItemBox = (TextBox)gvrow.FindControl("LineItemBox");
                TextBox EachBox = (TextBox)gvrow.FindControl("EachBox");
                TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");

                TextBox TotalBox = (TextBox)gvrow.FindControl("TotalBox");
                TextBox ItemBox = (TextBox)gvrow.FindControl("ItemNumBox");
                TextBox NotesBox = (TextBox)gvrow.FindControl("NotesBox");
                TextBox DescriptionBox = (TextBox)gvrow.FindControl("DescriptionBox");
                BDPLite DueDateBox = (BDPLite)gvrow.FindControl("DueDateBox");
                string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);



                con.Open();
                int result;
                string InsertQuery = "INSERT INTO SupplyOrderItems (SuppliesPONum, JobID, LotID, LineItem, ItemNum, Description, Notes, DueDate, PrEach, Quantity, Total, QBAccount) VALUES (@POID, @JobID, @LotID, @LineItem, @Item, @Desc, @Notes, @DueDate, @Each, @Qty, @Total, @Account)";
                System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(InsertQuery, con);

                comm2.Parameters.AddWithValue("@JobID", Convert.ToInt32(JobList.SelectedValue));
                comm2.Parameters.AddWithValue("@LotID", Convert.ToInt32(LotList.SelectedValue));
                comm2.Parameters.AddWithValue("@LineItem", LineItemBox.Text.Trim());
                comm2.Parameters.AddWithValue("@Desc", DescriptionBox.Text.Trim());
                comm2.Parameters.AddWithValue("@Qty", Convert.ToInt32(QtyBox.Text));
                comm2.Parameters.AddWithValue("@Each", Convert.ToDouble(EachBox.Text));
                comm2.Parameters.AddWithValue("@POID", (int)ViewState["POID"]);
                comm2.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate));
                comm2.Parameters.AddWithValue("@Item", ItemBox.Text);
                comm2.Parameters.AddWithValue("@Notes", NotesBox.Text.Trim());
                comm2.Parameters.AddWithValue("@Total", Convert.ToDouble(TotalBox.Text));
                comm2.Parameters.AddWithValue("@Account", AccountList.SelectedValue.ToString().Trim());

               
                result = comm2.ExecuteNonQuery();
                con.Close();

            }


            ToolingPOLineItems.SelectCommand = "SELECT *, CAST([LotID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [ToolingSupplyItems] WHERE SuppliesPONum = " + (int)ViewState["POID"];
            GridView1.DataSource = ToolingPOLineItems;
            GridView1.DataBind();
            CreateItemsViewMulti.ActiveViewIndex = 0;

        }
	}


}