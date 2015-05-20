﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.BOL
{
    public class ChaveLicao
    {
        private readonly int IdLicao;
        private readonly int IdExpl;

        public ChaveLicao(int idl, int ide)
        {
            IdLicao = idl;
            IdExpl = ide;

        }

        public int getIdLicao() { return IdLicao; }
        public int getIdExpl() { return IdExpl; }
    }
}