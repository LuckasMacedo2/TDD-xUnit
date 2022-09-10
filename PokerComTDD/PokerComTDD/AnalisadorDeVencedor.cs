namespace PokerComTDD
{
    public class AnalisadorDeVencedor
    {
        private IAnalisadorDeVencedorComParDeCartas _analisadorDeVencedorComParDeCartas;
        private IAnalisadorDeVencedorComMaiorCarta _analisadorDeVencedorComMaiorCarta;

        public AnalisadorDeVencedor()
        {
        }

        public AnalisadorDeVencedor(IAnalisadorDeVencedorComParDeCartas analisadorDeVencedorComParDeCartas, IAnalisadorDeVencedorComMaiorCarta analisadorDeVencedorComMaiorCarta)
        {
            _analisadorDeVencedorComParDeCartas = analisadorDeVencedorComParDeCartas;
            _analisadorDeVencedorComMaiorCarta = analisadorDeVencedorComMaiorCarta;
        }

        public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
        {
            var vencedor = _analisadorDeVencedorComParDeCartas.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            if(vencedor == null)
                vencedor = _analisadorDeVencedorComMaiorCarta.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            return vencedor;
        }

       
    }
}