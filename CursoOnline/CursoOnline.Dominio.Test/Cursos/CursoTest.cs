using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;
using static CursoOnline.Dominio._Base.ValidadorDeRegra;

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
        private readonly Faker _faker;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        // Setup: O contrutor da classe de teste é executado a cada método de teste
        public CursoTest()
        {
            _faker = new Faker();
            _nome = _faker.Random.Word();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = _faker.Random.Double(100, 1000);
            _descricao = _faker.Lorem.Paragraph();
        }

        // Clean-up: Executado sempre que o método de teste é finalizado.
        // Necessário implementar a interface IDisposable
        public void Dispose()
        {
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
            .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveCursoTerUmaCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveCursoTerUmValorInvalido(double valorInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() => CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem(Resource.ValorInvalido);
        }

        //-- Editar curso
        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;

            var curso = CursoBuilder.Novo().Build();

            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }

        [Theory] // Permite passar diferentes parâmetos para o método de teste
        [InlineData("")]    // Passagem de argumentos para o método testar
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarNome(nomeInvalido))
            .ComMensagem(Resource.NomeInvalido);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            var cargaHorariaEsperada = _faker.Random.Double(50, 1000);

            var curso = CursoBuilder.Novo().Build();

            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveAlterarComCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida))
                .ComMensagem(Resource.CargaHorariaInvalida);
        }

        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperado = _faker.Random.Double(100, 1000);

            var curso = CursoBuilder.Novo().Build();

            curso.AlterarValor(valorEsperado);

            Assert.Equal(valorEsperado, curso.Valor);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveAlterarComUmValorInvalido(double valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarValor(valorInvalido))
                .ComMensagem(Resource.ValorInvalido);
        }

    }
}
