<%@ WebHandler Language="C#" Class="Handler" %>

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


public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {

        String FileID = context.Request.QueryString["FileID"];
        String PartNumber = context.Request.QueryString["PartNumber"];
        String RevNumber = context.Request.QueryString["RevNumber"];
        SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString2"].ConnectionString);
        objSqlCon.Open();
        SqlTransaction objSqlTran = objSqlCon.BeginTransaction();
        SqlCommand objSqlCmd = new SqlCommand("FileGet", objSqlCon, objSqlTran);
        objSqlCmd.CommandType = CommandType.StoredProcedure;
        SqlParameter objSqlParam1 = new SqlParameter("@RevisionID", SqlDbType.VarChar);
        objSqlParam1.Value = FileID;
        objSqlCmd.Parameters.Add(objSqlParam1);

        string path = string.Empty;
        string fileType = string.Empty;

        using (SqlDataReader sdr = objSqlCmd.ExecuteReader())
        {
            while (sdr.Read())
            {
                path = sdr[0].ToString();
                fileType = sdr[1].ToString();
                PartNumber = sdr[2].ToString();
                RevNumber = sdr[3].ToString();
            }
        }

        objSqlCmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", objSqlCon, objSqlTran);

        byte[] objContext = (byte[])objSqlCmd.ExecuteScalar();

        SqlFileStream objSqlFileStream = new SqlFileStream(path, objContext ,FileAccess.Read);
        long FileSize = objSqlFileStream.Length;
        byte[] buffer = new byte[(int)FileSize + 1];

        objSqlFileStream.Read(buffer, 0, buffer.Length);
        objSqlFileStream.Close();
        objSqlTran.Commit();


        HttpContext.Current.Response.ContentType = "application/octet-stream";
        HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=\"" + PartNumber + " - Rev" + RevNumber + ".PDF\"");
        // Here you need to manage the download file stuff according to your need


        HttpContext.Current.Response.BinaryWrite(buffer);
        HttpContext.Current.Response.Flush();

    }

    public bool IsReusable {
        get {
            return false;
        }
    }
}