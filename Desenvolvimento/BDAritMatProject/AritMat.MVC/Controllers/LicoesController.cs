﻿using System;
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
                ViewBag.LicaoAtual = lvm;

                // determinar exercício a apresentar no final da lição
                //Exercicio e = new ExercicioDAO().GetNextExercicioLicaoAluno(avm.IdAluno, id);
                Exercicio e = new ExercicioDAO().GetNextExercicioLicaoAluno(avm.IdAluno, id, exp);
                Image img = null;
                string path = null;
                if (e.Imagem != null && e.IdExercicio == 3)
                {
                    MemoryStream ms = new MemoryStream(e.Imagem);
                    img = Image.FromStream(ms);
                    path = Server.MapPath("~") + @"Images\Exercicios\"+e.IdExercicio+".jpg";
                    img.Save(path, ImageFormat.Jpeg);

                }
                else
                {
                    Image imag = Image.FromFile(Server.MapPath("~") + @"Images\Exercicios\"+e.IdExercicio+".jpg");
                    MemoryStream mstream = new MemoryStream();
                    imag.Save(mstream, ImageFormat.Jpeg);
                    byte[] arr = mstream.ToArray();
                    e.Imagem = arr;
                    new ExercicioDAO().UpdateExercicio(e);
                    
                }
                ViewBag.ImagePath = Server.MapPath("~") + @"Images\Exercicios\"+e.IdExercicio+".jpg";
                ViewBag.ExercicioAtual = e;
                ViewBag.Dicas = new DicaDAO().GetDicasExercicio(e.IdExercicio);

                return View();
            }
    
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult AnalisaResposta(int licao, int expl, int aluno, int ex, int r)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = new RespostaDAO().IsRespostaCerta(r);

            string message = "ACERTOU";
            new LicaoDAO().RegistaResposta(licao, expl, aluno, ex, r);
            
            message = result ? "ACERTOU" : "FALHOU!";

            return Json(message);
        }
    }
}
