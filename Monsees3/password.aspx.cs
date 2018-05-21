using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.Text;

// Default
// D.S.Harmor
// Description - Main Login page
// 08/19/2011 - Initial Version
// 
// 

namespace Monsees
{
    public partial class Password : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorMsg.Text = "";
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();                      
        }

 
        public string Authenticate(string email, string login)
        {
            string sqlstring = "Select password from [Contact] where Email='" + email.Trim() + "' and login='" + login.Trim() + "'";
         

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
            string result = null;  
             
            // check if reader hase any value then return true otherwise return false
            if (reader.Read())
            {
               result = reader["password"].ToString();
                
            }
            else
            {
               
                result = null;
            }
            con.Close();
            return result;
        
        }

        

        protected void PasswordButton_Click(object sender, EventArgs e)
        {
            string Email = EmailTextBox.Text;
            string Username = UsernameTextBox.Text;
            string result1 = null;
            MailMessage message = new MailMessage();
            StringBuilder emailbody = new StringBuilder("");

            try
            {
            
                UsernameTextBox.Text = "";
                result1 = Authenticate(Email, Username);
                if (result1 != null)
                {
                    emailbody.Append("<html><body><table width='80%'><tr><td>");
                    emailbody.Append("This e-mail was automatically generated.  Please do not respond to this e-mail or send mail to this e-mail address, as the message will not be received by Monsees Tool.</br></br>");
                    emailbody.Append("The following is the requested password for your customer portal account.  Please contact us if you continue to have problems logging into your customer portal: " + result1 + "<br><br>");
                    emailbody.Append("Regards, <br><br>System Administrator");
                    emailbody.Append("</tr></td></table></body></html>");
                    message.From = new MailAddress("administrator@monseestool.com");
                    message.To.Add(new MailAddress(Email.Trim()));
                    message.Body = emailbody.ToString();
                    message.IsBodyHtml = true;
                    message.Subject = "Acct. Password Request";
                    if (filMyFile.PostedFile != null)
                    {
                        HttpPostedFile myFile = filMyFile.PostedFile;
                        int nFileLen = myFile.ContentLength;
                        byte[] myData = new byte[nFileLen];
                        string FileName = Path.GetFileName(filMyFile.PostedFile.FileName);
                        myFile.InputStream.Read(myData, 0, nFileLen);
                        filMyFile.PostedFile.SaveAs(Server.MapPath(FileName));
                        message.Attachments.Add(new Attachment(Server.MapPath(FileName)));
                    }

                    SmtpClient emailWithAttach = new SmtpClient();
                    emailWithAttach.Host = "localhost";
                    emailWithAttach.DeliveryMethod = SmtpDeliveryMethod.Network;
                    emailWithAttach.UseDefaultCredentials = true;
                    emailWithAttach.Send(message);

     
                }
                else
                {
                 
                    ErrorMsg.Text = "We have no record of a user with that E-mail address and username.";
                    
                }
            }
            catch(Exception ex)
           {
                throw ex;
}
        }

        }

      
    }

