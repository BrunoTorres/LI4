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

       // GET: Testes/Resolver/
        public new ActionResult Resolver()
        {
            if(Session["User"] == null)
                return RedirectToAction("Login", "Home");
            Teste teste = new TesteDAO(db).GetTeste();
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
            ViewBag.LicoesAdd = new LicaoDAO().GetLicoesAdd();
            ViewBag.LicoesSub = new LicaoDAO().GetLicoesSub();
            
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


            new TesteDAO(db).RegistaRespostasTeste(aluno, teste, resps);

            return Json(JsonConvert.SerializeObject(
                new RespostaAExercicio
                {
                    Certas = certas,
                    Erradas = 8 - certas
                }));
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
