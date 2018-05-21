using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Monsees
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        public string LotNumber;
        protected string MonseesConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            if (!IsPostBack)
            {
                LotNumber = Request["id"];
                Session["ID"] = LotNumber;
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int newquantity=0;
            int quantitysum = 0;
            TextBox QtyBox;
            string DeliveryItemID;
            string InventoryID;
            string JobID="0";
            string RevisionID="0";
            

            string sqlstring = "Select [JobID], [Active Version] from [Job Item] where [JobItemID] = " + LotNumber;

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
                JobID = reader["JobID"].ToString();
                RevisionID = reader["Active Version"].ToString();
            }
            con.Close();

            foreach (GridViewRow row in GridView1.Rows)
            {
                QtyBox = (TextBox)GridView1.Rows[row.RowIndex].FindControl("InvQtySelected");
                newquantity = Convert.ToInt32(QtyBox.Text);
                if (((CheckBox)GridView1.Rows[row.RowIndex].FindControl("InventoryRework")).Checked) quantitysum = quantitysum + newquantity;
            }

            foreach (GridViewRow row2 in GridView2.Rows)
            {
                QtyBox = (TextBox)GridView2.Rows[row2.RowIndex].FindControl("DeliveryQtySelected");
                newquantity = Convert.ToInt32(QtyBox.Text);
                if (((CheckBox)GridView2.Rows[row2.RowIndex].FindControl("DeliveryRework")).Checked) quantitysum = quantitysum + newquantity;
            }

            if (quantitysum > 0)
            {
                System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("ReplaceJobItem", con);
                comm2.CommandType = System.Data.CommandType.StoredProcedure;
                comm2.Parameters.AddWithValue("@Revision", Convert.ToInt32(RevisionID));
                comm2.Parameters.AddWithValue("@qty", newquantity);
                comm2.Parameters.AddWithValue("@JobID", JobID);
                con.Open();
                comm2.ExecuteNonQuery();
                con.Close();

                foreach (GridViewRow row3 in GridView1.Rows)
                {
                    InventoryID = GridView2.DataKeys[row3.RowIndex].Value.ToString();
                    QtyBox = (TextBox)GridView1.Rows[row3.RowIndex].FindControl("InvQtySelected");
                    newquantity = Convert.ToInt32(QtyBox.Text);
                    System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                    sqlstring = "DELETE FROM Inventory WHERE InventoryID = @InventoryID";
                    comm3.Parameters.AddWithValue("@InventoryID", InventoryID);
                    con.Open();
                    comm3.ExecuteNonQuery();
                    con.Close();

                }

                foreach (GridViewRow row4 in GridView2.Rows)
                {
                    DeliveryItemID = GridView2.DataKeys[row4.RowIndex].Value.ToString();
                    QtyBox = (TextBox)GridView1.Rows[row4.RowIndex].FindControl("DeliveryQtySelected");
                    newquantity = Convert.ToInt32(QtyBox.Text);
                    //change deliveryitem LotNumber to new job item here             
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int newquantity;
            int quantitysum = 0;
            TextBox QtyBox;
            string DeliveryItemID;
            string InventoryID;

            foreach (GridViewRow row in GridView1.Rows)
            {
                QtyBox = (TextBox)GridView1.Rows[row.RowIndex].FindControl("InvQtySelected");
                newquantity = Convert.ToInt32(QtyBox.Text);
                if (((CheckBox)GridView1.Rows[row.RowIndex].FindControl("InventoryRework")).Checked) quantitysum = quantitysum + newquantity;
            }

            foreach (GridViewRow row2 in GridView2.Rows)
            {
                QtyBox = (TextBox)GridView2.Rows[row2.RowIndex].FindControl("DeliveryQtySelected");
                newquantity = Convert.ToInt32(QtyBox.Text);
                if (((CheckBox)GridView2.Rows[row2.RowIndex].FindControl("DeliveryRework")).Checked) quantitysum = quantitysum + newquantity;
            }

            if (quantitysum > 0)
            {

                foreach (GridViewRow row3 in GridView1.Rows)
                {
                    InventoryID = GridView1.DataKeys[row3.RowIndex].Value.ToString();
                    QtyBox = (TextBox)GridView1.Rows[row3.RowIndex].FindControl("InvQtySelected");
                    newquantity = Convert.ToInt32(QtyBox.Text);
                    //delete inventory lines here
                }

                foreach (GridViewRow row4 in GridView2.Rows)
                {
                    DeliveryItemID = GridView2.DataKeys[row4.RowIndex].Value.ToString();
                    QtyBox = (TextBox)GridView2.Rows[row4.RowIndex].FindControl("DeliveryQtySelected");
                    newquantity = Convert.ToInt32(QtyBox.Text);
                    //mark deliveryitem not RTS             
                }
            }
        }
    }
}