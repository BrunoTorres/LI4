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
                ViewBag.LicoesAdd = new LicaoDAO().GetLicoesAdd();
                ViewBag.LicoesSub = new LicaoDAO().GetLicoesSub();
                ViewBag.Exercicio = new ExercicioDAO(db).GetNextExercicioLicaoAluno(avm.IdAluno, lvm.IdLicao, lvm.NumExpl).IdExercicio;
                Session["NextLicao"] = lvm;

                return View();
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult ProximaExplicacao(int licao, int numExpl)
        {
            Licao l = db.Licoes.Find(licao, numExpl + 1);
            if (l != null)
                return Json(JsonConvert.SerializeObject(
                    new RespostaAExercicio
                    {
                        OQueFazer = LicaoDAO.PROX_EXPLICACAO
                    }));

            return Json(JsonConvert.SerializeObject(
                new RespostaAExercicio
                {
                    OQueFazer = LicaoDAO.LICAO_ANTERIOR
                }));
        }

        [HttpPost]
        public ActionResult ProximaLicao(int licao, int numExpl)
        {
            Licao l = db.Licoes.Find(licao + 1, 1);
            if (l != null && l.Tipo == db.Licoes.Find(licao, 1).Tipo)
                return Json(JsonConvert.SerializeObject(
                    new RespostaAExercicio
                    {
                        OQueFazer = LicaoDAO.PROX_LICAO
                    }));

            return Json(JsonConvert.SerializeObject(
                new RespostaAExercicio
                {
                    OQueFazer = LicaoDAO.PROX_EXPLICACAO
                }));
        }

    }
}
