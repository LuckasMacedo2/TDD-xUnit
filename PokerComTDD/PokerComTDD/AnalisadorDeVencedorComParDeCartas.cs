namespace PokerComTDD
{
    public interface IAnalisadorDeVencedorComParDeCartas
    {
        public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador);
    }

    public class AnalisadorDeVencedorComParDeCartas : IAnalisadorDeVencedorComParDeCartas
    {
        public AnalisadorDeVencedorComParDeCartas()
        {

        }

        public string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
        {
            var parDeCartasDoPrimeiroJogador = cartasDoPrimeiroJogador
               .Select(carta => new Carta(carta).Peso)
               .GroupBy(peso => peso)
               .Where(grupo => grupo.Count() > 1);

            var parDeCartasDoSegundoJogador = cartasDoSegundoJogador
                .Select(carta => new Carta(carta).Peso)
                .GroupBy(peso => peso)
                .Where(grupo => grupo.Count() > 1);

            if (parDeCartasDoPrimeiroJogador != null && parDeCartasDoPrimeiroJogador.Any()
                && parDeCartasDoSegundoJogador != null && parDeCartasDoSegundoJogador.Any())
            {
                var maiorParDeCartasDoPrimeiroJogador = parDeCartasDoPrimeiroJogador
                    .Select(valor => valor.Key).OrderBy(valor => valor).Max();

                var maiorParDeCartasDoSegundoJogador = parDeCartasDoSegundoJogador
                    .Select(valor => valor.Key).OrderBy(valor => valor).Max();

                if (maiorParDeCartasDoPrimeiroJogador > maiorParDeCartasDoSegundoJogador)
                    return "Primeiro jogador";
                else if (maiorParDeCartasDoSegundoJogador > maiorParDeCartasDoPrimeiroJogador)
                    return "Segundo jogador";
            }
            else if (parDeCartasDoPrimeiroJogador != null && parDeCartasDoPrimeiroJogador.Any())
            {
                return "Primeiro jogador";
            }
            else if (parDeCartasDoSegundoJogador != null && parDeCartasDoSegundoJogador.Any())
            {
                return "Segundo jogador";
            }

            return null;
        }
    }
}