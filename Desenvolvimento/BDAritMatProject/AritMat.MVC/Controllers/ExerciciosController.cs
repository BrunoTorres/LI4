using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class ExerciciosController : Controller
    {
        private BDAritMatProjectEntities db = new BDAritMatProjectEntities();

        // GET: Exercicios
        public ActionResult Index()
        {
            var exercicios = db.Exercicios.Include(e => e.AdministradorCriou).Include(e => e.TipoExercicio);
            return View(exercicios.ToList());
        }

        // GET: Exercicios/Details/5
        public ActionResult Resolver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(Session["User"] == null || Session["NextLicao"] == null)
                return RedirectToAction("Login","Home");

            Exercicio exercicio = db.Exercicios.Find(id);
            if (exercicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dicas = exercicio.Dicas.ToList();
            ViewBag.LicaoAtual = Session["NextLicao"] as LicoesViewModel;
            return View(exercicio);
        }

        // GET: Exercicios/Create
        public ActionResult Create()
        {
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username");
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area");
            return View();
        }

        // POST: Exercicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdExercicio,Tipo,Administrador,Texto,Dificuldade,Imagem,TempoEx")] Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                db.Exercicios.Add(exercicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username", exercicio.Administrador);
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area", exercicio.Tipo);
            return View(exercicio);
        }

        // GET: Exercicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicio exercicio = db.Exercicios.Find(id);
            if (exercicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username", exercicio.Administrador);
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area", exercicio.Tipo);
            return View(exercicio);
        }

        // POST: Exercicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdExercicio,Tipo,Administrador,Texto,Dificuldade,Imagem,TempoEx")] Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Administrador = new SelectList(db.administradores, "IdAdministrador", "Username", exercicio.Administrador);
            ViewBag.Tipo = new SelectList(db.Tipos, "IdTipo", "Area", exercicio.Tipo);
            return View(exercicio);
        }

        // GET: Exercicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercicio exercicio = db.Exercicios.Find(id);
            if (exercicio == null)
            {
                return HttpNotFound();
            }
            return View(exercicio);
        }

        // POST: Exercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercicio exercicio = db.Exercicios.Find(id);
            db.Exercicios.Remove(exercicio);
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

        [HttpPost]
        public ActionResult AnalisaResposta(int licao, int expl, int aluno, int ex, int r)
        {
            var result = new RespostaDAO().IsRespostaCerta(r);

            new LicaoDAO().RegistaResposta(licao, expl, aluno, ex, r);

            // 1 - mostrar exercicio mais dificil, 2 - recomendar proxima lição, 3 - proximo exercicio, 4 - nova explicação, 5 - lição anterior
            int op = new LicaoDAO().OQueFazer(aluno, licao, expl, ex, result);
            switch (op)
            {
                case 2: // PROX_LIÇÃO
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio{ OQueFazer = LicaoDAO.PROX_LICAO, NextLesson = licao + 1, NextExpl = 1}));
                case 8: // APRENDEU_TODAS
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.APRENDEU_TODAS }));
                case 1: // EXER_MAIS_DIFICIL
                    Exercicio newEx = new ExercicioDAO().GetNextExercicioLicaoAluno(aluno, licao, expl);
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.EXER_MAIS_DIFICIL, NextExercicio = newEx.IdExercicio }));
                case 6: // EXER_MAX_DIF
                    List<Exercicio> exsMax = new ExercicioDAO().GetExerciciosLicaoMaxDificuldade(licao);

                    // há a possibilidade de sair um que já tenha realizado e falhado
                    Exercicio exMaxDif = exsMax.ElementAt(new Random().Next(0, exsMax.Count - 1));
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.EXER_MAX_DIF, NextExercicio = exMaxDif.IdExercicio }));
                case 7: // EXER_RANDOM
                    List<Exercicio> exs = new ExercicioDAO().GetExerciciosLicao(licao);

                    // podem sair exercicios repetidos
                    Exercicio exRand = exs.ElementAt(new Random().Next(0, exs.Count - 1));
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.EXER_RANDOM, NextExercicio = exRand.IdExercicio }));
                case 5: // Lição anterior
                    int tipoAnterior = new LicaoDAO().GetTipoLicao(licao);
                    int nextLesson = db.Licoes.Any(l => l.Tipo == tipoAnterior && l.idLicao < licao)
                            ? licao - 1
                            : 1;
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio{ OQueFazer = LicaoDAO.LICAO_ANTERIOR, NextLesson = nextLesson, NextExpl = 1 }));
                case 3: // prox exercicio
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.PROX_EXER, NextExercicio = new ExercicioDAO().ExisteOutroExercicioMesmaDifDisponivel(aluno, licao, ex).IdExercicio }));
                case 4: // prox explicação
                    Licao nextL = new LicaoDAO().GetLicao(licao, expl + 1);
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.PROX_EXPLICACAO, NextLesson = licao, NextExpl = nextL != null ? nextL.NumExpl : 1 }));
            }

            return Json(JsonConvert.SerializeObject(
                new RespostaAExercicio { OQueFazer = LicaoDAO.PROX_EXER }));
        }
    }
}
