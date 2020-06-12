using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalInstitutions.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult NotFound()
        {
	        return View();
		}

        public ViewResult AccessDenied()
        {
	        return View();
        }

        public ViewResult HttpError()
        {
	        // представление всех остальных кодов HTTP
	        return View("HttpError");
        }
	}
}