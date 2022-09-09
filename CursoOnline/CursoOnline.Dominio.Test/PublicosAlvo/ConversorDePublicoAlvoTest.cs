using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test._Util;
using Xunit;

namespace CursoOnline.Dominio.Test.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private readonly ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();

        [Theory]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvo.Estudante, "Estudante")]
        [InlineData(PublicoAlvo.Universitario, "Universitario")]
        public void DeveConverterPublicoAlvo(PublicoAlvo publicoAlvoEsperado, string publicoAlvoString)
        {
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoString);

            Assert.Equal(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoEhInvalido()
        {
            const string publicoAlvoInvalido = "Invalido";

            Assert.Throws<ExcecaoDeDominio>(() => _conversor.Converter(publicoAlvoInvalido)).ComMensagem(Resource.PublicoAlvoInvalido);
        }
    }

    //public class ConversorDePublicoAlvo
    //{
    //    public ConversorDePublicoAlvo()
    //    {
    //    }

    //    public PublicoAlvo Converter(string publicAlvo)
    //    {
    //        ValidadorDeRegra.Novo().Quando(!Enum.TryParse<PublicoAlvo>(publicAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
    //            .DispararExcecaoSeExistir();

    //        return publicoAlvoConvertido;
    //    }
    //}
}
