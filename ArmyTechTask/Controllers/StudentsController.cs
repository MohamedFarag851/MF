using ArmyTechTask.Models;
using ArmyTechTask.Models.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ArmyTechTask.Controllers
{
    public class StudentsController : Controller
    {


        private ArmyTechTaskEntities db = new ArmyTechTaskEntities();


        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s=>s.Field ).Include(s=>s.Neighborhood)
                .Include(s=>s.Governorate).ToList();
            return View(students);
        }

        public ActionResult Create()
        {
            StudentViewModel viewModel = new StudentViewModel
            {
                Governorates = db.Governorates.ToList(),
                Neighborhoods = db.Neighborhoods.ToList(),
                Fields = db.Fields.ToList(),
                Teachers=db.Teachers.ToList()

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);

              

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Student student = db.Students.Find(id);
            StudentViewModel model = new StudentViewModel
            {
                Student= db.Students.Find(id)
            };
            if (model.Student == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetNeighborhoods(int id)
        {
            return Json(new SelectList(db.Neighborhoods.Where(empt => (empt.GovernorateId == id)), "ID", "Name"));
        }
    }
}