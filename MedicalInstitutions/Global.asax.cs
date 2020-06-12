using MedicalInstitutions.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation.Mvc;
using MedicalInstitutions.Controllers;

namespace MedicalInstitutions
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
	        AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FluentValidationModelValidatorProvider.Configure();
		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			var httpContext = ((MvcApplication)sender).Context;
			var currentController = string.Empty;
			var currentAction = string.Empty;
			var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

			if (currentRouteData != null)
			{
				if (!string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
				{
					currentController = currentRouteData.Values["controller"].ToString();
				}

				if (!string.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
				{
					currentAction = currentRouteData.Values["action"].ToString();
				}
			}

			// пойманное исключение
			var ex = Server.GetLastError();


			// ну а дальше подготовка к вызову подходящего метода контроллера ошибок
			var controller = new ErrorController();
			var routeData = new RouteData();
			var action = "Index";

			if (ex is HttpException)
			{
				switch (((HttpException)ex).GetHttpCode())
				{
					case 403:
						action = "AccessDenied";
						//action = "Index";
						break;
					case 404:
						action = "NotFound";
						//action = "Index";
						break;
					default:
						action = "HttpError";
						//action = "Index";
						break;
						// можно добавить свои методы контроллера для любых кодов ошибок
				}
			}

			httpContext.ClearError();
			httpContext.Response.Clear();
			httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
			httpContext.Response.TrySkipIisCustomErrors = true;

			routeData.Values["controller"] = "Error";
			routeData.Values["action"] = action;

			controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
			((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
		}
	}
}
