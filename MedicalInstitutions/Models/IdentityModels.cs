using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MedicalInstitutions.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
		MedicalInstitutionsContext db = new MedicalInstitutionsContext();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
	        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        public string GetRoles
        {
	        get
	        {
		        var userRoles = db.Roles
			        .Where(r => r.Users
				        .Any(ur => ur.UserId == Id))
			        .Select(r => r.Name)
			        .ToList();
		        return string.Join(" | ", userRoles.ToArray());
	        }
        }
    }

 //   public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
 //   {
 //       public ApplicationDbContext()
 //           : base("MedicalInstitutionsContext2")
 //       {
 //       }

 //       public static ApplicationDbContext Create()
 //       {
 //           return new ApplicationDbContext();
 //       }
 //   }

	//public class ApplicationDbDbInitializer : //DropCreateDatabaseIfModelChanges<MedicalInstitutionsContext>//
	//	DropCreateDatabaseAlways<ApplicationDbContext>
	//{
	//	protected override void Seed(ApplicationDbContext context)
	//	{
	//		//context.Dentists.AddRange(_dentistsToAdd);
	//		//context.Hospitals.AddRange(_hospitalsToAdd);

	//		var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

	//		var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

	//		var role1 = new IdentityRole { OperationName = "admin" };
	//		var role2 = new IdentityRole { OperationName = "user" };

	//		roleManager.Create(role1);
	//		roleManager.Create(role2);

	//		var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
	//		string password = "admin2";
	//		var result = userManager.Create(admin, password);

	//		if (result.Succeeded)
	//		{
	//			// добавляем для пользователя роль
	//			userManager.AddToRole(admin.Id, role1.OperationName);
	//			userManager.AddToRole(admin.Id, role2.OperationName);
	//		}

	//		context.SaveChanges();
	//		base.Seed(context);
	//	}
	//}
}