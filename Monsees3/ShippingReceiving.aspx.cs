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
    public partial class _Default2 : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        private string MatPriceID;
        private string SubcontractItemID;
        private string JobItemID;
              

        protected void Page_Load(object sender, EventArgs e)
        {
            
                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                MonseesSqlDataSourceMaterial.ConnectionString = MonseesConnectionString;
                MonseesSqlDataSourcePrepare.ConnectionString = MonseesConnectionString;
                MonseesSqlDataSourceSubcontract.ConnectionString = MonseesConnectionString;
                //ViewChangeButton.Text = "View Subcontract POs";

                //if (!Page.IsPostBack)
                //{
                //    MaterialGrid.DataBind();
                //    MaterialGrid2.DataBind();
                //    SubcontractGrid.DataBind();
                //}

                Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;
           
        }

        protected void MaterialGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
           
 
        }

        protected void MonseesSqlDataSourcePrepare_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.CommandTimeout = 0;
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
                        MatPriceID = gvRow.Cells[0].Text;                        

                        gvRow = MaterialGrid.Rows[index];
                        string MaterialItemID = MaterialGrid.DataKeys[index].Value.ToString();

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("ReceiveMaterial", con3);
                        comm3.CommandType = System.Data.CommandType.StoredProcedure;
                        comm3.Parameters.AddWithValue("@MatItemID", MaterialItemID);
                        con3.Open();
                        comm3.ExecuteNonQuery();
                        con3.Close();
                        Response.Write("<script type='text/javascript'>window.open('MaterialLabel.aspx?id=" + MatPriceID + "','_blank');</script>");
                        SubcontractGrid.DataBind();
                        




            }
        }

        protected void SubcontractGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (((HiddenField)e.Row.FindControl("NewPart")).Value != "")
                {
                    if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        e.Row.BackColor = System.Drawing.Color.FromName("#ffc880");

                    }
                }

                if (((HiddenField)e.Row.FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }


                if (((HiddenField)e.Row.FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)e.Row.FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("Hot")).Value))
                    {
                        e.Row.Font.Bold = true;

                    }
                }

            }
        }

        protected void MaterialGrid2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#fbffb5");

                }


                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)e.Row.FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }


                if (((HiddenField)e.Row.FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)e.Row.FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("Hot")).Value))
                    {
                        e.Row.Font.Bold = true;

                    }
                }

            }
        }

        protected void SubcontractGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            bool check;



            string command_name = e.CommandName;

            if ((command_name == "Received"))
            {
                int index = Convert.ToInt32(e.CommandArgument);


                gvRow = SubcontractGrid.Rows[index];
                SubcontractItemID = SubcontractGrid.DataKeys[index].Value.ToString();

                System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("ReceiveSubcontract", con3);
                comm3.CommandType = System.Data.CommandType.StoredProcedure;
                comm3.Parameters.AddWithValue("@SubItemID", SubcontractItemID);
                con3.Open();
                comm3.ExecuteNonQuery();
                con3.Close();
                SubcontractGrid.DataBind();

                

            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            byte[] buffer;
            Guid fileId = Guid.NewGuid();
            switch (e.CommandName)
            {
                case "Attach":
                    FileUpload myFileTest = (FileUpload)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("filMyFileTest");
                    if (myFileTest.HasFile)
                    {
                        buffer = new byte[(int)myFileTest.FileContent.Length];
                        myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                        if (myFileTest.FileContent.Length > 0)
                        {
                            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                            {
                                con.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PlateCertAdd", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@subcontractitemID", LinkID);
                                cmd.Parameters.AddWithValue("@fileType", myFileTest.PostedFile.ContentType);
                                cmd.Parameters.AddWithValue("@ID", fileId);
                                cmd.Parameters.AddWithValue("@Certification", buffer);


                                cmd.ExecuteNonQuery();


                                con.Close();
                            }



                        }
                        GridView1.DataBind();
                    }
                    break;
                case "Ignore":
                    string sqlstring = "UPDATE [Subcontract Item] SET IgnoreCertReq = 1 WHERE [SubcontractItemID] = " + LinkID;

                    System.Data.SqlClient.SqlConnection con5 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                    System.Data.SqlClient.SqlCommand comm5 = new System.Data.SqlClient.SqlCommand(sqlstring, con5);


                    con5.Open();

                    // execute sql command and store a return values in reade
                    comm5.ExecuteNonQuery();
                    con5.Close();
                    GridView1.DataBind();
                    break;
                default:
                    break;
            }
        }


        protected void MaterialGrid2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            
            TextBox Location;
            int index = Convert.ToInt32(e.CommandArgument);
            

            string command_name = e.CommandName;

            switch (e.CommandName)
            {
                case "Prepared":
                    


                    gvRow = MaterialGrid2.Rows[index];
                    JobItemID = gvRow.Cells[1].Text;
                    

                
                    SqlConnection connection = new SqlConnection(MonseesConnectionString);
                    connection.Open();
                    if ((connection.State & ConnectionState.Open) > 0)
                    {
                        connection.Close();
                        try
                        {
                            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                            MonseesSqlDataSourcePrepare.ConnectionString = MonseesConnectionString;

                            Location = (TextBox)gvRow.FindControl("Loc");
                            MonseesSqlDataSourcePrepare.UpdateParameters["Location"].DefaultValue = Location.Text;
                            MonseesSqlDataSourcePrepare.UpdateParameters["JobItemID"].DefaultValue = JobItemID;
                            MonseesSqlDataSourcePrepare.Update();

                        }
                        catch
                        {
                            Response.Write("Didn't Work.");
                        }
                    }
                    else
                    {
                        Response.Write("No network connection!");
                    }
                break;
                case "GetFile":
                    String PartNumber;
                        
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;                       
                        PartNumber = clickedRow.Cells[3].Text;
                       
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber);
                    for (int i = 0; i < SubcontractGrid.Rows.Count; i++)
                    {


                        if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                        {
                            SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                        }

                        if (((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value != "")
                        {
                            if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                            }
                        }


                        if (((HiddenField)SubcontractGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                        {
                            SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                        }

                        if (((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value != "")
                        {
                            string hot = ((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value;
                            if (Convert.ToBoolean(((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value))
                            {
                                SubcontractGrid.Rows[i].Font.Bold = true;

                            }


                        }


                    }

                    for (int i = 0; i < MaterialGrid2.Rows.Count; i++)
                    {


                        if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                        {
                            MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                        }

                        if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                        {
                            MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                        }


                        if (((HiddenField)MaterialGrid2.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                        {
                            MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                        }

                        if (((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value != "")
                        {
                            string hot = ((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value;
                            if (Convert.ToBoolean(((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value))
                            {
                                MaterialGrid2.Rows[i].Font.Bold = true;

                            }


                        }


                    }
                    break;
                case "Morelabels":
                    int MatPriceID = Convert.ToInt32(MaterialGrid2.DataKeys[index].Value.ToString());
                    Response.Write("<script type='text/javascript'>window.open('MaterialLabel.aspx?id=" + MatPriceID + "','_blank');</script>");

                    for (int i = 0; i < SubcontractGrid.Rows.Count; i++)
                    {


                        if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                        {
                            SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                        }

                        if (((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value != "")
                        {
                            if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                            }
                        }


                        if (((HiddenField)SubcontractGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                        {
                            SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                        }

                        if (((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value != "")
                        {
                            string hot = ((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value;
                            if (Convert.ToBoolean(((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value))
                            {
                                SubcontractGrid.Rows[i].Font.Bold = true;

                            }


                        }


                    }

                    for (int i = 0; i < MaterialGrid2.Rows.Count; i++)
                    {


                        if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                        {
                            MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                        }

                        if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                        {
                            MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                        }


                        if (((HiddenField)MaterialGrid2.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                        {
                            MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                        }

                        if (((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value != "")
                        {
                            string hot = ((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value;
                            if (Convert.ToBoolean(((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value))
                            {
                                MaterialGrid2.Rows[i].Font.Bold = true;

                            }


                        }


                    }
                    break;

                default:
                break;
            }




            
        }

        protected bool UpdateMaterialRecord()
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
        }

       
        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }
      
        protected void UpdateViews()
        {
                        
            int CurrentView = ShipReceiveMultiView.ActiveViewIndex;

            if (CurrentView == 0)
            {
                SubcontractGrid.DataBind();
                //ViewChangeButton.Text = "View Material Orders";
                ShipReceiveMultiView.SetActiveView(Subcontract);
                
            }
            else
             {
             if (CurrentView == 1)
               {
                   
                 MaterialGrid.DataBind();   
                 //ViewChangeButton.Text = "View Subcontract POs"; 
                 ShipReceiveMultiView.SetActiveView(Materials);
               };
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID = GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            byte[] buffer;
            Guid fileId = Guid.NewGuid();
            switch (e.CommandName)
            {
                case "Attach":
                    FileUpload myFileTest = (FileUpload)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("filMyFileTest");
                    if (myFileTest.HasFile)
                    {
                        buffer = new byte[(int)myFileTest.FileContent.Length];
                        myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                        if (myFileTest.FileContent.Length > 0)
                        {
                            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString))
                            {
                                con.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MaterialCertAdd", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@MatPriceID", LinkID);
                                cmd.Parameters.AddWithValue("@fileType", myFileTest.PostedFile.ContentType);
                                cmd.Parameters.AddWithValue("@ID", fileId);
                                cmd.Parameters.AddWithValue("@Certification", buffer);


                                cmd.ExecuteNonQuery();


                                con.Close();
                            }



                        }
                        GridView2.DataBind();
                    }
                    break;
                case "Ignore":
                    string sqlstring = "UPDATE [Material_Price2] SET IgnoreCertReq = 1 WHERE [MatPriceID] = " + LinkID;

                    System.Data.SqlClient.SqlConnection con5 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                    System.Data.SqlClient.SqlCommand comm5 = new System.Data.SqlClient.SqlCommand(sqlstring, con5);


                    con5.Open();

                    // execute sql command and store a return values in reade
                    comm5.ExecuteNonQuery();
                    con5.Close();
                    GridView2.DataBind();
                    break;
                default:
                    break;
            }
        }

        protected void ViewChangeButton_Click(object sender, EventArgs e)
        {
            UpdateViews();
        }

        protected void LoggedInViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //GridView cells are 0 based
            GridViewRow gvRow;
            bool check;
            

            string command_name = e.CommandName;

            if ((command_name == "Received"))
            {
                int index = Convert.ToInt32(e.CommandArgument);


                gvRow = GridView3.Rows[index];
                string ToolingItemID = GridView3.DataKeys[index].Value.ToString();

                System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("ReceiveTooling", con3);
                comm3.CommandType = System.Data.CommandType.StoredProcedure;
                comm3.Parameters.AddWithValue("@ToolingItemID", Convert.ToInt32(ToolingItemID));
                con3.Open();
                comm3.ExecuteNonQuery();
                con3.Close();
                GridView3.DataBind();


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SubcontractGrid.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value != "")
                {
                    if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                    }
                }


                if (((HiddenField)SubcontractGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value))
                    {
                        SubcontractGrid.Rows[i].Font.Bold = true;

                    }


                }


            }

            for (int i = 0; i < MaterialGrid2.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (((HiddenField)MaterialGrid2.Rows[i].FindControl("NewPart")).Value != "")
                {
                    if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                    }
                }


                if (((HiddenField)MaterialGrid2.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value))
                    {
                        MaterialGrid2.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            //GridView cells are 0 based
            GridViewRow gvRow;

            int index = Convert.ToInt32(e.CommandArgument);


            gvRow = GridView4.Rows[index];
            MatPriceID = gvRow.Cells[0].Text;

            
            string StockInventoryID = GridView4.DataKeys[index].Value.ToString();

            string command_name = e.CommandName;

            if ((command_name == "Labels") || (command_name == "Clear"))
            {
                switch (e.CommandName)
                {
                    case "Labels":
                        Response.Write("<script type='text/javascript'>window.open('MaterialLabel.aspx?id=" + MatPriceID + "','_blank');</script>");

                        break;

                    case "Clear":
                        SqlConnection connection = new SqlConnection(MonseesConnectionString);
                        connection.Open();
                        if ((connection.State & ConnectionState.Open) > 0)
                        {
                            connection.Close();
                            try
                            {

                                SqlDataSource2.UpdateParameters["StockInventoryID"].DefaultValue = StockInventoryID;
                                SqlDataSource2.Update();

                            }
                            catch
                            {
                                Response.Write("Didn't Work.");
                            }
                        }
                        else
                        {
                            Response.Write("No network connection!");
                        }

                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < SubcontractGrid.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value != "")
                {
                    if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                    }
                }


                if (((HiddenField)SubcontractGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value))
                    {
                        SubcontractGrid.Rows[i].Font.Bold = true;

                    }


                }


            }

            for (int i = 0; i < MaterialGrid2.Rows.Count; i++)
            {

                

                if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (((HiddenField)MaterialGrid2.Rows[i].FindControl("NewPart")).Value != "")
                {
                    if (Convert.ToInt32(((HiddenField)MaterialGrid2.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                    }
                }


                if (((HiddenField)MaterialGrid2.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    MaterialGrid2.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)MaterialGrid2.Rows[i].FindControl("Hot")).Value))
                    {
                        MaterialGrid2.Rows[i].Font.Bold = true;

                    }


                }


            }
        }
    }
}
