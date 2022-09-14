using Moq;
using System.Reflection;
using Xunit;

namespace PokerComTDD
{
    /*
     História: No jogo de Poker, uma mão consistem em cinco cartas que podem ser comparadas
                Jás mais baixa para a mais alta da seguinte maneira:
	                - Carta alta: A carta de maior valor;
	                - Um par: Duas cartas de mesmo valor;
	                - Flush: Todas as cartas do mesmo naipe.

                * As cartas devem ser válidas
                * As cartas são, em ordem crescente de vvalor: 2, 3, 4, 5, 6, 7, 8, 9, 10, Valete, Dama, Rei, As.
                * Os naipes são: Outro (D), Copa (H), Espada (S), Paus (C)

                ** Não é possível treinar métodos private
     
     */
    public class AnalisadorDeVencedorTest
    {
        private readonly Mock<IAnalisadorDeVencedorComMaiorCarta> _analisadorDeVencedorComMaiorCarta;
        private readonly Mock<IAnalisadorDeVencedorComParDeCartas> _analisadorDeVencedorComParDeCartas;
        private readonly AnalisadorDeVencedor _analisador;
        private readonly List<string> _cartasDoPrimeiroJogador;
        private readonly List<string> _cartasDoSegundoJogador;

        public AnalisadorDeVencedorTest()
        {
            _analisadorDeVencedorComMaiorCarta = new Mock<IAnalisadorDeVencedorComMaiorCarta>();
            _analisadorDeVencedorComParDeCartas = new Mock<IAnalisadorDeVencedorComParDeCartas>();
           _analisador = new AnalisadorDeVencedor(_analisadorDeVencedorComParDeCartas.Object, _analisadorDeVencedorComMaiorCarta.Object);

            _cartasDoPrimeiroJogador = "2O,4C,3P,6C,7C".Split(",").ToList();
            _cartasDoSegundoJogador = "3O,5C,2E,9C,7P".Split(",").ToList();
        }

        [Fact]
        public void DeveAnalisarVencedorQueTemMaiorCarta()
        {
            _analisadorDeVencedorComMaiorCarta.Setup(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador)).Returns("Segundo jogador");

            _analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador);

            _analisadorDeVencedorComMaiorCarta
                .Verify(analisador => analisador
                .Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador));
        }

        [Fact]
        public void DeveAnalisarVencedorQueTemParDeCartas()
        {
            _analisadorDeVencedorComParDeCartas.Setup(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador)).Returns("Segundo jogador");

            _analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador);

            _analisadorDeVencedorComParDeCartas
                .Verify(analisador => analisador
                .Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador));
        }

        [Fact]
        public void NaoDeveAnalisarVencedorComMaiorCartaQuandoJogadorTerCartasComPar()
        {
             _analisadorDeVencedorComParDeCartas.Setup(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador)).Returns("Segundo jogador");

            _analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador);

            _analisadorDeVencedorComMaiorCarta
                .Verify(analisador => analisador.Analisar(_cartasDoPrimeiroJogador, _cartasDoSegundoJogador), Times.Never);
        }


        // Inicial antes do método virar uma controller
        //[Theory]
        //[InlineData("2O,4C,3P,6C,7C", "3O,5C,2E,9C,7P", "Segundo jogador")]
        //[InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,7C", "Primeiro jogador")]
        //[InlineData("3O,5C,2E,9C,7P", "2O,4C,3P,6C,10E", "Segundo jogador")]
        //[InlineData("3O,5C,2E,9C,VP", "2O,4C,3P,6C,AE", "Segundo jogador")]
        //public void DeveAnalisarQuandoJogadorTiverMaiorCarta(string cartasDoPrimeiroJogadorStr, string cartasDoSegundoJogadorStr, string vencedorEsperado)
        //{
        //    var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorStr.Split(",").ToList();
        //    var cartasDoSegundoJogador = cartasDoSegundoJogadorStr.Split(",").ToList();            

        //    var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

        //    Assert.Equal(vencedorEsperado, vencedor);
        //}

        //[Theory]
        //[InlineData("2O,2C,3P,6C,7C", "3O,5C,2E,9C,7P", "Primeiro jogador")]
        //[InlineData("3O,5C,2E,9C,7P", "2O,2C,3P,6C,7C", "Segundo jogador")]
        //[InlineData("2O,2C,3P,6C,7C", "DO,DC,2E,9C,7P", "Segundo jogador")]
        //[InlineData("DO,DC,2E,9C,7P", "2O,2C,3P,6C,7C", "Primeiro jogador")]

        //public void DeveAnalisarVencedorQuandoTiverUmParDeCartasDoMesmoValor(string cartasDoPrimeiroJogadorStr, string cartasDoSegundoJogadorStr, string vencedorEsperado)
        //{
        //    var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorStr.Split(",").ToList();
        //    var cartasDoSegundoJogador = cartasDoSegundoJogadorStr.Split(",").ToList();

        //    var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);
        //    Assert.Equal(vencedorEsperado, vencedor);
        //}

        //[Theory]
        //[InlineData("3O,5C,RE,AC,RP", "RO,2C,3P,RC,7C", "Primeiro jogador")]
        //[InlineData("RO,2C,3P,RC,7C", "3O,5C,RE,AC,RP", "Segundo jogador")]
        //public void DeveAnalisarVencedorQuandoDoisJogadoresEstaoEmpatadosEmParSendoVencedorOQueTemAMaiorCarta(string cartasDoPrimeiroJogadorStr, string cartasDoSegundoJogadorStr, string vencedorEsperado)
        //{
        //    var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorStr.Split(",").ToList();
        //    var cartasDoSegundoJogador = cartasDoSegundoJogadorStr.Split(",").ToList();
        //    var vencedor = _analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

        //    Assert.Equal(vencedorEsperado, vencedor);
        //}
    }
}
