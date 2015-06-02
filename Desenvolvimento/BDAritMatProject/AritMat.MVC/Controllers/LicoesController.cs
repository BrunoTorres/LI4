using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AritMat.MVC.DataAccess;
using AritMat.MVC.Models;
using AritMat.MVC.Models.ViewModels;

namespace AritMat.MVC.Controllers
{
    public class LicoesController : Controller
    {
        private BDAritMatProjectEntities db = new BDAritMatProjectEntities();

        // GET: Licoes
        public ActionResult Index()
        {
            var licoes = db.Licoes.Include(l => l.Administrador1).Include(l => l.Tipo1);
            return View(licoes.ToList());
        }

        // GET: Licoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licao licao = db.Licoes.Find(id);
            if (licao == null)
            {
                return HttpNotFound();
            }
            return View(licao);
        }

        // GET: Licoes/Create
        public ActionResult Create()
        {
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username");
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area");
            return View();
        }

        // POST: Licoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLicao,NumExpl,Tipo,Administrador,Texto,Video,Imagem,TempoLicao")] Licao licao)
        {
            if (ModelState.IsValid)
            {
                db.Licoes.Add(licao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username", licao.Administrador);
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area", licao.Tipo);
            return View(licao);
        }

        // GET: Licoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licao licao = db.Licoes.Find(id);
            if (licao == null)
            {
                return HttpNotFound();
            }
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username", licao.Administrador);
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area", licao.Tipo);
            return View(licao);
        }

        // POST: Licoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLicao,NumExpl,Tipo,Administrador,Texto,Video,Imagem,TempoLicao")] Licao licao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username", licao.Administrador);
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area", licao.Tipo);
            return View(licao);
        }

        // GET: Licoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licao licao = db.Licoes.Find(id);
            if (licao == null)
            {
                return HttpNotFound();
            }
            return View(licao);
        }

        // POST: Licoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Licao licao = db.Licoes.Find(id);
            db.Licoes.Remove(licao);
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

        public ActionResult VerLicao(int id)
        {
            AlunoViewModel avm = Session["User"] as AlunoViewModel;
            ViewBag.LicoesAdd =
                db.Licoes.SqlQuery(
                    "SELECT * FROM Licao AS LI INNER JOIN Tipo AS TI ON LI.Tipo = TI.IdTipo WHERE TI.Area = 'Adição'")
                    .ToList();
            ViewBag.LicoesSub =
                db.Licoes.SqlQuery(
                    "SELECT * FROM Licao AS LI INNER JOIN Tipo AS TI ON LI.Tipo = TI.IdTipo WHERE TI.Area = 'Subtração'")
                    .ToList();

            // determinar explicação a apresentar dentro da lição escolhida
            List<Licao> listLicao =
                db.Licoes.SqlQuery("SELECT * FROM Licao WHERE IdLicao = " + id + " AND NumExpl = 3").ToList();
            LicoesViewModel lvm = new LicoesViewModel(listLicao.First());
            Tipo t = db.Tipos.Find(lvm.Tipo);
            string area = t.Area;
            lvm.Area = area;
            ViewBag.LicaoAtual = lvm;

            // determinar exercício a apresentar no final da lição
            ViewBag.ExercicioAtual = new ExercicioDAO().GetNextExercicioLicaoAluno(avm.IdAluno, id);
            
            return View();
        }
    }
}
