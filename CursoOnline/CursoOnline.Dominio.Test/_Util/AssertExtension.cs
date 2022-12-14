using CursoOnline.Dominio._Base;
using Xunit;
using static CursoOnline.Dominio._Base.ValidadorDeRegra;

namespace CursoOnline.Dominio.Test._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ExcecaoDeDominio exception, string mensagem)
        {
            if (exception.MensagensDeErro.Contains(mensagem))
                Assert.True(true);
            else
                Assert.False(false, $"Esperava a mensagem: '{mensagem}'");

        }
    }
}
