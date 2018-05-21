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
    public class Thumbnail : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            string imageid = context.Request.QueryString["ImID"];
            
            if (imageid == null || imageid == "")
            {
                //Set a default imageID
                imageid = "1";
            }
            else
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("select SetupImage from SetupImages where SetupImageID='" + imageid + "'", connection);
                SqlDataReader dr = command.ExecuteReader();
                dr.Read();
           
                Stream str = new MemoryStream((Byte[])dr[0]);

                Bitmap loBMP = new Bitmap(str);
                Bitmap bmpOut = new Bitmap(80, 80);

                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.White, 0, 0, 80, 80);
                g.DrawImage(loBMP, 0, 0, 80, 80);

                MemoryStream ms = new MemoryStream();
                bmpOut.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] bmpBytes = ms.GetBuffer();
                bmpOut.Dispose();
                ms.Close();
                context.Response.ContentType = "image/jpg";
                context.Response.BinaryWrite(bmpBytes);
            
                connection.Close();
                context.Response.End();  
            }
            
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