using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AritMat.MVC.Models;
using Microsoft.Ajax.Utilities;

namespace AritMat.MVC.DataAccess
{
    public class LicaoDAO
    {
        private BDAritMatProjectEntities db;
        private ExercicioDAO exercicioDAO;
        private TipoDAO tipoDAO;


        public LicaoDAO()
        {
            db = new BDAritMatProjectEntities();
            //exercicioDAO = new ExercicioDAO();
            //tipoDAO = new TipoDAO();
        }

        public Licao GetNextLicaoAluno(int idAluno)
        {
            /*if (db.AlunoLicoes.Count(a => a.Aluno == idAluno) == 0)
                return GetLicao(1, 1);*/
            Aluno a = db.Alunos.Find(idAluno);

            if (a.LicoesVistas.Count == 0)
                return GetLicao(1, 1);

            int lastId = a.LicoesVistas.OrderByDescending(al => al.Data).First().Licao;

            List<AlunoLicao> explsVistas = a.LicoesVistas.OrderByDescending(l => l.Data).Where(l => l.Licao == lastId).ToList();

            bool todasNull = true;
            int i = 0;

            while (i < explsVistas.Count && todasNull)
            {
                if (explsVistas.ElementAt(i).RespErradas != null)
                    todasNull = false;
                i++;
            }

            // ainda nao respondeu a exercícios nesta lição -> mostrar a última vista
            if(todasNull)
                return GetLicao(explsVistas.First().Licao, explsVistas.First().Explicacao);

            int certas = a.ExerciciosEmLicao.Count(l => l.Resposta > 0 && l.Licao == lastId);
            int totalResp = a.ExerciciosEmLicao.Count(l => l.Licao == lastId);
            double percentCertas = (double)certas/(double)totalResp;

            System.Diagnostics.Debug.WriteLine("CERTAS: " + certas + " | % CERTAS: " + percentCertas + " | TOTAL: " + totalResp);

            int totalExsLicao = new ExercicioDAO().GetNumExerciciosLicao(lastId);
            System.Diagnostics.Debug.WriteLine("TOTAL EXS: " + totalExsLicao);

            // aprendeu a lição anterior
            if (percentCertas > 0.75 && a.ExerciciosEmLicao.Count(l => l.Licao == lastId) > (int)(0.5 * totalExsLicao))
            {
                // ultima lição disponivel
                if (lastId == db.Licoes.OrderByDescending(l => l.idLicao).First().idLicao)
                {
                    return null;
                }

                // proxima lição
                return GetLicao(lastId + 1, 1);
            }

            // não aprendeu a lição anterior e passou o limite de repostas errada -> mostrar lição anterior
            int firstLesson = db.Licoes.OrderBy(l => l.idLicao).First().idLicao;

            double percentErradas = (double)(totalResp - certas) / (double)totalResp;
            if (percentErradas > 0.75 && a.ExerciciosEmLicao.Count(l => l.Licao == lastId) > (int)(0.5 * totalExsLicao) && lastId != firstLesson)
            {
                // ir para lição anterior à última que viu
                return GetLicao(lastId-1, 1);
            }

            // ir para próxima explicação da ultima lição que viu, se existir
            int lastExpl =
                a.LicoesVistas.OrderByDescending(l => l.Data).First(l => l.Licao == lastId).Explicacao;

            if(db.Licoes.Count(l => l.idLicao == lastId && l.NumExpl == lastExpl + 1) > 0)
                return GetLicao(lastId, explsVistas.First().Explicacao + 1);

            System.Diagnostics.Debug.WriteLine("GET NEXT LESSON - FIM");
            // não existem mais explicações -> mostrar a primeira
            return GetLicao(lastId, 1);
        }


        private int GetNumLicoesTipo(int tipoLicao)
        {
            return db.Licoes.DistinctBy(l => l.idLicao).Where(l => l.Tipo == tipoLicao).ToList().Count;
        }

        private int GetNumExplicacoesLicao(int idLicao)
        {
            return db.Licoes.Count(l => l.idLicao == idLicao);
        }

        public int GetTipoLicao(int nextLicao)
        {
            return db.Licoes.SqlQuery("SELECT * FROM Licao WHERE IdLicao = " + nextLicao).First().Tipo;
        }

        public List<Licao> GetLicoesAdd()
        {
            return db.Licoes.Distinct().Where(l => l.Tipo == 1).ToList();
        }

        public List<Licao> GetLicoesSub()
        {
            return db.Licoes.Where(l => l.Tipo == 2).ToList();
        }
        /*public Exercicio GetNextExercicioLicaoAluno(int idAluno, int idLicao)
        {
            List<AlunoExercicioLicao> exercicios = exercicioDAO.GetExerciciosAlunoLicao(idAluno, idLicao);

            if(exercicios.Count == 0)
                return exercicioDAO.GetFirstExercicioTipo(GetTipoLicao(idLicao));

            AlunoExercicioLicao ael = new ExercicioDAO().GetLastExercicioDone(idAluno, idLicao);

            int dificuldade = db.Exercicios.Find(ael.Exercicio).Dificuldade;

            if (ael.Resposta > 0) // acertou resposta do último exercício
                // não está a verificar se já fez o exercício selecionado
                return
                    db.Exercicios.Where(ex => ex.Tipo == GetTipoLicao(idLicao) && ex.Dificuldade > dificuldade)
                        .OrderBy(ex => ex.Dificuldade)
                        .First();

            return db.Exercicios.Where(ex => ex.Dificuldade <= dificuldade && ex.IdExercicio != ael.Exercicio)
                        .OrderByDescending(ex => ex.Dificuldade)
                        .First();

        }*/

