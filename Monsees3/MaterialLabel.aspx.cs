using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Montsees.Data.DataModel;
using Montsees.Data;
using Montsees.Data.Repository;
using Dapper;
using Monsees.Pages;


namespace Monsees
{
    public partial class WebForm3 : DataPage
    {
        protected MaterialTagModel MaterialData;
        protected int MatPriceID;

        protected void Page_Load(object sender, EventArgs e)
        {
            MatPriceID = Int32.Parse(Request["id"]);
            GetData();
           
        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            MaterialData = inspectionRepository.GetMaterialTagByMatPriceID(MatPriceID);

            this.UnitOfWork.End();
        }
    }
}