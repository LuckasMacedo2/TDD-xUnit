namespace PokerComTDD
{
    public interface IAnalisadorDeVencedorComMaiorCarta
    {
        public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador);
    }

    public class AnalisadorDeVencedorComMaiorCarta : IAnalisadorDeVencedorComMaiorCarta
    {
        public AnalisadorDeVencedorComMaiorCarta()
        {
        }

        public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
        {
            var cartaComMaiorPesoDoPrimeiroJogador = cartasDoPrimeiroJogador
                .Select(carta => new Carta(carta).Peso)
                .OrderBy(peso => peso)
                .Max();

            var cartaComMaiorPesoDoSegundoJogador = cartasDoSegundoJogador
                .Select(carta => new Carta(carta).Peso)
                .OrderBy(peso => peso)
                .Max();

            return cartaComMaiorPesoDoPrimeiroJogador > cartaComMaiorPesoDoSegundoJogador ? "Primeiro jogador" : "Segundo jogador";
        }
    }
}