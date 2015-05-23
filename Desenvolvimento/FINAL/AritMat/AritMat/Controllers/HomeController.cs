using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AritMat.BOL;

namespace AritMat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public string CheckLogin()
        {
            Aluno a = new BOL.AritMat().UserLogin(Request["username"], Request["password"]);
            return a != null ? "ok" : "error";
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckRegister()
        {
            string pw = Request["pw"];
            string confpw = Request["confpw"];
            
            Aluno a = new Aluno();
            a.SetNome(Request["nome"]);
            a.SetUsername(Request["user"]);
            a.SetPassword(Request["pw"]);

            /*if(!Request["data"].Equals("NA"))
                a.SetDataNascimento(DateTime.Parse(Request["data"]));*/

            bool r = new BOL.AritMat().Registar(a);

            string t = r ? "true" : "false";

            return r ? RedirectToAction("Login") : RedirectToAction("Contact");
        }
    }
}