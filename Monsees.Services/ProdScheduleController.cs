using Montsees.Data.Repository;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;
using System.IO;



namespace Monsees.Services
{
   

    

    public class ProdScheduleController : ControllerBase
    {
        static UserControlRestService handler;


        static ProdScheduleController()
		{
			handler = new UserControlRestService(typeof(InspectionController));
		}

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (Request["a"] != null)
			{
				handler.HandleRequest(this);
			}
		}
 

		protected InspectionRepository _inspectionRepository;

		protected InspectionRepository inspectionRepository
		{
			get
			{
				if (_inspectionRepository == null) 
					_inspectionRepository = new InspectionRepository(UnitOfWork);

				return _inspectionRepository;
			}
		}
        

		[ServiceMethod]       
        public static string LoadControl2(string id)
        {
            Page page = new Page();
            Control control = page.LoadControl("~/NestedLoginCtrl.ascx");
            NestedLoginCtrl UserCtrl = (NestedLoginCtrl)control;
            UserCtrl.SetJobItem(id);
            HtmlForm form = new HtmlForm();
            form.Controls.Add(UserCtrl);
            page.Controls.Add(form);
            StringWriter writer = new StringWriter();
            HttpContext.Current.Server.Execute(page, writer, false);
            return writer.ToString();
        }

    }
}
