using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class Imagem : ElementoEstudo
    {
        private Image imagem;

        public Imagem(Image img)
        {
            imagem = img;
        }
    }
}