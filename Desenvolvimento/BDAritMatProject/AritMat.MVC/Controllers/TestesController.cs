using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AritMat.MVC.Models;

namespace AritMat.MVC.Controllers
{
    public class TestesController : Controller
    {
        private BDAritMatProjectEntities db = new BDAritMatProjectEntities();

        // GET: Testes
        public ActionResult Index()
        {
            return View(db.Testes.ToList());
        }

        // GET: Testes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teste teste = db.Testes.Find(id);
            if (teste == null)
            {
                return HttpNotFound();
            }
            return View(teste);
        }

        // GET: Testes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Testes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTeste,Dificuldade")] Teste teste)
        {
            if (ModelState.IsValid)
            {
                db.Testes.Add(teste);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teste);
        }

        // GET: Testes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teste teste = db.Testes.Find(id);
            if (teste == null)
            {
                return HttpNotFound();
            }
            return View(teste);
        }

        // POST: Testes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTeste,Dificuldade")] Teste teste)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teste).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teste);
        }

        // GET: Testes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teste teste = db.Testes.Find(id);
            if (teste == null)
            {
                return HttpNotFound();
            }
            return View(teste);
        }

        // POST: Testes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teste teste = db.Testes.Find(id);
            db.Testes.Remove(teste);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
