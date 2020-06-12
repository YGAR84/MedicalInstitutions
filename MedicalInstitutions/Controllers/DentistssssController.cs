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
//using MedicalInstitutions.Models.Staffs.MedicalStaffs.Surgeons;
//using PagedList;

//namespace MedicalInstitutions.Controllers
//{
//    public class DentistsController : Controller
//    {
//        private MedicalInstitutionsContext db = new MedicalInstitutionsContext();

//        // GET: Dentists
//        public ActionResult Index(int ? pageNum)
//        {
            

//            int SizeOfPage = 4;
//            int NumOfPage = (pageNum ?? 1);

//            var Dentists = db.Dentists.OrderBy(dentist => dentist.Id);
            

//			return View(Dentists.ToPagedList(NumOfPage, SizeOfPage)/*await */) /*ToListAsync().*/;
//		}

//        // GET: Dentists/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            if (dentist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(dentist);
//        }

//        // GET: Dentists/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Dentists/Create
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,NumOfOperations,NumOfFatalOperations,FirstName,SecondName,Salary,Vacation")] Dentist dentist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Dentists.Add(dentist);
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            return View(dentist);
//        }

//        // GET: Dentists/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            if (dentist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(dentist);
//        }

//        // POST: Dentists/Edit/5
//        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
//        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,NumOfOperations,NumOfFatalOperations,FirstName,SecondName,Salary,Vacation")] Dentist dentist)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(dentist).State = EntityState.Modified;
//                await db.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return View(dentist);
//        }

//        // GET: Dentists/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            if (dentist == null)
//            {
//                return HttpNotFound();
//            }
//            return View(dentist);
//        }

//        // POST: Dentists/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Dentist dentist = await db.Dentists.FindAsync(id);
//            db.Dentists.Remove(dentist);
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
