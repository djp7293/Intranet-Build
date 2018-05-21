using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using System.Linq;
using System.Web;

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
    public partial class WebForm2 : DataPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;
            int index;
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string command_name = e.CommandName;

            if ((command_name == "GetFile"))
            {
                Int32 totrows = GridView1.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                objSqlCon.Open();
                MatCertList record = new MatCertList();
                SqlTransaction objSqlTran = objSqlCon.BeginTransaction();


                SqlParameter objSqlParam1;
                GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                List<MatCertList> filepaths = new List<MatCertList>();


                string sqlcommand;
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "GetFile":

                        String Material;
                        string MatPriceID;
                        MatPriceID = clickedRow.Cells[0].Text;
                        Material = clickedRow.Cells[5].Text;
                        SqlCommand objSqlCmd2 = new SqlCommand("GetMatlCert", objSqlCon, objSqlTran);
                        objSqlCmd2.CommandType = CommandType.StoredProcedure;
                        objSqlParam1 = new SqlParameter("@MatPriceID", SqlDbType.Int);
                        objSqlParam1.Value = Convert.ToInt32(MatPriceID);
                        objSqlCmd2.Parameters.Add(objSqlParam1);


                        using (SqlDataReader sdr = objSqlCmd2.ExecuteReader())
                        {

                            while (sdr.Read())
                            {
                                record.ID = sdr[0].ToString();
                                record.MatPriceID = Convert.ToInt32(sdr[1].ToString());
                                record.SerialNumber = Convert.ToInt32(sdr[2].ToString());
                                record.filetype = sdr[3].ToString();
                                filepaths.Add(record);
                            }
                        }

                        objSqlCon.Close();

                        foreach (MatCertList filePath in filepaths)
                        {

                            //FileStream objSqlFileStream = new FileStream(filePath.part + " - Rev" + filePath.revision, FileMode.OpenOrCreate, FileAccess.Write);
                            //BinaryWriter bw = new BinaryWriter(objSqlFileStream);
                            //long FileSize = objSqlFileStream.Length;


                            sqlcommand = "SELECT MaterialCert FROM MaterialCerts WHERE SerialNumber = " + filePath.SerialNumber;
                            SqlConnection con1 = new SqlConnection(MonseesConnectionString);
                            SqlCommand comm1 = new SqlCommand(sqlcommand, con1);
                            SqlDataAdapter dp = new SqlDataAdapter(comm1);
                            DataSet ds = new DataSet("MyImages");

                            byte[] fileBytes = new byte[0];

                            dp.Fill(ds, "MyImages");
                            DataRow myRow;
                            myRow = ds.Tables["MyImages"].Rows[0];

                            fileBytes = (byte[])myRow["MaterialCert"];
                            //FileStream objSqlFileStream2 = new FileStream(filePath.part + " - Rev" + filePath.revision, FileMode.Open, FileAccess.Read);

                            //objSqlFileStream2.Read(fileBytes, 0, fileBytes.Length);
                            //objSqlFileStream2.Close();
                            //objSqlTran.Commit();
                            //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath.path);

                            string type;
                            HttpContext.Current.Response.ContentType = "application/octet-stream";
                            if (record.filetype == "image")
                            {
                                type = "jpg";
                            }
                            else
                            {
                                type = record.filetype;
                            }
                            HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=\"" + MatPriceID + " - " + Material + "." + type + "\"");
                            // Here you need to manage the download file stuff according to your need


                            HttpContext.Current.Response.BinaryWrite(fileBytes);
                            HttpContext.Current.Response.Flush();
                        }

                        break;

                    default:

                        break;
                }
            }
        }
    }
}