        public Licao GetLicao(int id, int exp)
        {
            return db.Licoes.Find(id, exp);
        }

        public float Formula(int pont, int dif, int certas, int erradas, int nLicoes)
        {
            int fator;

            System.Diagnostics.Debug.WriteLine("PONT: " + pont + "\nDIF: " + dif + "\nCERTAS: " + certas + "\nERRADAS: " + erradas + "\nLICOES: " + nLicoes);

            if((certas - erradas) < 1)
                return ((pont * dif) / nLicoes);

            return ((pont * dif * (certas - erradas)) / nLicoes);
        }

        // transação que regista as alterações na BD quando resolve um exercício!
        public void RegistaResposta(int licao, int expl, int aluno, int ex, int r)
        {
            var dbTransaction = db.Database.BeginTransaction();
            Aluno a = db.Alunos.Find(aluno);
            System.Diagnostics.Debug.WriteLine("ALUNO");
            try
            {
                AlunoExercicioLicao ael = new AlunoExercicioLicao();

                ael.Licao = licao;
                ael.Explicacao = expl;
                ael.Aluno = aluno;
                ael.Resposta = new RespostaDAO().GetPontuacao(r);
                ael.Exercicio = ex;
                ael.Data = System.DateTime.Now;

                db.AlunoExercicioLicoes.AddOrUpdate(ael);
                System.Diagnostics.Debug.WriteLine("ALUNO-EXERCICIO-LICAO");
                //AlunoLicao alAnterior = GetAlunoLicaoMaisRecente(aluno, licao);

                AlunoLicao alAnterior = null;
                if (a.LicoesVistas.Any(l => l.Licao == licao && l.Explicacao == expl))
                    alAnterior = a.LicoesVistas.First(l => l.Licao == licao && l.Explicacao == expl);

                System.Diagnostics.Debug.WriteLine("AFTER ALANTERIOR");
                AlunoLicao al = new AlunoLicao();
                al.Aluno = aluno;
                al.Explicacao = expl;
                al.Licao = licao;
                al.Data = System.DateTime.Now;
                if (alAnterior == null)
                {
                    if (ael.Resposta < 0)
                    {
                        al.RespErradas = 1;
                        al.RespCertas = 0;
                    }
                    else
                    {
                        al.RespErradas = 0;
                        al.RespCertas = 1;
                    }
                }
                else
                {
                    if (ael.Resposta < 0)
                    {
                        al.RespErradas = alAnterior.RespErradas + 1;
                        al.RespCertas = alAnterior.RespCertas;
                    }
                    else
                    {
                        al.RespErradas = alAnterior.RespErradas;
                        al.RespCertas = alAnterior.RespCertas + 1;
                    }
                }
                db.AlunoLicoes.AddOrUpdate(al);
                System.Diagnostics.Debug.WriteLine("ALUNO-LICAO");
                Tipo tipo = db.Licoes.Find(licao,expl).TipoLicao;
                float estadoAnterior = 0;

                if (a.Aprendizagens.Count(aprend => aprend.Tipo == tipo.IdTipo) > 0)
                    estadoAnterior = (float) a.Aprendizagens.OrderByDescending(aprend => aprend.Data).First().Estado;

                System.Diagnostics.Debug.WriteLine("TIPO: " + tipo.IdTipo + " | AREA: " + tipo.Area);
                Aprendizagem ap = new Aprendizagem();
                ap.Aluno = aluno;
                ap.Data = System.DateTime.Now;
                ap.Estado = estadoAnterior + Formula((int)ael.Resposta, new ExercicioDAO().GetExercicio(ex).Dificuldade, GetNumRespostasCertas(aluno, tipo.IdTipo), GetNumRespostasErradas(aluno, tipo.IdTipo), GetNumLicoesTipo(tipo.IdTipo));
                ap.Tipo = tipo.IdTipo;

                db.Aprendizagens.Add(ap);
                System.Diagnostics.Debug.WriteLine("APRENDIZAGEM");
                db.SaveChanges();
                dbTransaction.Commit();
                //dbTransaction.Rollback();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION: " + e.Message + " || SOURCE: " + e.Source);
                dbTransaction.Rollback();
            }
        }

        private AlunoLicao GetAlunoLicaoMaisRecente(int aluno, int licao)
        {
            Aluno a = db.Alunos.Find(aluno);
            return a.LicoesVistas.Count == 0 ? null : a.LicoesVistas.OrderByDescending(l => l.Data).First(l => l.Licao == licao);
        }

        private int GetNumRespostasCertas(int idAluno, int tipo)
        {
            Aluno a = db.Alunos.Find(idAluno);

            List<Licao> listaLicoesTipo = db.Licoes.DistinctBy(l => l.idLicao).Where(l => l.Tipo == tipo).ToList();
            int certas = 0;

            foreach (var l in listaLicoesTipo)
            {
                List<AlunoLicao> licoesVistas = a.LicoesVistas.Where(lv => lv.Licao == l.idLicao && lv.RespCertas != null).ToList();
                certas += licoesVistas.Sum(lv => (int) lv.RespCertas);
            }

            return certas;

        }

        private int GetNumRespostasErradas(int idAluno, int tipo)
        {
            Aluno a = db.Alunos.Find(idAluno);

            List<Licao> listaLicoesTipo = db.Licoes.DistinctBy(l => l.idLicao).Where(l => l.Tipo == tipo).ToList();
            int erradas = 0;

            foreach (var l in listaLicoesTipo)
            {
                List<AlunoLicao> licoesVistas = a.LicoesVistas.Where(lv => lv.Licao == l.idLicao && lv.RespErradas != null).ToList();
                erradas += licoesVistas.Sum(lv => (int)lv.RespErradas);
            }

            return erradas;

        }
    }
}