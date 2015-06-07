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
using Newtonsoft.Json;

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

        // GET: Testes/Resolver/
        public ActionResult Resolver()
        {
            if(Session["User"] == null)
                return RedirectToAction("Login", "Home");
            Teste teste = new TesteDAO().GetTeste();
            if (teste == null)
            {
                return HttpNotFound();
            }
            foreach (var ex in teste.ExerciciosDoTeste.Where(e => e.Imagem != null))
            {
                MemoryStream ms = new MemoryStream(ex.Imagem);
                Image img = Image.FromStream(ms);
                img.Save(Server.MapPath("~/Images/Exercicios/E" + ex.IdExercicio + ".png"), ImageFormat.Png);
            }
            
            return View(teste);
        }

        [HttpPost]
        public ActionResult AnalisaTeste(int aluno, int teste, int r0, int r1, int r2, int r3, int r4, int r5, int r6, int r7)
        {
            int certas = 0;

            certas += new RespostaDAO().IsRespostaCerta(r0) ? 1 : 0;
            certas += new RespostaDAO().IsRespostaCerta(r1) ? 1 : 0;
            certas += new RespostaDAO().IsRespostaCerta(r2) ? 1 : 0;
            certas += new RespostaDAO().IsRespostaCerta(r3) ? 1 : 0;
            certas += new RespostaDAO().IsRespostaCerta(r4) ? 1 : 0;
            certas += new RespostaDAO().IsRespostaCerta(r5) ? 1 : 0;
            certas += new RespostaDAO().IsRespostaCerta(r6) ? 1 : 0;
            certas += new RespostaDAO().IsRespostaCerta(r7) ? 1 : 0;

            List<int> resps = new List<int>();
            resps.Add(r0);
            resps.Add(r1);
            resps.Add(r2);
            resps.Add(r3);
            resps.Add(r4);
            resps.Add(r5);
            resps.Add(r6);
            resps.Add(r7);


            new TesteDAO().RegistaRespostasTeste(aluno, teste, resps);

            return Json(JsonConvert.SerializeObject(
                new RespostaAExercicio
                {
                    Certas = certas,
                    Erradas = 8 - certas
                }));
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
