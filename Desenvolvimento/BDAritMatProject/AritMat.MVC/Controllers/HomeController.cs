using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AritMat.MVC.DataAccess;
using AritMat.MVC.Models;
using AritMat.MVC.Models.ViewModels;
using Microsoft.AspNet.Identity.Owin;

namespace AritMat.MVC.Controllers
{
    public class HomeController : Controller
    {
        BDAritMatProjectEntities db = new BDAritMatProjectEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "Sobre";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contatos";

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AlunoLoginModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = new AlunoDAO().CheckLogin(model);

            if (result)
            {
                Aluno al = db.Alunos.SqlQuery("SELECT * FROM Aluno WHERE Username = '" + model.Username + "'").FirstOrDefault();
                Session["User"] = new AlunoViewModel(al);

                TempData["User"] = model;
                return RedirectToAction("Index", "Alunos");
            }
            if (TempData["User"] != null) return RedirectToAction("Index", "Alunos", model);
            
            ViewData["User"] = null;
            ModelState.AddModelError("Login", "Falha na autenticação");
            ViewBag.User = null;
            Session["User"] = null;
            TempData["User"] = null;

            return View(model);
        }

        public ActionResult Register()
        {
            if(Session["User"] != null)
                return RedirectToAction("Index","Alunos");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(AlunoRegisterModel model)
        {
           var result = new AlunoDAO().AddAluno(model);

            if (result)
            {
                return RedirectToAction("Login", "Home");
            }
            ModelState.AddModelError("Register", "Username já em uso");
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            ViewBag.LicaoAtual = null;
            return RedirectToAction("Index");
        }
    }
}