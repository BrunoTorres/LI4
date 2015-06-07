using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.MVC.JSonAux
{
    public class RespostaAExercicio
    {
        public int OQueFazer { get; set; }
        public int NextLesson { get; set; }
        public int NextExpl { get; set; }
        public int NextExercicio { get; set; }
        public int Certas { get; set; }
        public int Erradas { get; set; }
    }
}