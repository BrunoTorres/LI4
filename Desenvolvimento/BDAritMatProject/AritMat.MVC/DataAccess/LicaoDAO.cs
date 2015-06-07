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

        public static readonly int EXER_MAIS_DIFICIL = 1;
        public static readonly int PROX_LICAO = 2;
        public static readonly int PROX_EXER = 3;
        public static readonly int PROX_EXPLICACAO = 4;
        public static readonly int LICAO_ANTERIOR = 5;
        public static readonly int EXER_MAX_DIF = 6;
        public static readonly int EXER_RANDOM = 7;
        public static readonly int APRENDEU_TODAS = 8;
        public static readonly int NAO_HA_ANTERIOR = 9;


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

            int totalExsLicao = new ExercicioDAO(db).GetNumExerciciosLicao(lastId);

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
            return db.Licoes.DistinctBy(l => l.idLicao).Where(l => l.Tipo == 1).ToList();
        }

        public List<Licao> GetLicoesSub()
        {
            return db.Licoes.DistinctBy(l => l.idLicao).Where(l => l.Tipo == 2).ToList();
        }

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

            try
            {
                AlunoExercicioLicao ael = new AlunoExercicioLicao
                {
                    Licao = licao,
                    Explicacao = expl,
                    Aluno = aluno,
                    Resposta = new RespostaDAO().GetPontuacao(r),
                    Exercicio = ex,
                    Data = System.DateTime.Now
                };


                db.AlunoExercicioLicoes.AddOrUpdate(ael);

                a.Pontuacao += (int) ael.Resposta;
                db.Alunos.AddOrUpdate(a);

                AlunoLicao alAnterior = null;
                if (a.LicoesVistas.Any(l => l.Licao == licao && l.Explicacao == expl))
                    alAnterior = a.LicoesVistas.First(l => l.Licao == licao && l.Explicacao == expl);

                AlunoLicao al = new AlunoLicao
                {
                    Aluno = aluno,
                    Explicacao = expl,
                    Licao = licao,
                    Data = System.DateTime.Now
                };
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

                Tipo tipo = db.Licoes.Find(licao,expl).TipoLicao;
                float estadoAnterior = 0;

                if (a.Aprendizagens.Count(aprend => aprend.Tipo == tipo.IdTipo) > 0)
                    estadoAnterior = (float) a.Aprendizagens.OrderByDescending(aprend => aprend.Data).First().Estado;

                Aprendizagem ap = new Aprendizagem
                {
                    Aluno = aluno,
                    Data = System.DateTime.Now,
                    Estado =
                        estadoAnterior +
                        Formula((int) ael.Resposta, new ExercicioDAO(db).GetExercicio(ex).Dificuldade,
                            GetNumRespostasCertas(aluno, tipo.IdTipo), GetNumRespostasErradas(aluno, tipo.IdTipo),
                            GetNumLicoesTipo(tipo.IdTipo)),
                    Tipo = tipo.IdTipo
                };

                db.Aprendizagens.Add(ap);

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

        public int OQueFazer(int aluno, int licao, int expl, int ex, bool result)
        {
            Exercicio e = db.Exercicios.Find(ex);
            Aluno a = db.Alunos.Find(aluno);
            int tipo = db.Licoes.Find(licao, expl).Tipo;

            int certas = (int) a.LicoesVistas.Where(l => l.Licao == licao && l.RespCertas != null).Sum(l => l.RespCertas);
            int totalResp = (int) a.LicoesVistas.Where(l => l.Licao == licao && l.RespCertas != null).Sum(l => l.RespCertas);
            totalResp += (int)a.LicoesVistas.Where(l => l.Licao == licao && l.RespErradas != null).Sum(l => l.RespErradas);

            double percentCertas = (double)certas / (double)totalResp;

            int totalExsLicao = new ExercicioDAO(db).GetNumExerciciosLicao(licao);
            List<Exercicio> exerMaxDif = new ExercicioDAO(db).GetExerciciosLicaoMaxDificuldade(licao);

            // se já acertou um exercicio de maxima dificuldade
            bool acertouEmMax = false;
            for (int i = 0; i < exerMaxDif.Count && !acertouEmMax; i++)
            {
                if (a.ExerciciosEmLicao.Any(el => el.Exercicio == exerMaxDif.ElementAt(i).IdExercicio && el.Resposta > 0))
                    acertouEmMax = true;
            }

            if (result)
            {
                bool existeProx = db.Licoes.Any(ll => ll.idLicao == licao + 1 && ll.Tipo == tipo);
                // recomendar prox lição ou exercicio de max dif
                if (percentCertas > 0.75 && a.ExerciciosEmLicao.Count(l => l.Licao == licao) > (int) (0.5*totalExsLicao))
                {
                    if (existeProx && acertouEmMax)
                        return PROX_LICAO;

                    if(!existeProx && acertouEmMax)
                        return APRENDEU_TODAS;

                    return EXER_MAX_DIF;
                }

                return e.Dificuldade == 5 ? EXER_RANDOM : EXER_MAIS_DIFICIL;
            }

            int erradas = totalResp - certas;
            System.Diagnostics.Debug.WriteLine("ERRADAS SWITCH: " + erradas);
            switch (erradas)
            {
                case 1:
                    if(new ExercicioDAO(db).ExisteOutroExercicioMesmaDifDisponivel(aluno, licao, ex) != null)
                        return PROX_EXER;
                    
                    return PROX_EXPLICACAO;
                case 2:
                case 3:
                    return PROX_EXPLICACAO;
                default:
                    return LICAO_ANTERIOR;
            }
        }
    }
}