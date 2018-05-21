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
    public partial class SubcontractPO : DataPage
	{
        public Int32 POID=0;
        public Int32 createnew = 0;
        public List<LotListModel> LotList { get; set; }
        public List<WorkcodeModel> WorkCodeList { get; set; }
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
            if ((int)ViewState["POID"]==0)
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
                string sqlstring = "SELECT PostedToQB FROM [Subcontract Item] WHERE SubcontractID =  " + (int)ViewState["POID"] + ";";
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
                    SubPurchOrder.SelectCommand = "SELECT * FROM [SubcontractPOs] WHERE SubcontractID=" + (int)ViewState["POID"];
                    ListView2.DataSource = SubPurchOrder;
                    ListView2.DataBind();
                    SubPOLineItems.SelectCommand = "SELECT *, CAST([JobItemID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [SubcontractItems] WHERE SubcontractID = " + (int)ViewState["POID"];
                    GridView1.DataSource = SubPOLineItems;
                    GridView1.DataBind();
                }
            }

         
		}

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            LotList = inspectionRepository.GetLotDescription();
            WorkCodeList = inspectionRepository.GetWorkcodes();
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

            SubPOLineItems.SelectCommand = "SELECT *, CAST([JobItemID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM [SubcontractItems] WHERE SubcontractID = " + (int)ViewState["POID"];
            GridView1.DataSource = SubPOLineItems;
            gvwChild.DataBind();

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = -1;

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            DropDownList WorkCodeList = (DropDownList)gvrow.FindControl("WorkCodeList");
            DropDownList LotList = (DropDownList)gvrow.FindControl("LotList");
            
            TextBox EachBox = (TextBox)gvrow.FindControl("EachBox");
            TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");
            TextBox TotalBox = (TextBox)gvrow.FindControl("TotalBox");
            TextBox NotesBox = (TextBox)gvrow.FindControl("NotesBox");
            TextBox LineItemBox = (TextBox)gvrow.FindControl("LineItemBox");
            TextBox ShipChargeBox = (TextBox)gvrow.FindControl("ShipCostBox");
            BDPLite DueDateBox = (BDPLite)gvrow.FindControl("DueDateBox");
            
            BDPLite DateReturnedBox = (BDPLite)gvrow.FindControl("DateReturnedBox");
            string ReturnedDate = string.IsNullOrEmpty(Convert.ToString(DateReturnedBox.SelectedDate)) ? "1/1/2100" : Convert.ToString(DateReturnedBox.SelectedDate);
            string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);
                          

            con.Open();
            int result;
            string UpdateQuery = "UPDATE [Subcontract Item] SET JobItemID=@JobItemID, LineItem=@LineItem, Each=@Each, Quantity=@Qty, Total=@Total, DueDate=@DueDate, Notes=@Notes, DateReturned=@DateReturned, WorkCodeID=@WorkCode WHERE SubcontractItemID = @SubcontractItemID";
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);

            comm2.Parameters.AddWithValue("@WorkCode", Convert.ToInt32(WorkCodeList.SelectedValue));
            comm2.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(LotList.SelectedValue));
            comm2.Parameters.AddWithValue("@LineItem", LineItemBox.Text);
            comm2.Parameters.AddWithValue("@Each", Convert.ToDouble(EachBox.Text));
           
            comm2.Parameters.AddWithValue("@Qty", Convert.ToInt32(QtyBox.Text));
            comm2.Parameters.AddWithValue("@Total", Convert.ToDouble(TotalBox.Text));
            comm2.Parameters.AddWithValue("@SubcontractItemID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDateBox.SelectedDate));
            
            comm2.Parameters.AddWithValue("@DateReturned", Convert.ToDateTime(ReturnedDate));
            comm2.Parameters.AddWithValue("@Notes", NotesBox.Text);
            

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


            SubPOLineItems.SelectCommand = "SELECT *, CAST([JobItemID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM SubcontractItems WHERE SubcontractID = " + (int)ViewState["POID"];
            GridView1.DataSource = SubPOLineItems;
            gvwChild.DataBind();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = -1;


            SubPOLineItems.SelectCommand = "SELECT *, CAST([JobItemID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM SubcontractItems WHERE SubcontractID = " + (int)ViewState["POID"];
            GridView1.DataSource = SubPOLineItems;
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


                dpl = (DropDownList)Row.FindControl("WorkCodeList");
                BDPLite DueDateBox = (BDPLite)Row.FindControl("DueDateBox");
               
                BDPLite ReturnedBox = (BDPLite)Row.FindControl("DateReturnedBox");
                if (dpl != null)
                {
                    dpl.DataSource = WorkCodeList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hfworkcode")).Value.Trim();
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
               
                if(DueDateBox != null)
                {
                    string DueDate = string.IsNullOrEmpty(((HiddenField)Row.FindControl("hfDate")).Value.Trim())? Convert.ToString(DateTime.Today) : ((HiddenField)Row.FindControl("hfDate")).Value.Trim();
                    DueDateBox.SelectedDate = Convert.ToDateTime(DueDate);
                }
               
                if (ReturnedBox != null)
                {
                    string ReturnedDate = string.IsNullOrEmpty(((HiddenField)Row.FindControl("hfDateReturned")).Value.Trim()) ? "1/1/2100" : ((HiddenField)Row.FindControl("hfDateReturned")).Value.Trim();
                    ReturnedBox.SelectedDate = Convert.ToDateTime(ReturnedDate);
                }

            }

        }

        protected void EachBox_TextChanged(object sender, EventArgs e)
        {
            TextBox EachBox = (TextBox)sender;
            GridViewRow gvrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox TotalBox = (TextBox)gvrow.FindControl("TotalBox");
            TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");
            Int32 Qty = Convert.ToInt32(QtyBox.Text.ToString().Trim());
            Double Each = Convert.ToInt32(EachBox.Text.ToString().Trim());
            Double Total = Qty * Each;
            TotalBox.Text = Convert.ToString(Total);
        }

        protected void TotalBox_TextChanged(object sender, EventArgs e)
        {
            TextBox TotalBox = (TextBox)sender;
            GridViewRow gvrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            TextBox EachBox = (TextBox)gvrow.FindControl("EachBox");
            TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");
            Int32 Qty = Convert.ToInt32(QtyBox.Text.ToString().Trim());
            Double Total = Convert.ToInt32(TotalBox.Text.ToString().Trim());
            Double Each = Total / Qty;
            EachBox.Text = Convert.ToString(Each);
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
            BDPLite DueDateBox = (BDPLite)FormView1.FindControl("DueDateBox");
            string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);
             

            
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);            
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("AddSubOrder", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@VendorID", Convert.ToInt32(VendorSelectList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@IssueDate", DateTime.Today);
            comm.Parameters.AddWithValue("@ShipMethod", Convert.ToInt32(ShipMethodList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@Employee", Convert.ToInt32(EmployeeSelectList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate));
            comm.Parameters.AddWithValue("@Description", DescriptionBox.Text);
            comm.Parameters.AddWithValue("@Note", NoteBox.Text);
            SqlParameter returnval = comm.Parameters.Add("@returnval", SqlDbType.Int);
            returnval.Direction = ParameterDirection.ReturnValue;

            con.Open();
            comm.ExecuteNonQuery();
            POID = (int)returnval.Value;
            con.Close();
            ViewState["POID"] = POID;
            CreateViewMulti.ActiveViewIndex = 0;
            Button1.Visible = true;
            SubPurchOrder.SelectCommand = "SELECT * FROM [SubcontractPOs] WHERE SubcontractID=" + (int)ViewState["POID"];
            ListView2.DataSource = SubPurchOrder;
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
           

                dpl = (DropDownList)Row.FindControl("WorkCodeList");
                BDPLite DueDateBox = (BDPLite)Row.FindControl("DueDateBox");

                BDPLite ReturnedBox = (BDPLite)Row.FindControl("DateReturnedBox");
                if (dpl != null)
                {
                    dpl.DataSource = WorkCodeList;
                    dpl.DataBind();
                   
                }

                dpl = (DropDownList)Row.FindControl("LotList");
                if (dpl != null)
                {
                    dpl.DataSource = LotList;
                    dpl.DataBind();
                    
                }

                if (DueDateBox != null)
                {
                    DueDateBox.SelectedDate = DateTime.Today;
                }

               

            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvrow in GridView2.Rows)
            {                
                System.Data.SqlClient.SqlConnection con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                string insertquery = "INSERT INTO [Subcontract Item] (JobItemID, SubcontractID, LineItem, WorkCodeID, Each, DateSent, Notes, Quantity, DueDate, Total) VALUES (@JobItemID, @POID, @LineItem, @WorkCode, @Each, @DateSent, @Notes, @Qty, @DueDate, @Total);";
                System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(insertquery, con1);
                comm2.Parameters.Clear();
               
                DropDownList WorkCodeList = (DropDownList)gvrow.FindControl("WorkCodeList");
                DropDownList LotList = (DropDownList)gvrow.FindControl("LotList");

                TextBox EachBox = (TextBox)gvrow.FindControl("EachBox");
                TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");
                TextBox TotalBox = (TextBox)gvrow.FindControl("TotalBox");
                TextBox NotesBox = (TextBox)gvrow.FindControl("NotesBox");
                TextBox LineItemBox = (TextBox)gvrow.FindControl("LineItemBox");
                TextBox ShipChargeBox = (TextBox)gvrow.FindControl("ShipCostBox");
                BDPLite DueDateBox = (BDPLite)gvrow.FindControl("DueDateBox");
                BDPLite DateSentBox = (BDPLite)gvrow.FindControl("DateSentBox");

                
              
                string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);
                string DateSent = string.IsNullOrEmpty(Convert.ToString(DateSentBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DateSentBox.SelectedDate);


                con1.Open();
                int result;

                comm2.Parameters.AddWithValue("@WorkCode", Convert.ToInt32(WorkCodeList.SelectedValue));
                comm2.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(LotList.SelectedValue));
                comm2.Parameters.AddWithValue("@LineItem", LineItemBox.Text);
                comm2.Parameters.AddWithValue("@Each", Convert.ToDouble(EachBox.Text));

                comm2.Parameters.AddWithValue("@Qty", Convert.ToInt32(QtyBox.Text));
                comm2.Parameters.AddWithValue("@Total", Convert.ToDouble(TotalBox.Text));
                
                comm2.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDateBox.SelectedDate));
                comm2.Parameters.AddWithValue("@DateSent", DateTime.Today);
                POID = Convert.ToInt32(((Label)ListView2.Items[0].FindControl("SubcontractIDLabel")).Text.Trim());
                comm2.Parameters.AddWithValue("@POID", POID);
               
                comm2.Parameters.AddWithValue("@Notes", NotesBox.Text);
               
                result = comm2.ExecuteNonQuery();

                con1.Close();

                
            }
            SubPOLineItems.SelectCommand = "SELECT *, CAST([JobItemID] As nVarChar(50)) + ' - ' + [PartNumber] + ', ' + [DrawingNumber] + ' Rev.' + [Revision Number] As LotDescription FROM SubcontractItems WHERE SubcontractID = " + (int)ViewState["POID"];
            GridView1.DataSource = SubPOLineItems;
            GridView1.DataBind();
            CreateItemsViewMulti.ActiveViewIndex = 0;
        }
	}


}