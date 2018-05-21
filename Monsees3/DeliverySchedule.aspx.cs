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
    public partial class _Delivery : System.Web.UI.Page
    {
        private string MonseesConnectionString;
       
        private Int32 index;
        
       

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
           
               
                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                MonseesSqlDataSourceDel.ConnectionString = MonseesConnectionString;
              
                
             
          
           
        }

        protected void DeliveryViewGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    index = e.Row.RowIndex;
                    string DeliveryID = DeliveryViewGrid.DataKeys[index].Values[0].ToString();
                    GridView LotViewGrid = e.Row.FindControl("LotViewGrid") as GridView;
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    MonseesSqlDataSourceLots.ConnectionString = MonseesConnectionString;
                    MonseesSqlDataSourceLots.SelectCommand = "SELECT [LotNumber], [Quantity], [JobNumber], [RTS], [PCert], [MCert] FROM [FormLots] WHERE DeliverID=" + DeliveryID;
                    LotViewGrid.DataSource = MonseesSqlDataSourceLots;
                    LotViewGrid.DataBind();
                }
                catch (Exception f)
                {

                }
            }

        }


        protected void ProductionViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
           
 
        }



        protected void DeliveryViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            GridView gv;
            string DeliveryID;

            string command_name = e.CommandName;

            if ((command_name == "GetFile"))
            {
                Int32 totrows = DeliveryViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {



                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = clickedRow.Cells[6].Text;
                        RevNumber = clickedRow.Cells[7].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
                        break;
                    default:

                        break;

                }
            }
        }

        protected void DeliveryViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


        protected void LotViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;

            string command_name = e.CommandName;
            GridView view = sender as GridView;



            Int32 totrows = DeliveryViewGrid.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;

            //TO DO: Check to see if the user is already logged into the given job

            switch (e.CommandName)
            {
                case "ViewReport":

                    gvRow = view.Rows[index];
                    LotID = gvRow.Cells[0].Text;
                    //Check to see if user is already logged in
                    //MessageBox("The index fired is " + index);
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                    System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                    con3.Open();

                    System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("CreateInspection", con3);
                    comm3.CommandType = CommandType.StoredProcedure;


                    comm3.Parameters.AddWithValue("@JobItemID", LotID);

                    comm3.ExecuteNonQuery();

                    con3.Close();
                    string pageName = "InspectionReportPrint.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");
                    break;
                case "Print Report":

                    gvRow = view.Rows[index];
                    LotID = gvRow.Cells[0].Text;
                    //Check to see if user is already logged in
                    //MessageBox("The index fired is " + index);
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                    System.Data.SqlClient.SqlConnection con6 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                    con6.Open();

                    System.Data.SqlClient.SqlCommand comm6 = new System.Data.SqlClient.SqlCommand("CreateInspection", con6);
                    comm6.CommandType = CommandType.StoredProcedure;


                    comm6.Parameters.AddWithValue("@JobItemID", LotID);

                    comm6.ExecuteNonQuery();

                    con6.Close();
                    string pageNameprt = "InspectionReportPrint.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?JobItemID=" + LotID + "');</script>");
                    break;
                case "Close":
                    gvRow = view.Rows[index];
                    LotID = gvRow.Cells[0].Text;
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                    con.Open();
                    int result;
                    string SqlStr = "UPDATE [Job Item] SET [IsOpen] = False WHERE JobItemID = @LotID; UPDATE DeliveryItem SET RTS = True WHERE LotNumber = @LotID";
                    System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(SqlStr, con);
                    comm2.CommandType = CommandType.Text;


                    comm2.Parameters.AddWithValue("@LotID", LotID);

                    result = comm2.ExecuteNonQuery();


                    break;

                default:

                    break;

            }

        }


        
       
        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }
      
        


   
    }
}
