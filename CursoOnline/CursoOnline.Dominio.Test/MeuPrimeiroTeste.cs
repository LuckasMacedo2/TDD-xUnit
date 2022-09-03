using Xunit;

namespace CursoOnline.Dominio.Test
{
    // Tem que ser public a classe
    public class MeuPrimeiroTeste
    {
        [Fact(DisplayName = "DeveVariavel1SerIgualVariavel2")] // Para que o método possa ser executado
        public void DeveVariavel1SerIgualVariavel2()
        {
            /*
             Triple A
                A - Arrange - Organização: organização do código
                A - Action - Ação: 
                A - Assert - Asserção: Teste do código, se o código está correto
             */

            // Organização
            var var1 = 1;
            var var2 = 1;

            // Ação
            var1 = var2;

            // Asserção
            // Define se o teste está certo ou não
            Assert.Equal(var1, var2);
        }
    }
}
