//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using MedicalInstitutions.Models;
//using MedicalInstitutions.Models.Staffs.ServiceStaffs;

//namespace MedicalInstitutions.Controllers
//{
//    public class JanitorsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Janitors
//        public async Task<ActionResult> Index()
//        {
//            return View(await db.Janitors.ToListAsync());
//        }

//        // GET: Janitors/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Janitor janitor = await db.Janitors.FindAsync(id);
//            if (janitor == null)
//            {
//                return HttpNotFound();
//            }
//            return View(janitor);
//        }

//        // GET: Janitors/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Janitors/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Janitor janitor)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Janitors.Add(janitor);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            return View(janitor);
//        }

//        // GET: Janitors/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Janitor janitor = await db.Janitors.FindAsync(id);
//            if (janitor == null)
//            {
//                return HttpNotFound();
//            }
//            return View(janitor);
//        }

//        // POST: Janitors/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Janitor janitor)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(janitor).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return View(janitor);
//        }

//        // GET: Janitors/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Janitor janitor = await db.Janitors.FindAsync(id);
//            if (janitor == null)
//            {
//                return HttpNotFound();
//            }
//            return View(janitor);
//        }

//        // POST: Janitors/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Janitor janitor = await db.Janitors.FindAsync(id);
//            db.Janitors.Remove(janitor);
//            await db.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
