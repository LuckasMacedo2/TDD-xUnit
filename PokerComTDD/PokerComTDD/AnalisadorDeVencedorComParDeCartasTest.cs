using Xunit;

namespace PokerComTDD
{
    public class AnalisadorDeVencedorComParDeCartasTest
    {
        private readonly AnalisadorDeVencedorComParDeCartas _analisador;

        public AnalisadorDeVencedorComParDeCartasTest()
        {
            _analisador = new AnalisadorDeVencedorComParDeCartas();
        }

        [Theory]
        [InlineData("2O,2C,3P,6C,7C", "3O,5C,2E,9C,7P", "Primeiro jogador")]
        [InlineData("3O,5C,2E,9C,7P", "2O,2C,3P,6C,7C", "Segundo jogador")]
        [InlineData("2O,2C,3P,6C,7C", "DO,DC,2E,9C,7P", "Segundo jogador")]
        [InlineData("DO,DC,2E,9C,7P", "2O,2C,3P,6C,7C", "Primeiro jogador")]

        public void DeveAnalisarVencedorQuandoTiverUmParDeCartasDoMesmoValor(string cartasDoPrimeiroJogadorStr, string cartasDoSegundoJogadorStr, string vencedorEsperado)
        {
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorStr.Split(",").ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorStr.Split(",").ToList();

            var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);
            Assert.Equal(vencedorEsperado, vencedor);
        }

        [Fact]
        public void NaoDeveAnalisarVencedorQuandoJogadoresNaoTemParDeCartas()
        {

            var cartasDoPrimeiroJogador = "2O,4C,3P,6C,7C".Split(",").ToList();
            var cartasDoSegundoJogador = "3O,5C,2E,9C,7P".Split(",").ToList();

            var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            Assert.Null(vencedor);
        }

    }
}
