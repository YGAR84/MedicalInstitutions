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
//    public class NursesController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Nurses
//        public async Task<ActionResult> Index()
//        {
//            return View(await db.Nurses.ToListAsync());
//        }

//        // GET: Nurses/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Nurse nurse = await db.Nurses.FindAsync(id);
//            if (nurse == null)
//            {
//                return HttpNotFound();
//            }
//            return View(nurse);
//        }

//        // GET: Nurses/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Nurses/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Nurse nurse)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Nurses.Add(nurse);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            return View(nurse);
//        }

//        // GET: Nurses/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Nurse nurse = await db.Nurses.FindAsync(id);
//            if (nurse == null)
//            {
//                return HttpNotFound();
//            }
//            return View(nurse);
//        }

//        // POST: Nurses/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Salary,Vacation,EmploymentDate,SalaryAddition,VacationAddition,FirstName,SecondName")] Nurse nurse)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(nurse).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return View(nurse);
//        }

//        // GET: Nurses/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Nurse nurse = await db.Nurses.FindAsync(id);
//            if (nurse == null)
//            {
//                return HttpNotFound();
//            }
//            return View(nurse);
//        }

//        // POST: Nurses/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Nurse nurse = await db.Nurses.FindAsync(id);
//            db.Nurses.Remove(nurse);
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
