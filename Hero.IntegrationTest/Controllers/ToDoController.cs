using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hero.Attributes;
using Hero.IntegrationTest.Models;

namespace Hero.IntegrationTest.Controllers
{
    [AbilityMvcAuthorization(Ability = "ToDoView")]
    public class ToDoController : Controller
    {
        private ToDoDbContext db = new ToDoDbContext();

        //
        // GET: /ToDo/

        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        //
        // GET: /ToDo/Details/5

        public ActionResult Details(int id = 0)
        {
            ToDo todo = db.Items.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // GET: /ToDo/Create

        [AbilityMvcAuthorization(Ability = "Manage")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ToDo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AbilityMvcAuthorization(Ability = "Manage")]
        public ActionResult Create(ToDo todo)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        //
        // GET: /ToDo/Edit/5
        [AbilityMvcAuthorization(Ability = "Manage")]
        public ActionResult Edit(int id = 0)
        {
            ToDo todo = db.Items.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // POST: /ToDo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AbilityMvcAuthorization(Ability = "Manage")]
        public ActionResult Edit(ToDo todo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        //
        // GET: /ToDo/Delete/5
        [AbilityMvcAuthorization(Ability = "Manage")]
        public ActionResult Delete(int id = 0)
        {
            ToDo todo = db.Items.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // POST: /ToDo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AbilityMvcAuthorization(Ability = "Manage")]
        public ActionResult DeleteConfirmed(int id)
        {
            ToDo todo = db.Items.Find(id);
            db.Items.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}