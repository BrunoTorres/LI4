using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AritMat.MVC.DataAccess;
using AritMat.MVC.JSonAux;
using AritMat.MVC.Models;
using AritMat.MVC.Models.ViewModels;
using Newtonsoft.Json;

namespace AritMat.MVC.Controllers
{
    public class LicoesController : Controller
    {
        private BDAritMatProjectEntities db = new BDAritMatProjectEntities();

        // GET: Licoes
        public ActionResult Index()
        {
            var licoes = db.Licoes.Include(l => l.AdministradorCriou).Include(l => l.TipoLicao);
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
        public ActionResult Create(
            [Bind(Include = "idLicao,NumExpl,Tipo,Administrador,Texto,Video,Imagem,TempoLicao")] Licao licao)
        {
            if (ModelState.IsValid)
            {
                db.Licoes.Add(licao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username",
                licao.Administrador);
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
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username",
                licao.Administrador);
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area", licao.Tipo);
            return View(licao);
        }

        // POST: Licoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "idLicao,NumExpl,Tipo,Administrador,Texto,Video,Imagem,TempoLicao")] Licao licao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username",
                licao.Administrador);
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

        public ActionResult VerLicao(int id, int exp = 1)
        {
            AlunoViewModel avm = Session["User"] as AlunoViewModel;
            if (avm != null)
            {
                ViewBag.LicoesAdd = new LicaoDAO().GetLicoesAdd();
                ViewBag.LicoesSub = new LicaoDAO().GetLicoesSub();

                // determinar explicação a apresentar dentro da lição escolhida
                LicoesViewModel lvm = new LicoesViewModel(new LicaoDAO().GetLicao(id, exp));
                Tipo t = new TipoDAO().GetTipoLicao(lvm.Tipo);
                string area = t.Area;
                lvm.Area = area;
                if (lvm.Imagem != null)
                {
                    MemoryStream ms = new MemoryStream(lvm.Imagem);
                    Image img = Image.FromStream(ms);
                    img.Save(Server.MapPath("~/Images/Licoes/L" + lvm.IdLicao + "E" + lvm.NumExpl + ".png"), ImageFormat.Png);
                }
                
                ViewBag.LicaoAtual = lvm;
                ViewBag.Exercicio = new ExercicioDAO().GetNextExercicioLicaoAluno(avm.IdAluno, lvm.IdLicao, lvm.NumExpl).IdExercicio;
                Session["NextLicao"] = lvm;

                return View();
            }

            return RedirectToAction("Login", "Home");
        }

    }
}
