using MedicalInstitutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedicalInstitutions.Controllers
{
	[OutputCache(CacheProfile = "defaultCacheProfile")]
	public class HomeController : Controller
    {

        MedicalInstitutionsContext db = new MedicalInstitutionsContext();

        public ActionResult Index()
        {
	        return View();
        }

        public ActionResult About()
        {
	        ViewBag.Message = "Medical institutions description page.";

	        return View();
        }


	}
}