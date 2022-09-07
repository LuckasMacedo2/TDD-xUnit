using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
    /*
        História: Criar e editar um curso para que sejam abertas matriculas para o mesmo
        Critério de aceite: 
            - Criar um curso com nome, carga horária, público alvo e valor do curso
            - As opção de público alvo são: Estudante, universitário, empregado e empreendedor
            - Todos os campos do curso são obrigatórios
            Novo critério de aceite:
            - Curso deve ter uma descrição   

    */
    public class CursoTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        // Setup: O contrutor da classe de teste é executado a cada método de teste
        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado");

            var faker = new Faker();
            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100, 1000);
            _descricao = faker.Lorem.Paragraph();
        }

        // Clean-up: Executado sempre que o método de teste é finalizado.
        // Necessário implementar a interface IDisposable
        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            // Arrange
            var nome = _nome;
            var cargaHoraria = _cargaHoraria;
            var publicoAlvo = _publicoAlvo;
            var valor = _valor;
            var descricao = "Curso legal";

            // Act
            var curso = new Curso(nome, descricao, cargaHoraria, publicoAlvo, valor);

            // Assert
            Assert.Equal(nome, curso.Nome);
            Assert.Equal(cargaHoraria, curso.CargaHoraria);
            Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            Assert.Equal(valor, curso.Valor);

        }

        [Fact]
        public void DeveCriarCursoComObjetosEsperados()
        {
            /*
                História: Criar e editar um curso para que sejam abertas matriculas para o mesmo
                Critério de aceite: 
                    - Criar um curso com nome, carga horária, público alvo e valor do curso
                    - As opção de público alvo são: Estudante, universitário, empregado e empreendedor
                    - Tdoso os campos do curso são obrigatórios
            */

            // Arrange
            // Objeto anonimo
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            // Act
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            // Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);

        }

        [Theory] // Permite passar diferentes parâmetos para o método de teste
        [InlineData("")]    // Passagem de argumentos para o método testar
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo().ComNome(nomeInvalido).Build())
            .ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem("Carga horária  inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem("Valor inválido");
        }

    }
}
