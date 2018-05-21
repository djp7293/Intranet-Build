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
	public partial class MaterialPO : DataPage
	{
        public Int32 POID = 0;
        public List<MaterialModel> MaterialList { get; set; }
        public List<DimensionModel> DimensionList { get; set; }
        public List<MaterialSizeModel> SizeList { get; set; }
        public List<EmployeeModel> EmployeeList { get; set; }
        public List<VendorListModel> VendorList { get; set; }
        public List<ShipMethod> ShipMethods { get; set; }
        public List<ContactModel> ContactList { get; set; }
        bool executed = false;

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
                string sqlstring = "SELECT MaterialPO.MaterialPOID, CAST(MIN(CAST(COALESCE (MatQuoteLine.Ordered, 1) AS Int)) AS Bit) AS Executed FROM MatQuoteLine RIGHT OUTER JOIN Material_Price2 ON MatQuoteLine.MatQuoteLineID = Material_Price2.MatQuoteLineID RIGHT OUTER JOIN MaterialPO ON Material_Price2.MaterialPOID = MaterialPO.MaterialPOID GROUP BY MaterialPO.MaterialPOID HAVING MaterialPO.MaterialPOID =  " + (int)ViewState["POID"] + ";";
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

                    executed = Convert.ToBoolean(reader["Executed"].ToString());


                }

                if (executed) GridView1.AutoGenerateEditButton = false;

                con.Close();
                // Check if the user is already logged in or not
                if (!IsPostBack)
                {
                    MatlPurchOrder.SelectCommand = "SELECT * FROM [MaterialPOs] WHERE MaterialPOID=" + (int)ViewState["POID"];
                    ListView2.DataSource = MatlPurchOrder;
                    ListView2.DataBind();
                    MatPOLineItems.SelectCommand = "SELECT [MatPriceID], [cost], [MaterialName], [Dimension], [Size], [Length], [quantity], [VendorName], [MaterialPOID], [MaterialDimID], [MaterialSizeID], [MaterialID], [DueDate], [ItemNum], [Shipping], [ShippingCharge], [ConfirmationNum], [ContactName], [received], [JobNumber], [MinOfMatlCertReqd] FROM [MaterialOrders2] WHERE MaterialPOID = " + (int)ViewState["POID"];
                    GridView1.DataSource = MatPOLineItems;
                    GridView1.DataBind();
                }
                
            }
		}

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            MaterialList = inspectionRepository.GetMaterials();
            DimensionList = inspectionRepository.GetDimensions();
            SizeList = inspectionRepository.GetMaterialSizes();
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

            MatPOLineItems.SelectCommand = "SELECT [MatPriceID], [cost], [MaterialName], [Dimension], [Size], [Length], [quantity], [VendorName], [MaterialPOID], [MaterialDimID], [MaterialSizeID], [MaterialID], [DueDate], [ItemNum], [Shipping], [ShippingCharge], [ConfirmationNum], [ContactName], [received], [JobNumber], [MinOfMatlCertReqd] FROM [MaterialOrders2] WHERE MaterialPOID = " + (int)ViewState["POID"];
            GridView1.DataSource = MatPOLineItems;
            gvwChild.DataBind();

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = -1;

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            DropDownList MaterialList = (DropDownList)gvrow.FindControl("MatlList");
            DropDownList DimensionList = (DropDownList)gvrow.FindControl("DimList");
            DropDownList SizeList = (DropDownList)gvrow.FindControl("SizeList");
            TextBox LengthBox = (TextBox)gvrow.FindControl("LengthBox");
            TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");
            TextBox CostBox = (TextBox)gvrow.FindControl("CostBox");
            TextBox ItemBox = (TextBox)gvrow.FindControl("ItemNumBox");
            TextBox ShippingBox = (TextBox)gvrow.FindControl("ShippingBox");
            TextBox ShipChargeBox = (TextBox)gvrow.FindControl("ShipCostBox");
            BDPLite DueDateBox = (BDPLite)gvrow.FindControl("DueDateBox");
            string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);
            
                

            con.Open();
            int result;
            string UpdateQuery = "UPDATE Material_Price2 SET MaterialID=@Material, MatDimID=@Dimension, MatSizeID=@Size, Length=@L, Quantity=@Qty, cost=@Cost, DueDate=@DueDate, ItemNum=@Item, Shipping=@Shipping, ShippingCharge=@ShipCharge WHERE MatPriceID = @MatPriceID";
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);

            comm2.Parameters.AddWithValue("@Material", Convert.ToInt32(MaterialList.SelectedValue));
            comm2.Parameters.AddWithValue("@Dimension", Convert.ToInt32(DimensionList.SelectedValue));
            comm2.Parameters.AddWithValue("@Size", Convert.ToInt32(SizeList.SelectedValue));
            comm2.Parameters.AddWithValue("@L", Convert.ToDouble(LengthBox.Text));
            comm2.Parameters.AddWithValue("@Qty", Convert.ToInt32(QtyBox.Text));
            comm2.Parameters.AddWithValue("@cost", Convert.ToDouble(CostBox.Text));
            comm2.Parameters.AddWithValue("@MatPriceID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate));
            comm2.Parameters.AddWithValue("@Item", ItemBox.Text);
            comm2.Parameters.AddWithValue("@Shipping", ShippingBox.Text.Trim());
            comm2.Parameters.AddWithValue("@ShipCharge", Convert.ToDouble(String.IsNullOrEmpty(ShipChargeBox.Text.Trim()) ? "0" : ShipChargeBox.Text.Trim()));
            

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


            MatPOLineItems.SelectCommand = "SELECT [MatPriceID], [cost], [MaterialName], [Dimension], [Size], [Length], [quantity], [VendorName], [MaterialPOID], [MaterialDimID], [MaterialSizeID], [MaterialID], [DueDate], [ItemNum], [Shipping], [ShippingCharge], [ConfirmationNum], [ContactName], [received], [JobNumber], [MinOfMatlCertReqd] FROM [MaterialOrders2] WHERE MaterialPOID = " + (int)ViewState["POID"];
            GridView1.DataSource = MatPOLineItems;
            gvwChild.DataBind();

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = -1;


            MatPOLineItems.SelectCommand = "SELECT [MatPriceID], [cost], [MaterialName], [Dimension], [Size], [Length], [quantity], [VendorName], [MaterialPOID], [MaterialDimID], [MaterialSizeID], [MaterialID], [DueDate], [ItemNum], [Shipping], [ShippingCharge], [ConfirmationNum], [ContactName], [received], [JobNumber], [MinOfMatlCertReqd] FROM [MaterialOrders2] WHERE MaterialPOID = " + (int)ViewState["POID"];
            GridView1.DataSource = MatPOLineItems;
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


                dpl = (DropDownList)Row.FindControl("MatlList");
                BDPLite DueBox = (BDPLite)Row.FindControl("DueDateBox");
                if (dpl != null)
                {
                    dpl.DataSource = MaterialList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hfMatl")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)Row.FindControl("DimList");
                if (dpl != null)
                {
                    dpl.DataSource = DimensionList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hfDim")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)Row.FindControl("SizeList");
                if (dpl != null)
                {
                    dpl.DataSource = SizeList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hfSize")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                if(DueBox != null)
                {
                    DueBox.SelectedDate = Convert.ToDateTime(((HiddenField)Row.FindControl("hfDate")).Value.Trim());
                }

            }

        }

        protected void InitiateOrder_Click(object sender, EventArgs e)
        {
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            int[] nos = new int[Convert.ToInt32(txtNo.Text)];
            for (int i = 0; i < nos.Length; i++)
                nos[i] = i + 1;
            
            DropDownList VendorSelectList = (DropDownList)FormView1.FindControl("VendorNameList");
            DropDownList ShipMethodList = (DropDownList)FormView1.FindControl("ShipMethodList");
            DropDownList EmployeeSelectList = (DropDownList)FormView1.FindControl("NameList");
            DropDownList ContactList = (DropDownList)FormView1.FindControl("ContactList");
            TextBox ShipChargeBox = (TextBox)FormView1.FindControl("ShipChargeBox");
            TextBox NoteBox = (TextBox)FormView1.FindControl("NotesBox");
            TextBox ConfirmationBox = (TextBox)FormView1.FindControl("ConfirmationBox");
            BDPLite DueDateBox = (BDPLite)FormView1.FindControl("DueDateBox");
            string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);



            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand("AddMatlOrder", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@VendorID", Convert.ToInt32(VendorSelectList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@IssueDate", DateTime.Today);
            comm.Parameters.AddWithValue("@ShipMethod", Convert.ToInt32(ShipMethodList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@Employee", Convert.ToInt32(EmployeeSelectList.SelectedValue.ToString()));
            comm.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate));
            comm.Parameters.AddWithValue("@ShipCharge", Convert.ToDouble(ShipChargeBox.Text.Trim()));
            comm.Parameters.AddWithValue("@Note", NoteBox.Text);
            comm.Parameters.AddWithValue("@Confirmation", ConfirmationBox.Text);
            comm.Parameters.AddWithValue("@Contact", Convert.ToInt32(ContactList.SelectedValue.ToString()));
            SqlParameter returnval = comm.Parameters.Add("@returnval", SqlDbType.Int);
            returnval.Direction = ParameterDirection.ReturnValue;

            con.Open();
            comm.ExecuteNonQuery();
            POID = (int)returnval.Value;
            con.Close();
            ViewState["POID"] = POID;
            CreateViewMulti.ActiveViewIndex = 0;
            Button1.Visible = true;
            MatlPurchOrder.SelectCommand = "SELECT * FROM [MaterialPOs] WHERE MaterialPOID=" + (int)ViewState["POID"];
            ListView2.DataSource = MatlPurchOrder;
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

            dpl = (DropDownList)Form.Row.FindControl("ContactList");
            if (dpl != null)
            {
                dpl.DataSource = ContactList;
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
            

                dpl = (DropDownList)Row.FindControl("MatlList");
                BDPLite DueBox = (BDPLite)Row.FindControl("DueDateBox");
                if (dpl != null)
                {
                    dpl.DataSource = MaterialList;
                    dpl.DataBind();
                   
                }

                dpl = (DropDownList)Row.FindControl("DimList");
                if (dpl != null)
                {
                    dpl.DataSource = DimensionList;
                    dpl.DataBind();
                   
                }

                dpl = (DropDownList)Row.FindControl("SizeList");
                if (dpl != null)
                {
                    dpl.DataSource = SizeList;
                    dpl.DataBind();
                   
                }

                dpl = (DropDownList)Row.FindControl("ShipMethodList");
                if (dpl != null)
                {
                    dpl.DataSource = ShipMethods;
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

                DropDownList MaterialList = (DropDownList)gvrow.FindControl("MatlList");
                DropDownList DimensionList = (DropDownList)gvrow.FindControl("DimList");
                DropDownList SizeList = (DropDownList)gvrow.FindControl("SizeList");
                TextBox LengthBox = (TextBox)gvrow.FindControl("LengthBox");
                TextBox QtyBox = (TextBox)gvrow.FindControl("QtyBox");
                TextBox CostBox = (TextBox)gvrow.FindControl("CostBox");
                TextBox ItemBox = (TextBox)gvrow.FindControl("ItemNumBox");
                DropDownList ShipMethodList = (DropDownList)gvrow.FindControl("ShipMethodList");
                TextBox ShipChargeBox = (TextBox)gvrow.FindControl("ShipCostBox");
                BDPLite DueDateBox = (BDPLite)gvrow.FindControl("DueDateBox");
                string DueDate = string.IsNullOrEmpty(Convert.ToString(DueDateBox.SelectedDate)) ? Convert.ToString(DateTime.Today) : Convert.ToString(DueDateBox.SelectedDate);

                con.Open();
                int result;
                string UpdateQuery = "INSERT INTO Material_Price2 (MaterialID, MaterialPOID, MatDimID, MatSizeID, Length, Quantity, cost, DueDate, ItemNum, Shipping, ShippingCharge) VALUES (@Material, @POID, @Dimension, @Size, @L, @Qty, @Cost, @DueDate, @Item, @Shipping, @ShipCharge);";
                System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);

                comm2.Parameters.AddWithValue("@Material", Convert.ToInt32(MaterialList.SelectedValue));
                comm2.Parameters.AddWithValue("@Dimension", Convert.ToInt32(DimensionList.SelectedValue));
                comm2.Parameters.AddWithValue("@Size", Convert.ToInt32(SizeList.SelectedValue));
                comm2.Parameters.AddWithValue("@L", Convert.ToDouble(LengthBox.Text));
                comm2.Parameters.AddWithValue("@Qty", Convert.ToInt32(QtyBox.Text));
                comm2.Parameters.AddWithValue("@cost", Convert.ToDouble(CostBox.Text));
                comm2.Parameters.AddWithValue("@POID", (int)ViewState["POID"]);
                comm2.Parameters.AddWithValue("@DueDate", Convert.ToDateTime(DueDate));
                comm2.Parameters.AddWithValue("@Item", ItemBox.Text);
                comm2.Parameters.AddWithValue("@Shipping", ShipMethodList.Text.Trim());
                comm2.Parameters.AddWithValue("@ShipCharge", Convert.ToDouble(String.IsNullOrEmpty(ShipChargeBox.Text.Trim()) ? "0" : ShipChargeBox.Text.Trim()));

                result = comm2.ExecuteNonQuery();
                con.Close();
                
            }
            
            MatPOLineItems.SelectCommand = "SELECT [MatPriceID], [cost], [MaterialName], [Dimension], [Size], [Length], [quantity], [VendorName], [MaterialPOID], [MaterialDimID], [MaterialSizeID], [MaterialID], [DueDate], [ItemNum], [Shipping], [ShippingCharge], [ConfirmationNum], [ContactName], [received], [JobNumber], [MinOfMatlCertReqd] FROM [MaterialOrders2] WHERE MaterialPOID = " + (int)ViewState["POID"]; 
            GridView1.DataSource = MatPOLineItems;
            GridView1.DataBind();
            CreateItemsViewMulti.ActiveViewIndex = 0;

        }

        protected void VendorNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
             this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            DropDownList CustomerList = (DropDownList)sender;
            DropDownList ContactDropList = (DropDownList)FormView1.FindControl("ContactList");
            Int32 CustomerID = Convert.ToInt32(CustomerList.SelectedValue.ToString().Trim());
            ContactList = inspectionRepository.GetContactsbySupplierId(CustomerID);
            
            if (ContactDropList != null)
            {
                ContactDropList.DataSource = ContactList;
                ContactDropList.DataBind();

            }
            this.UnitOfWork.End();
        }

	}


}