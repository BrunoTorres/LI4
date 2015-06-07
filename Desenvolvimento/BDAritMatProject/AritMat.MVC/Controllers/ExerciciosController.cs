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
    public class ExerciciosController : Controller
    {
        private BDAritMatProjectEntities db = new BDAritMatProjectEntities();

       
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

            if (exercicio.Imagem != null)
            {
                MemoryStream ms = new MemoryStream(exercicio.Imagem);
                Image img = Image.FromStream(ms);
                img.Save(Server.MapPath("~/Images/Exercicios/E" + exercicio.IdExercicio + ".png"), ImageFormat.Png);
            }
            ViewBag.LicoesAdd = new LicaoDAO().GetLicoesAdd();
            ViewBag.LicoesSub = new LicaoDAO().GetLicoesSub();
            ViewBag.Dicas = exercicio.Dicas.ToList();
            ViewBag.LicaoAtual = Session["NextLicao"] as LicoesViewModel;
            return View(exercicio);
        }

        [HttpPost]
        public ActionResult AnalisaResposta(int licao, int expl, int aluno, int ex, int r)
        {
            var result = new RespostaDAO().IsRespostaCerta(r);

            new LicaoDAO().RegistaResposta(licao, expl, aluno, ex, r);

            // 1 - mostrar exercicio mais dificil, 2 - recomendar proxima lição, 3 - proximo exercicio, 4 - nova explicação, 5 - lição anterior
            int op = new LicaoDAO().OQueFazer(aluno, licao, expl, ex, result);
            System.Diagnostics.Debug.WriteLine("OP: " + op);
            switch (op)
            {
                case 2: // PROX_LIÇÃO
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio{ OQueFazer = LicaoDAO.PROX_LICAO, NextLesson = licao + 1, NextExpl = 1}));
                case 8: // APRENDEU_TODAS
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.APRENDEU_TODAS }));
                case 1: // EXER_MAIS_DIFICIL
                    Exercicio newEx = new ExercicioDAO(db).GetNextExercicioLicaoAluno(aluno, licao, expl);
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.EXER_MAIS_DIFICIL, NextExercicio = newEx.IdExercicio }));
                case 6: // EXER_MAX_DIF
                    List<Exercicio> exsMax = new ExercicioDAO(db).GetExerciciosLicaoMaxDificuldade(licao);

                    // há a possibilidade de sair um que já tenha realizado e falhado
                    Exercicio exMaxDif = exsMax.ElementAt(new Random().Next(0, exsMax.Count - 1));
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.EXER_MAX_DIF, NextExercicio = exMaxDif.IdExercicio }));
                case 7: // EXER_RANDOM
                    List<Exercicio> exs = new ExercicioDAO(db).GetExerciciosLicao(licao);

                    // podem sair exercicios repetidos
                    Exercicio exRand = exs.ElementAt(new Random().Next(0, exs.Count - 1));
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.EXER_RANDOM, NextExercicio = exRand.IdExercicio }));
                case 5: // Lição anterior
                    int tipoAnterior = new LicaoDAO().GetTipoLicao(licao);
                    bool existeAnterior = db.Licoes.Any(l => l.Tipo == tipoAnterior && l.idLicao < licao);
                    int nextLesson = existeAnterior
                            ? licao - 1
                            : licao;
                    int decisao = existeAnterior ? LicaoDAO.LICAO_ANTERIOR : LicaoDAO.NAO_HA_ANTERIOR;
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio{ OQueFazer = decisao, NextLesson = nextLesson, NextExpl = 1 }));
                case 3: // prox exercicio
                    return Json(JsonConvert.SerializeObject(
                        new RespostaAExercicio { OQueFazer = LicaoDAO.PROX_EXER, NextExercicio = new ExercicioDAO(db).ExisteOutroExercicioMesmaDifDisponivel(aluno, licao, ex).IdExercicio }));
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
