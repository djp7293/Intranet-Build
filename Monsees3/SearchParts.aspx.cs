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
    public partial class _Default_SearchParts : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        
        private Int32 index;
        private DataTable dt = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
          
                MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
                
                bool toggle = false;
                StringBuilder query = new StringBuilder("SELECT * from SearchParts");


                if (!String.IsNullOrEmpty(CompanyFilter.Text))
                {
                    if (toggle == false)
                    {
                        toggle = true;
                        query.Append(" WHERE  CompanyName LIKE '%" + CompanyFilter.Text + "%'");
                    }
                }

                if (!String.IsNullOrEmpty(PartFilter.Text))
                {
                    if (toggle == false)
                    {
                        toggle = true;
                        query.Append(" WHERE  LIKE '%" + PartFilter.Text + "%'");
                    }
                    else query.Append(" And PartNumber LIKE '%" + PartFilter.Text + "%'");
                }

                if (!String.IsNullOrEmpty(DescFilter.Text))
                {
                    if (toggle == false)
                    {
                        toggle = true;
                        query.Append(" WHERE  DrawingNumber = LIKE '%" + DescFilter.Text + "%'");
                    }
                    else query.Append(" And  DrawingNumber = LIKE '%" + DescFilter.Text + "%'");
                }

                query.Append(" ORDER BY PartNumber");
                MonseesSqlDataSource.SelectCommand = query.ToString();

        }


    

        protected void PartGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string DetailID;
            
            string command_name = e.CommandName;

            if ((command_name == "ViewHistory"))
            {
                Int32 totrows = PartGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job
                
                switch (e.CommandName)
                {
                    case "ViewHistory":
                        gvRow = PartGrid.Rows[index];
                        DetailID = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('PartHistory.aspx?Type=0&DetailID=" + DetailID + "','_blank');</script>");

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


       

        protected void MaterialPOListCtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonseesSqlDataSource.FilterParameters["MaterialPOFilter"].DefaultValue = ((DropDownList)sender).SelectedValue;
        }

        protected void ToolingSupplyPOListCtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonseesSqlDataSource.FilterParameters["ToolingSupplyPOFilter"].DefaultValue = ((DropDownList)sender).SelectedValue;
        }   

       

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool toggle = false;
            StringBuilder query = new StringBuilder("SELECT * from SearchParts");


            if (!String.IsNullOrEmpty(CompanyFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  CompanyName LIKE '%" + CompanyFilter.Text + "%'");
                }
            }

            if (!String.IsNullOrEmpty(PartFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE PartNumber LIKE '%" + PartFilter.Text + "%'");
                }
                else query.Append(" And PartNumber LIKE '%" + PartFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(DescFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
                }
                else query.Append(" And  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
            }

            query.Append(" ORDER BY PartNumber");

            MonseesSqlDataSource.SelectCommand = query.ToString();
            PartGrid.DataBind();

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            bool toggle = false;
            StringBuilder query = new StringBuilder("SELECT * from SearchParts");


            if (!String.IsNullOrEmpty(CompanyFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  CompanyName LIKE '%" + CompanyFilter.Text + "%'");
                }
            }

            if (!String.IsNullOrEmpty(PartFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE PartNumber LIKE '%" + PartFilter.Text + "%'");
                }
                else query.Append(" And PartNumber LIKE '%" + PartFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(DescFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
                }
                else query.Append(" And  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
            }
           

            query.Append(" ORDER BY PartNumber");
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            string queryStr = query.ToString();                              
            SqlDataAdapter sda = new SqlDataAdapter(queryStr, conn);
            sda.Fill(dt);
            ExportTableData(dt);
        }

        // this does all the work to export to excel
        public void ExportTableData(DataTable dtdata)
        {
            string attach = "attachment;filename=parthistoryexport.xls";
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
 
