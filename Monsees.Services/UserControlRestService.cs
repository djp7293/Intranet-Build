namespace Monsees.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Reflection;
	using System.Web.UI;
	using System.Web;
	using Newtonsoft.Json;
	using Monsees.Database;
    

	public class ServiceMethod : Attribute
	{

	}

	public class UserControlRestService
	{
		#region Properties

		public delegate string ErrorHandlerDelegate(Exception ex);

		private Dictionary<string, MethodInfo> Actions;
		private Type serviceType { get; set; }
		public ErrorHandlerDelegate ErrorHandler { get; set; }

		#endregion


		#region Init

		public UserControlRestService(Type type)
		{
			InitializeActions(type);
		}

		private void InitializeActions(Type serviceType)
		{
			var methods = serviceType.GetMethods()
				.Where(m => m.GetCustomAttributes(typeof(ServiceMethod), false).Length > 0);
			Actions = methods.ToDictionary(v => v.Name, d => d);
		}

		#endregion

 

		public void HandleRequest(UserControl control)
		{
			HandleRequest(control, control.Page, control.Request);
		}

		public void HandleRequest(Page page)
		{
			HandleRequest(page, page, page.Request);
		}

		

		private void HandleRequest(object target, Page page, HttpRequest request)
		{

			try
			{

				page.Response.ClearHeaders();
				page.Response.Clear();
				page.Response.ContentType = "application/json";

				MethodInfo m = null;

				try
				{
					string action = request["a"];
					m = Actions[action];
				}
				catch
				{
					throw new Exception("Bad Request");
				}


				List<object> attributeList = new List<object>();

				m.GetParameters().ToList().ForEach(p =>
				{
					object converted = null;

					if (p.ParameterType == typeof(string))
					{
						string stringParam = String.Concat(request[p.Name]);
						if (stringParam.StartsWith("\"") && stringParam.EndsWith("\"") && stringParam.Length >= 2)
						{
							stringParam = stringParam.Substring(1, stringParam.Length - 2);
						}
						converted = stringParam;
					}
					else
					{
						converted = request[p.Name] != null ? JsonConvert.DeserializeObject(request[p.Name], p.ParameterType) : null;
					}

					attributeList.Add(converted);
				});


				object result = m.Invoke(target, attributeList.ToArray());

				if (result != null)
				{
					page.Response.Write(JsonConvert.SerializeObject(result));
				}
				else
				{
					page.Response.Write("{}");
				}

			}
			catch (Exception ex)
			{
				page.Response.Write(JsonConvert.SerializeObject(new ErrorMessage() { ErrorMessageText = GetMessage(ex) }));
			}

			page.Response.Flush();
			page.Response.SuppressContent = true;
			HttpContext.Current.ApplicationInstance.CompleteRequest();
		}

		private string GetMessage(Exception ex)
		{
			if (this.ErrorHandler != null)
			{
				return this.ErrorHandler(ex);
			}
			else if (ex.InnerException != null)
			{
				return ex.InnerException.Message;
			}

			else return ex.Message;
		}

	}
 
	public class ErrorMessage
	{
		public string ErrorMessageText {get;set;}
	}
}
