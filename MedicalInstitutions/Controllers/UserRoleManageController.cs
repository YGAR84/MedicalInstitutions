using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using MedicalInstitutions.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace MedicalInstitutions.Controllers
{
	public class UserRoleManageController : Controller
	{
		private ApplicationUserManager _userManager;

		public ApplicationUserManager UserManager
		{
			get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			private set => _userManager = value;
		}

		MedicalInstitutionsContext db = new MedicalInstitutionsContext();

		private int PageSize = 4;
        // GET: UserRoleManage
        public ActionResult Index(int? pageNum)
        {
	        int numOfPage = pageNum ?? 1;
	        var users = UserManager.Users.Include(u => u.Roles).OrderBy(u => u.UserName);
            return View(users.ToPagedList(numOfPage, PageSize));
        }

        // GET: Laboratories/Create
        public async Task<ActionResult> Create()
        {
	        ViewBag.Roles = await db.Roles.ToListAsync();
			return View();
        }

        // POST: Laboratories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Email,UserName,UserRoles,Password")] CreateUserModel createUserModel)
        {
	        ViewBag.Roles = await db.Roles.ToListAsync();
			if (ModelState.IsValid)
	        {

		        ApplicationUser user = new ApplicationUser
		        {
			        UserName = createUserModel.UserName,
			        Email = createUserModel.Email
		        };
		        IdentityResult result;
		        if (createUserModel.Password.IsNullOrWhiteSpace())
		        {
			        result = await UserManager.CreateAsync(user);
				}
		        else
		        {
					result = await UserManager.CreateAsync(user, createUserModel.Password);
				}

		        if (!result.Succeeded)
		        {
			        return View(createUserModel);
		        }

				if (createUserModel.UserRoles != null)
		        {
			        result = UserManager.AddToRoles(user.Id, createUserModel.UserRoles);
			        if (!result.Succeeded)
			        {
				        return View(createUserModel);
			        }
				}

				return RedirectToAction("Index");
	        }



			return View(createUserModel);
        }

		public async Task<ActionResult> Edit(string id)
        {
	        if (id.IsNullOrWhiteSpace())
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }
	        ApplicationUser user = await UserManager.FindByIdAsync(id);
			if (user == null)
	        {
		        return HttpNotFound();
	        }

			ViewBag.Roles = await db.Roles.ToListAsync();
			ViewBag.UserRoles = await UserManager.GetRolesAsync(user.Id);
			return View(user);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,AccessFailedCount,UserName,LockoutEnabled,selectedRoles")] ApplicationUser user, string[] selectedRoles)
		{
			ViewBag.Roles = await db.Roles.ToListAsync();
			ViewBag.UserRoles = await UserManager.GetRolesAsync(user.Id);

			if (ModelState.IsValid)
			{
				db.Entry(user).State = EntityState.Modified;

				var userRoles = await UserManager.GetRolesAsync(user.Id);
				var result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.ToArray());
				if (!result.Succeeded)
				{
					return View(user);
				}
				if (selectedRoles != null)
				{
					result = UserManager.AddToRoles(user.Id, selectedRoles);
					if (!result.Succeeded)
					{
						return View(user);
					}
				}

				await db.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			return View(user);
		}

		public ActionResult ChangeUserPassword()
		{
			return View();
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<ActionResult> ChangeUserPassword([Bind(Include = "NewPassword")] ChangeUserPasswordModel changeUserPasswordModel)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = await UserManager.FindByNameAsync(changeUserPasswordModel.UserName);
		//		if (user == null)
		//		{
		//			return View(changeUserPasswordModel);
		//		}

		//		var result = await UserManager.AddPasswordAsync(user.Id, changeUserPasswordModel.NewPassword);
		//		if (!result.Succeeded)
		//		{
		//			return View(changeUserPasswordModel);
		//		}

		//		return RedirectToAction("Index");
		//	}

		//	return View(changeUserPasswordModel);
		//}

		// GET: Laboratories/Delete/5
		public async Task<ActionResult> Delete(string id)
		{
			if (id.IsNullOrWhiteSpace())
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser user = await UserManager.FindByIdAsync(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: Laboratories/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(string id)
		{
			ApplicationUser user = await UserManager.FindByIdAsync(id);
			var result = await UserManager.DeleteAsync(user);
			if (!result.Succeeded)
			{
				return RedirectToAction("Index", "Error");
			}
			return RedirectToAction("Index");
		}

		// GET: Laboratories/Details/5
		public async Task<ActionResult> Details(string id)
		{
			if (id.IsNullOrWhiteSpace())
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ApplicationUser user = await UserManager.FindByIdAsync(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		public class CreateUserModel
		{
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
			[Display(Name = "Username")]
			[StringLength(40, MinimumLength = 3, ErrorMessage = "Must have length between 3 and 40")]
			public string UserName { get; set; }

			[Display(Name = "User roles")]
			public string[] UserRoles { get; set; }

			[Required]
			[StringLength(100, ErrorMessage = "Value {0} must contain at least {2} symbols.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

		}

		public class ChangeUserPasswordModel
		{
			[Required]
			[StringLength(100, ErrorMessage = "Value {0} must contain at least {2} symbols.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string NewPassword { get; set; }
		}

	}
}