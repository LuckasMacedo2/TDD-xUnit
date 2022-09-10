using ExpectedObjects;
using Xunit;

namespace PokerComTDD
{
    public class CartaTest
    {
        [Theory]
        [InlineData("A", "O", 14)]
        [InlineData("10", "E", 10)]
        [InlineData("2", "P", 2)]
        public void DeveCriarUmaCarta(string valorDaCarta, string naipeDaCarta, int pesoDaCarta)
        {
            var cartaEsperada = new
            {
                Valor = valorDaCarta,
                Naipe = naipeDaCarta,
                Peso = pesoDaCarta
            };

            var carta = new Carta(cartaEsperada.Valor + cartaEsperada.Naipe);

            cartaEsperada.ToExpectedObject().ShouldMatch(carta);
        }

        [Theory]
        [InlineData("V", 11)]
        [InlineData("D", 12)]
        [InlineData("R", 13)]
        [InlineData("A", 14)]
        public void DeveCriarUmaCartaComPeso(string valorDaCarta, int pesoEsperado)
        {            
            var carta = new Carta(valorDaCarta + "E");

            Assert.Equal(pesoEsperado, carta.Peso);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("1")]
        [InlineData("15")]
        [InlineData("10000")]
        [InlineData("-1")]
        public void DeveValidarValorDaCarta(string valorDaCartaInvalida)
        {

            var mensagemDeErro = Assert.Throws<Exception>(() => new Carta(valorDaCartaInvalida + "O")).Message;
            Assert.Equal("Valor da carta inválida", mensagemDeErro);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("G")]
        [InlineData("10000")]
        [InlineData("A")]
        [InlineData("Z")]
        public void DeveValidarNaipeDaCarta(string naipeDaCartaInvalido)
        {
            // Válidos: Ouro (O), Copa (H), Espada (S) e Paus (C)
            var mensagemDeErro =  Assert.Throws<Exception>(() => new Carta("2" + naipeDaCartaInvalido)).Message;
            Assert.Equal("Naipe inválido", mensagemDeErro);
        }
    }
}
