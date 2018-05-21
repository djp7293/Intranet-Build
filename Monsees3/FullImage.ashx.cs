using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace Monsees
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string imageid = context.Request.QueryString["ImID"];
            Int32 count = 0;
            if (imageid == null || imageid == "")
            {
                //Set a default imageID
                imageid = "1";
            }
           
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select SetupImage from SetupImages where SetupID=" + imageid, connection);
            SqlDataReader dr = command.ExecuteReader();

            dr.Read();
            context.Response.ContentType = "image/jpg";
            context.Response.BinaryWrite((Byte[])dr[0]);
            
                
            connection.Close();
            context.Response.End();
           
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}