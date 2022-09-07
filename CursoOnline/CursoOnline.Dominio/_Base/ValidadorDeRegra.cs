namespace CursoOnline.Dominio._Base
{
    public  class ValidadorDeRegra
    {
        private readonly List<string> _mensagensDeErro;

        private ValidadorDeRegra()
        {
            _mensagensDeErro = new List<string>();
        }
        public static ValidadorDeRegra Novo()
        {
            return new ValidadorDeRegra();
        }
        public ValidadorDeRegra Quando(bool temErro, string mensagemDeErro)
        {
            if (temErro)
                _mensagensDeErro.Add(mensagemDeErro);

            return this;
        }
        public void DispararExcecaoSeExistir()
        {
            if (_mensagensDeErro.Any())
                throw new ExcecaoDeDominio(_mensagensDeErro);
        }
    }
}
