using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AritMat.Exceptions
{


    public class LastExplicacaoException : Exception
    {
        public LastExplicacaoException()
        {
        }

        public LastExplicacaoException(string msg)
            : base(msg)
        {
        }
    }
}