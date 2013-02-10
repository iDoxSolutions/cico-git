using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.DAL;
using Cico.Models;

namespace Cico.Controllers
{
    public class ChecklistController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Checklist/

        public ActionResult Index(int? SelectedChecklist)
        {
            var checklists = unitOfWork.ChecklistRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.SelectedChecklist = new SelectList(checklists, "DepartmentID", "Name", SelectedChecklist);

            int checklistID = SelectedChecklist.GetValueOrDefault();
            return View(unitOfWork.ChecklistRepository.Get(
                filter: c => !SelectedChecklist.HasValue || c.Id == checklistID,
                orderBy: q => q.OrderBy(d => d.Id),
                includeProperties: "Department"));
        }

        //
        // GET: /Course/Details/5

        public ActionResult Details(int id)
        {
            var query = "SELECT * FROM Checklist WHERE ChecklistID = @p0";
            return View(unitOfWork.CourseRepository.GetWithRawSql(query, id).Single());
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CheckList checklist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ChecklistRepository.Insert(checklist);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(checklist.Id);
            return View(checklist);
        }

        public ActionResult Edit(int id)
        {
            CheckList checklist = unitOfWork.ChecklistRepository.GetByID(id);
            PopulateDepartmentsDropDownList(checklist.Id);
            return View(checklist);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CourseRepository.Update(course);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (add a variable name after DataException)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = unitOfWork.DepartmentRepository.Get(
                orderBy: q => q.OrderBy(d => d.Name));
            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
        }

        //
        // GET: /Course/Delete/5

        public ActionResult Delete(int id)
        {
            Course course = unitOfWork.CourseRepository.GetByID(id);
            return View(course);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = unitOfWork.CourseRepository.GetByID(id);
            unitOfWork.CourseRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        //public ActionResult UpdateCourseCredits(int? multiplier)
        //{
        //    if (multiplier != null)
        //    {
        //        ViewBag.RowsAffected = unitOfWork.CourseRepository.UpdateCourseCredits(multiplier.Value);
        //    }
        //    return View();
        //}
        
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}