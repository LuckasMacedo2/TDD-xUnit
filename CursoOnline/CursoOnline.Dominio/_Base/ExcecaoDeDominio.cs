
using System;

namespace CursoOnline.Dominio._Base
{
    public class ExcecaoDeDominio : ArgumentException
    {
        public List<string> MensagensDeErro { get; private set; }

        public ExcecaoDeDominio(List<string> mensagensDeErro)
        {
            MensagensDeErro = mensagensDeErro;
        }

    }
}