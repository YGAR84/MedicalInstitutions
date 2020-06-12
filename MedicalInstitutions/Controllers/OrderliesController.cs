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
//    public class OrderliesController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Orderlies
//        public async Task<ActionResult> Index()
//        {
//            return View(await db.Orderlies.ToListAsync());
//        }

//        // GET: Orderlies/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Orderly orderly = await db.Orderlies.FindAsync(id);
//            if (orderly == null)
//            {
//                return HttpNotFound();
//            }
//            return View(orderly);
//        }

//        // GET: Orderlies/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Orderlies/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Orderly orderly)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Orderlies.Add(orderly);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            return View(orderly);
//        }

//        // GET: Orderlies/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Orderly orderly = await db.Orderlies.FindAsync(id);
//            if (orderly == null)
//            {
//                return HttpNotFound();
//            }
//            return View(orderly);
//        }

//        // POST: Orderlies/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Orderly orderly)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(orderly).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return View(orderly);
//        }

//        // GET: Orderlies/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Orderly orderly = await db.Orderlies.FindAsync(id);
//            if (orderly == null)
//            {
//                return HttpNotFound();
//            }
//            return View(orderly);
//        }

//        // POST: Orderlies/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Orderly orderly = await db.Orderlies.FindAsync(id);
//            db.Orderlies.Remove(orderly);
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
