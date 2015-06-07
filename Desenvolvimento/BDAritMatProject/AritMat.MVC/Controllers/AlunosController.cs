using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AritMat.MVC.DataAccess;
using AritMat.MVC.Models;
using AritMat.MVC.Models.ViewModels;

namespace AritMat.MVC.Controllers
{
    public class AlunosController : Controller
    {
        private BDAritMatProjectEntities db = new BDAritMatProjectEntities();
        private LicaoDAO licaoDAO;

        public AlunosController()
        {
        }


        // GET: Alunos
        public ActionResult Index(AlunoViewModel model)
        {
            var avm = Session["User"] as AlunoViewModel;

            if (avm != null)
            {
                Licao l = new LicaoDAO().GetNextLicaoAluno(avm.IdAluno);
                double percCertas = new ExercicioDAO(db).GetPercentCertas(avm.IdAluno);
                int certas = new ExercicioDAO(db).GetNumCertas(avm.IdAluno);
                int testesFeitos = new TesteDAO(db).GetNumTestesFeitosAluno(avm.IdAluno);
                double percentTestes = new TesteDAO(db).GetPercentTestesAluno(avm.IdAluno);

                ViewBag.PercentCertas = percCertas;
                ViewBag.Certas = certas;
                ViewBag.TestesFeitos = testesFeitos;
                ViewBag.PercentTestes = percentTestes;

                System.Diagnostics.Debug.WriteLine("NTestes: " + testesFeitos + "\nMEDIA TESTES: " + percentTestes +
                    "\nNCertas: " + certas + "\nPercCertas: " + percCertas);

                ViewBag.NextLicao = l;

                ViewBag.LicoesAdd = new LicaoDAO().GetLicoesAdd();
                ViewBag.LicoesSub = new LicaoDAO().GetLicoesSub();
                return View(avm);
            }

            return View(model);
        }

        // GET: Alunos
        /*public ActionResult Index()
        {
            return RedirectToAction("Login", "Home");
        }*/


        // GET: Alunos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var avm = Session["User"] as AlunoViewModel;

            if (avm != null && avm.IdAluno == id)
            {
                Aluno aluno = db.Alunos.Find(id);
                if (aluno == null)
                {
                    return HttpNotFound();
                }
                AlunoEditModel aem = new AlunoEditModel(aluno);
                return View(aem);
            }
            
            return RedirectToAction("Login", "Home");
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAluno,Nome,Username,Password,DataNasc,Dica,Tema,Explicacao,Pontuacao")] Aluno aluno)
        {
            var result = new AlunoDAO().ChangeAluno(aluno);

            if (result)
            {
                return RedirectToAction("Details", "Alunos", new{id = aluno.IdAluno});
            }
            ModelState.AddModelError("Edit", "Erro, não conseguiu adicionar os novos detalhes");
            AlunoEditModel aem = new AlunoEditModel(aluno);
            return View(aem);

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var avm = Session["User"] as AlunoViewModel;

            if (avm != null && avm.IdAluno == id)
            {
                Aluno aluno = db.Alunos.Find(id);
                if (aluno == null)
                {
                    return HttpNotFound();
                }
                AlunoDetailsModel adm = new AlunoDetailsModel(aluno);
                return View(adm);
            }
            return RedirectToAction("Login", "Home");
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
