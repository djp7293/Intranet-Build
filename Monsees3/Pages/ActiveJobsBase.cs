using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Monsees.Pages
{
    public class ActiveJobsBase : Page
    {
        protected string MonseesConnectionString;
        protected string EmployeeLogin;
        protected string[] EmployeeLoginName;
        protected string EmployeeName;
        protected Int32 EmployeeID;
        protected Int32 index;
        protected Int32 indexl;


        //public SqlDataSource MonseesSqlDataSourceDeliveries { get; set; }
        //public SqlDataSource MonseesSqlDataSource { get; set; }
        //public SqlDataSource MonseesSqlDataSourceLoggedin { get; set; }
        //public DropDownList UsersDropDownList { get; set; }
        //public Button UserNameLabel { get; set; }
        //public Button ViewChangeButton { get; set; }
        //public GridView LoggedInViewGrid { get; set; }
        //public GridView ProductionViewGrid { get; set; }

        //public MultiView JobMultiView { get; set; }
        //public Label Last_Refreshed { get; set; }
        //public View JobOverview { get; set; }
        //public View LoggedInView { get; set; }
        //public View JobsLoggedInView { get; set; }



        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not

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

            BindPage();

        }


        protected virtual void BindPage()
        {
            UserNameLabel.Text = EmployeeName;

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceLoggedin.ConnectionString = MonseesConnectionString;
            EmployeeLogin = User.Identity.Name;
            MonseesSqlDataSourceLoggedin.SelectCommand = "Select * from ProductionViewWP1,Process where (ProductionViewWP1.JobItemID = Process.JobItemID) and Active=1 and (Process.EmployeeID =" + EmployeeID + ")";
            ViewChangeButton.Text = "View Jobs Logged into";

            if (Page.IsPostBack == false)
            {

                UsersDropDownList.Visible = false;
            }


            Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;
        }


        protected void DeptSchedule_Click(object sender, EventArgs e)
        {

            Response.Write("<script type='text/javascript'>window.open('DeptSchedule.aspx');</script>");

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

            ProductionViewGrid.DataBind();

        }

        protected void ProductionViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Session["Authenticate"] = false;
            Session["Employee"] = "";
            Session["EmployeeID"] = "";

            Response.Redirect("Default.aspx");
        }

        protected void ProductionViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            GridView gv;
            string LotID;
            string ProcessID;

            string command_name = e.CommandName;

            if ((command_name == "ThisOp") || (command_name == "Other") || (command_name == "Logout") || (command_name == "GetFile") || (command_name == "Deliveries") || (command_name == "PartHistory"))
            {
                Int32 totrows = ProductionViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "Other":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in
                        if (LoginCheck(LotID) == false)
                        {
                            Response.Write("<script type='text/javascript'>window.open('Login.aspx?JobItemID=" + LotID + "&EmpID=" + EmployeeID + "','_blank');</script>");
                        }
                        else
                        {
                            MessageBox("You are already Logged into this Job.");
                        }
                        break;
                    case "ThisOp":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in
                        if (LoginCheck(LotID) == false)
                        {

                            bool success = CreateProcessRecord();
                            if (success == true)
                            {

                            }
                            else
                            {
                                MessageBox("This login failed.");
                            }
                        }
                        else
                        {
                            MessageBox("You are already Logged into this Job.");
                        }
                        break;
                    case "PartHistory":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        string DetailID = "1";

                        string sqlstring = "Select [DetailID] from [Job Item] where [JobItemID] = " + LotID;

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
                            DetailID = reader["DetailID"].ToString();
                        }
                        con.Close();

                        Response.Write("<script type='text/javascript'>window.open('PartHistory.aspx?DetailID=" + DetailID + "','_blank');</script>");

                        break;

                    case "Deliveries":
                        gvRow = ProductionViewGrid.Rows[index];
                        gv = (GridView)gvRow.FindControl("DeliveryViewGrid");

                        LotID = gvRow.Cells[2].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                        MonseesSqlDataSourceDeliveries.ConnectionString = MonseesConnectionString;
                        MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + LotID;
                        gv.DataSource = MonseesSqlDataSourceDeliveries;
                        gv.DataBind();

                        break;

                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = clickedRow.Cells[4].Text;
                        RevNumber = clickedRow.Cells[5].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
                        break;
                    default:

                        break;

                }
            }
        }



        protected bool CreateProcessRecord()
        {
            MonseesDB objMonseesDB;
            MonseesDBStaticTables objEmployeeDictionary = MonseesDBStaticTables.Instance;
            bool return_result = false;

            GridViewRow gvRow;
            string LotID;
            string SetupID;
            gvRow = ProductionViewGrid.Rows[index];
            LotID = gvRow.Cells[2].Text;
            SetupID = gvRow.Cells[13].Text;

            objMonseesDB = new MonseesDB();
            try
            {


                UsersDropDownList.Visible = false;

                Response.Write("<script type='text/javascript'>window.open('Logout.aspx?EmpID=" + EmployeeID + "&JobItemID=" + LotID + "&JobSetupID=" + SetupID + "','_blank');</script>");

            }
            catch (System.Exception ex)
            {
                return_result = false;
            }
            finally
            {
                return_result = true;
            }

            return return_result;
        }

        protected bool LoginCheck(string LotID)
        {
            //Test to see if user is already logged into Job
            //Prevent if they are


            string sqlstring = "Select * from Process where Active=1 and JobItemID = '" + LotID.Trim() + "' and  (Process.EmployeeID =" + EmployeeID + ")";

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
            bool result = reader.HasRows;

            return result;
        }


        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }

        protected void UpdateViews()
        {

            int CurrentView = JobMultiView.ActiveViewIndex;

            if (CurrentView == 0)
            {
                LoggedInViewGrid.DataBind();
                ViewChangeButton.Text = "View all Production Jobs";
                JobMultiView.SetActiveView(JobsLoggedInView);



            }
            else
            {
                if (CurrentView == 1)
                {
                    ProductionViewGrid.DataBind();
                    ViewChangeButton.Text = "View Jobs Logged into";
                    JobMultiView.SetActiveView(JobOverview);
                };
            }
        }

        protected void ViewChangeButton_Click(object sender, EventArgs e)
        {
            UpdateViews();
        }

        protected void LoggedInViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadUsers()
        {
            string sqlstring = "Select [EmployeeID],name from [Employees] where Active = 1 ORDER BY name ASC";

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
                UsersDropDownList.Items.Add(reader["name"].ToString());
            }
            con.Close();
        }

        protected void UsersDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonseesDBStaticTables objEmployeeDictionary = MonseesDBStaticTables.Instance;
            EmployeeID = Int32.Parse(objEmployeeDictionary.EmployeeDictionary[UsersDropDownList.Text.Trim()]);
            MonseesSqlDataSourceLoggedin.SelectCommand = "Select * from ProductionView,Process where (ProductionView.JobItemID = Process.JobItemID) and Active=1 and (Process.EmployeeID =" + EmployeeID + ")";
            LoggedInViewGrid.EmptyDataText = UsersDropDownList.Text + " is not currently logged into any jobs.";
            LoggedInViewGrid.DataBind();
        }

        protected void UserNameLabel_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.open('EmployeeHistory.aspx?Employee=" + EmployeeID + "','_blank');</script>");

        }
    }
}