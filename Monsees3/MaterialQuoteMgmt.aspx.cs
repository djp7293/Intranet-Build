using System;
using System.IO;
using System.Collections;
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
using Monsees.Security;

// ActiveJobs
// D.S.Harmor
// Description - View of all Active Production Jobs allowing users to log in and out of Jobs
// 08/19/2011 - Initial Version
// 
// Known Issues
//-------------------
// 08/19/2011 - LoggedInViewGrid ProcessID field should be invisible but when it is the value is unable to be read.

namespace Monsees
{
    public partial class _Default_MatlQuoteMgmt : System.Web.UI.Page
    {

        protected Int32 index;
        protected string MonseesConnectionString;
        protected string[] EmployeeLoginName;
        protected string EmployeeName;
        protected Int32 EmployeeID;

        protected void Page_Load(object sender, EventArgs e)
        {

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;


            EmployeeLoginName = User.Identity.Name.Split('\\');

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string sqlstring = "Select [EmployeeID], [Name] FROM [Employees] WHERE [WindowsAuthLogin] = '" + EmployeeLoginName[1] + "';";
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

                EmployeeID = Convert.ToInt32(reader["EmployeeID"].ToString());
                EmployeeName = reader["Name"].ToString();


            }
            con.Close();
            
           
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string MatQuoteID = "0";
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView GridView3 = e.Row.FindControl("GridView3") as GridView;
                MatQuoteID = GridView2.DataKeys[e.Row.RowIndex].Value.ToString();

                QuoteRespForm.SelectCommand = "SELECT [Vendor], [Line], [Material], [Dimension], [Each], [Total], [Delivery], [Note], [D], [H], [W], [L], [Qty], [Ship Chg] AS Ship_Chg, [Chosen], [Ordered], [MatQuoteID] FROM [MatQuoteRespForm] WHERE [MatQuoteID] = " + MatQuoteID;

                GridView3.DataSource = QuoteRespForm;
                GridView3.DataBind();
            }

        }

        protected void MaterialOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string sqlstring;


            Int32 totrows = MaterialOrders.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = MaterialOrders.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;


            if ((command_name == "Accept") || (command_name == "Reject") || (command_name == "Modify"))
            {


                switch (e.CommandName)
                {
                    case "Accept":
                        sqlstring = "UPDATE MaterialPO SET [ApprovalRecd]=1 WHERE [JobSetupID] = " + LinkID;

                        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);


                        con.Open();

                        // execute sql command and store a return values in reade
                        comm.ExecuteNonQuery();
                        con.Close();
                        MaterialOrders.DataBind();
                        break;

                    case "Reject":

                        break;

                    case "Modify":
                        string pageName = (Page.IsInMappedRole("Office")) ? "MaterialPOEdit.aspx" : "MaterialPO.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?POID=" + LinkID + "');</script>");
                        break;

                    default:

                        break;

                }
            }
        }

        protected void MaterialGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            bool check;



            string command_name = e.CommandName;

            if ((command_name == "Received"))
            {
                int index = Convert.ToInt32(e.CommandArgument);


                gvRow = MaterialGrid.Rows[index];
                string MatPriceID = gvRow.Cells[0].Text;

               // check = UpdateMaterialRecord(MatPriceID);




            }
        }

        /*protected bool UpdateMaterialRecord(string MatPriceID)
        {
            MonseesDB objMonseesDB;





            objMonseesDB = new MonseesDB();

            try
            {
                string sqlstring = @"--Use monsees2 
									declare @True bit,@False bit; select @True = 1, @False = 0;UPDATE Material_Price2 SET Received = 1 WHERE MatPriceID=" + MatPriceID.Trim();
                int result;

                result = objMonseesDB.ExecuteNonQuery(sqlstring);

                if (result == 1)
                {
                    MaterialGrid.DataSourceID = MonseesSqlDataSourceMaterial.ID;
                    MaterialGrid.DataBind();


                }

            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                objMonseesDB.Close();
            }

            return true;
        }*/

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string sqlstring;
            


            Int32 totrows = GridView1.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = GridView1.DataKeys[index].Values[0].ToString();
            bool orderpending = ((CheckBox)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[17].Controls[0]).Checked;
            string pageName = "";
            string command_name = e.CommandName;


            if ((command_name == "Quote") || (command_name == "Order") || (command_name == "Remove"))
            {


                switch (e.CommandName)
                {
                    case "Quote":
                        pageName = (Page.IsInMappedRole("Office")) ? "MaterialQuoteEdit.aspx" : "MaterialQuote.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?POID=" + LinkID + "');</script>");
                        break;

                    case "Order":
                        pageName = (Page.IsInMappedRole("Office")) ? "MaterialPOEdit.aspx" : "MaterialPO.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?POID=" + LinkID + "');</script>");
                        break;

                    case "Remove":
                        if(orderpending)
                        {
                            MessageBox("These is an order pending for this quote request item.  Execute the purchase order to remove this quote item from the list.");
                        }
                        else
                        {
                            sqlstring = "DELETE FROM MatQuoteItem WHERE [MatQueueID] = " + LinkID;

                            System.Data.SqlClient.SqlConnection con5 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                            System.Data.SqlClient.SqlCommand comm5 = new System.Data.SqlClient.SqlCommand(sqlstring, con5);

                            con5.Open();

                            comm5.ExecuteNonQuery();
                            con5.Close();
                            GridView1.DataBind();
                        }
                        
                        break;

                    default:

                        break;

                }
            }
        
        }

       
    }
}
