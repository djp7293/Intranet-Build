using System;
using System.Net;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime;
using System.Runtime.InteropServices;

using System.Security.Principal;

// MonseesDB
// D.S.Harmor
// Description - A database helper class to simplify common code needed when accessing the database server
// 08/19/2011 - Initial Version
// 
// 

namespace Monsees
{


    
    public class myLabel : ITemplate
    {
        public DataControlRowType DataRowType;
        private string TextValue;
        private string ControlName;
        //Constructor...
        public myLabel(DataControlRowType type, string _ControlID, string _TextValue)
        {
            DataRowType = type;
            TextValue = _TextValue;
            ControlName = _ControlID;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            switch (DataRowType)
            {
                case DataControlRowType.DataRow:
                    Label bfield = new Label();
                    bfield.ID = ControlName;
                    bfield.Text = TextValue;
                    bfield.DataBinding += new EventHandler(lbl_DataBinding);
                    container.Controls.Add(bfield);
                    break;
                case DataControlRowType.Footer:
                    Label footer = new Label();
                    footer.ID = ControlName;
                    footer.Text = TextValue;
                    container.Controls.Add(footer);
                    break;
            }
        }

        void lbl_DataBinding(object sender, EventArgs e)
        {

            Label lnk = (Label)sender;
            GridViewRow container = (GridViewRow)lnk.NamingContainer;
            object dataValue = DataBinder.Eval(container.DataItem, TextValue);
            if (dataValue != DBNull.Value)
            {
                lnk.Text = dataValue.ToString();
            }
            else
            {
                lnk.Text = "";
            }
        }

    }

    public class MonseesDB
    {
        private string MonseesConnectionString;
        private SqlDataSource MonseesSqlDataSource;
        private System.Data.SqlClient.SqlConnection con;

        public MonseesDB()
        {
            MonseesSqlDataSource = new SqlDataSource();
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
            // create a connection with sqldatabase 
            con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            // create a sql command which will user connection string and your select statement string

        }

        public System.Data.SqlClient.SqlDataReader ExecuteReader(string sqlstring)
        {
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            // execute sql command and store a return values in reader
            con.Open();
            return comm.ExecuteReader();
        }


        public int ExecuteNonQuery(string sqlstring)
        {
            int result;

            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
            con.Open();
            // execute sql command result is the number of rows affected. should be 1

            result = comm.ExecuteNonQuery();

            return result;
        }

        public void Close()
        {
            con.Close();
            con.Dispose();
        }

    }
    public class InternetCS
{

//Creating the extern function...
[DllImport("wininet.dll")]
private extern static bool InternetGetConnectedState( out int Description, int ReservedValue ) ;

//Creating a function that uses the API function...
public static bool IsConnectedToInternet( )
{

int Desc ;
return InternetGetConnectedState( out Desc, 0 ) ;

}


}

    
}
