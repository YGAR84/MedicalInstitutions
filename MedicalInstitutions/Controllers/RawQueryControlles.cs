using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalInstitutions.Extensions;
using MedicalInstitutions.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RawQueryController : Controller
	{

		private RawQueryHelper _rawQueryHelper = new RawQueryHelper(new MedicalInstitutionsContext());

		private int PageSize = 4;

		public ActionResult Index([Bind(Include = "")] RawQuery rawQuery, string query, int? pageNum)
		{
			int numOfPage = (pageNum ?? 1);

			if (rawQuery == null)
			{
				rawQuery = new RawQuery();
				rawQuery.SelectResult = new List<IDictionary<string, object>>().ToPagedList(numOfPage, PageSize);
			}

			rawQuery.Query = query;

			if (rawQuery.SelectResult == null)
			{
				rawQuery.SelectResult = new List<IDictionary<string, object>>().ToPagedList(numOfPage, PageSize);
			}

			if (!string.IsNullOrEmpty(rawQuery.Query))
			{
				try
				{
					var result = _rawQueryHelper.DynamicListFromSql(rawQuery.Query);

					rawQuery.SelectResult = result.ToPagedList(numOfPage, PageSize);
					rawQuery.RecordsAffected = _rawQueryHelper.RecordsAffected;
					rawQuery.ColumnNames = _rawQueryHelper.ColumnNames;
				}
				catch (Exception e)
				{
					rawQuery.ErrorMessage = e.Message;
					return View(rawQuery);
				}


			}

			return View(rawQuery);
		}

	}
}