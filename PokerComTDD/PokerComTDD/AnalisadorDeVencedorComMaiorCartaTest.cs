using Xunit;

namespace PokerComTDD
{
    public class AnalisadorDeVencedorComMaiorCartaTest
    {
        [Theory]
        [InlineData("2O,4C,3P,6C,7C", "3O,5C,2E,9C,7P", "Segundo jogador")]
        [InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,7C", "Primeiro jogador")]
        [InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,10E", "Segundo jogador")]
        [InlineData("3O,5C,2E,9C,VP", "2O,4C,3P,6C,AE", "Segundo jogador")]
        public void DeveAnalisarQuandoJogadorTiverMaiorCarta(string cartasDoPrimeiroJogadorStr, string cartasDoSegundoJogadorStr, string vencedorEsperado)
        {
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorStr.Split(",").ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorStr.Split(",").ToList();

            var analisador = new AnalisadorDeVencedorComMaiorCarta();

            var vencedor = analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            Assert.Equal(vencedorEsperado, vencedor);
        }
    }
}
