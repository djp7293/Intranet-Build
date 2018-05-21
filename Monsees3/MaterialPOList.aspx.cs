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
using System.Text;
using BasicFrame.WebControls;

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
    public partial class _Default_Orders : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        
        private Int32 index;
        private DataTable dt = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSourceItem.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourcePO.ConnectionString = MonseesConnectionString;
       

        }


    

        protected void ItemViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string POIDVal;
            
            string command_name = e.CommandName;

            if ((command_name == "ViewPO") || (command_name == "ViewPOSupp") || (command_name == "ViewPOSub") || (command_name == "Attach") || (command_name == "Alloc"))
            {
                Int32 totrows = ItemViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job
                
                switch (e.CommandName)
                {
                    case "ViewPO":
                        gvRow = ItemViewGrid.Rows[index];
                        POIDVal = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('MaterialPO.aspx?Type=0&POID=" + POIDVal + "','_blank');</script>");

                        break;

                    case "ViewPOSub":
                        gvRow = ItemViewGrid.Rows[index];
                        POIDVal = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('MaterialPO.aspx?Type=1&POID=" + POIDVal + "','_blank');</script>");

                        break;

                    case "ViewPOSupp":
                        gvRow = ItemViewGrid.Rows[index];
                        POIDVal = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('MaterialPO.aspx?Type=2&POID=" + POIDVal + "','_blank');</script>");

                        break;


                    case "Attach":

                        
                        break;

                    case "Alloc":
                        

                        break;

                    
                    default:

                        break;

                }
            }
        }

        protected void OrderViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string POIDVal;

            string command_name = e.CommandName;

            if ((command_name == "ViewPO") || (command_name == "ViewPOSub") || (command_name == "ViewPOSupp"))
            {
                Int32 totrows = OrderViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewOps":
                        gvRow = OrderViewGrid.Rows[index];
                        POIDVal = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('MaterialPO.aspx?Type=0,POID=" + POIDVal + "','_blank');</script>");

                        break;
                    case "ViewPOSub":
                        gvRow = OrderViewGrid.Rows[index];
                        POIDVal = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('MaterialPO.aspx?Type=1&POID=" + POIDVal + "','_blank');</script>");

                        break;

                    case "ViewPOSupp":
                        gvRow = OrderViewGrid.Rows[index];
                        POIDVal = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('MaterialPO.aspx?Type=2&POID=" + POIDVal + "','_blank');</script>");

                        break;


                    default:

                        break;

                }
            }
        }

        
        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }


        protected void ItemViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = OrdersMultiView.ActiveViewIndex;

            if (CurrentView != 0)
            {
                OrderViewGrid.DataBind();
                
                OrdersMultiView.SetActiveView(POItems);

            }
            
        }

        protected void MaterialPOListCtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonseesSqlDataSourceItem.FilterParameters["MaterialPOFilter"].DefaultValue = ((DropDownList)sender).SelectedValue;
        }

        protected void ToolingSupplyPOListCtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonseesSqlDataSourceItem.FilterParameters["ToolingSupplyPOFilter"].DefaultValue = ((DropDownList)sender).SelectedValue;
        }   

        protected void OrderViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = OrdersMultiView.ActiveViewIndex;

            if (CurrentView != 1)
            {
                ItemViewGrid.DataBind();

                OrdersMultiView.SetActiveView(Orders);

            }

        }

       /* protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool toggle = false;
            StringBuilder query = new StringBuilder("SELECT * from ToolingSupplyItems");

            if (!String.IsNullOrEmpty(DropDownList3.SelectedValue))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  JobID = " + DropDownList3.SelectedValue.ToString());
                }
            }

            if (!String.IsNullOrEmpty(DropDownList2.SelectedValue))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  VendorID = " + DropDownList2.SelectedValue.ToString());
                }
                else query.Append(" And VendorID = " + DropDownList2.SelectedValue.ToString());
            }

            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  QBAccount = '" + DropDownList1.SelectedValue.ToString() + "'");
                }
                else query.Append(" And  QBAccount = '" + DropDownList1.SelectedValue.ToString() + "'");
            }

            if (DeliveryFirst.SelectedValue != null)
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  Date >= '" + DeliveryFirst.SelectedValue.ToString() + "'");
                }
                else query.Append(" And  Date >= '" + DeliveryFirst.SelectedValue.ToString() + "'");
            }

            if (DeliveryLast.SelectedValue != null)
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  Date <= '" + DeliveryLast.SelectedValue.ToString() + "'");
                }
                else query.Append(" And  Date <= '" + DeliveryLast.SelectedValue.ToString() + "'");
            }

            MonseesSqlDataSourceItem.SelectCommand = query.ToString();
            ItemViewGrid.DataBind();

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            bool toggle = false;
            StringBuilder query= new StringBuilder("SELECT * from ToolingSupplyItems");

            if (!String.IsNullOrEmpty(DropDownList3.SelectedValue))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  JobID = " + DropDownList3.SelectedValue.ToString());                
                }
            }

            if (!String.IsNullOrEmpty(DropDownList2.SelectedValue))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  VendorID = " + DropDownList2.SelectedValue.ToString());
                }
                else query.Append(" And VendorID = " + DropDownList2.SelectedValue.ToString());
            }

            if (!String.IsNullOrEmpty(DropDownList1.SelectedValue))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  QBAccount = '" + DropDownList1.SelectedValue.ToString() + "'");
                }
                else query.Append(" And  QBAccount = '" + DropDownList1.SelectedValue.ToString() + "'");
            }

            if (DeliveryFirst.SelectedValue != null)
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  Date = " + DeliveryFirst.SelectedValue.ToString());
                }
                else query.Append(" And  Date = " + DeliveryLast.SelectedValue.ToString());
            }

            if (DeliveryLast.SelectedValue != null)
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  Date = " + DropDownList1.SelectedValue.ToString());
                }
                else query.Append(" And  Date = " + DropDownList1.SelectedValue.ToString());
            }

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            string queryStr = query.ToString();                              
            SqlDataAdapter sda = new SqlDataAdapter(queryStr, conn);
            sda.Fill(dt);
            ExportTableData(dt);
        }*/

        // this does all the work to export to excel
        public void ExportTableData(DataTable dtdata)
        {
            string attach = "attachment;filename=toolingsupplypoitemexport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attach);
            Response.ContentType = "application/ms-excel";
            if (dtdata != null)
            {
                foreach (DataColumn dc in dtdata.Columns)
                {
                    Response.Write(dc.ColumnName + "\t");
                    //sep = ";";
                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dtdata.Rows)
                {
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
    }
}
 
