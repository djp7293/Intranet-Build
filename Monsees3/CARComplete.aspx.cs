using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicFrame.WebControls;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using Monsees.Data;
using Monsees.Pages;

namespace Monsees
{
    public partial class CARComplete : DataPage
    {
        protected List<EmployeeModel> EmployeeList;
        protected JobDetailModel JobItemDetails;
        protected CorrectiveActionModel CADetails;
        protected int JobItemID;
        protected int CARID;

        protected void Page_Load(object sender, EventArgs e)
        {
            CARID = Int32.Parse(Request["id"]);

            GetData();

            if (!IsPostBack)
            {
                if (CADetails.Completed)
                {
                    EditableMulti.SetActiveView(ViewView);
                    Label1.DataBind();
                    Label2.DataBind();
                    Label3.DataBind();
                    Label4.DataBind();
                    Label5.DataBind();
                    Label6.DataBind();
                    Label7.DataBind();
                    Label8.DataBind();
                }
                else
                {
                    InitDateLbl.DataBind();
                    ImplDateLbl.DataBind();
                    InitEmplLbl.DataBind();
                    ImplEmplLbl.DataBind();
                    ProblemCtrl.DataBind();
                    RootCtrl.DataBind();
                    CorrectionCtrl.DataBind();
                    PreventiveCtrl.DataBind();
                }
                
            }

        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            CADetails = inspectionRepository.GetCorrectiveActionbyCARID(CARID);
            JobItemDetails = inspectionRepository.GetJobDetailModelByJobItemId(CADetails.JobItemID);
            

            this.UnitOfWork.End();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection con1;
            System.Data.SqlClient.SqlCommand cmd1;

            
            string Root = RootCtrl.Text;
            string Immediate = CorrectionCtrl.Text;
            string Preventive = PreventiveCtrl.Text;

            con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            cmd1 = new System.Data.SqlClient.SqlCommand("CompleteCorrectiveAction", con1);
            cmd1.Parameters.Clear();
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@CARID", CARID);           
            cmd1.Parameters.AddWithValue("@Root", Root);
            cmd1.Parameters.AddWithValue("@Immediate", Immediate);
            cmd1.Parameters.AddWithValue("@Preventive", Preventive);
            cmd1.Parameters.AddWithValue("@Completed", 0);

            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();
            con1.Dispose();
        }

        protected void Complete_Click(object sender, EventArgs e)
        {

            System.Data.SqlClient.SqlConnection con1;
            System.Data.SqlClient.SqlCommand cmd1;


            string Root = RootCtrl.Text;
            string Immediate = CorrectionCtrl.Text;
            string Preventive = PreventiveCtrl.Text;

            con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            cmd1 = new System.Data.SqlClient.SqlCommand("CompleteCorrectiveAction", con1);
            cmd1.Parameters.Clear();
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@CARID", CARID);
            cmd1.Parameters.AddWithValue("@Root", Root);
            cmd1.Parameters.AddWithValue("@Immediate", Immediate);
            cmd1.Parameters.AddWithValue("@Preventive", Preventive);
            cmd1.Parameters.AddWithValue("@Completed", 1);

            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();
            con1.Dispose();

            EditableMulti.SetActiveView(ViewView);

        }
    }
}