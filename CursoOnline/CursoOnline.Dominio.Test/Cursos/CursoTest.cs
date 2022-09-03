using ExpectedObjects;
using Xunit;
using CursoOnline.Dominio.Test._Util;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            /*
                História: Criar e editar um curso para que sejam abertas matriculas para o mesmo
                Critério de aceite: 
                    - Criar um curso com nome, carga horária, público alvo e valor do curso
                    - As opção de público alvo são: Estudante, universitário, empregado e empreendedor
                    - Tdoso os campos do curso são obrigatórios
            */

            // Arrange
            //var nome = "Informártica básica";
            //var cargaHoraria = 80;
            //var publicoAlvo = "Estudantes";
            //var valor = 1500;

            //// Act
            //var curso = new Curso(nome, cargaHoraria, publicoAlvo, valor);

            //// Assert
            //Assert.Equal(nome, curso.Nome);
            //Assert.Equal(cargaHoraria, curso.CargaHoraria);
            //Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            //Assert.Equal(valor, curso.Valor);

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
                Nome = "Informártica básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)1500
            };

            // Act
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            // Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);

        }

        [Theory] // Permite passar diferentes parâmetos para o método de teste
        [InlineData("")]    // Passagem de argumentos para o método testar
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informártica básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)1500
            };

            Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
            .ComMensagem("Nome inválido");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                Nome = "Informártica básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)1500
            };

            Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
                .ComMensagem("Carga horária  inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1.5)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informártica básica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)1500
            };

            Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, valorInvalido))
                .ComMensagem("Valor inválido");
        }

        public class Curso
        {
            public string Nome { get; set; }
            public double CargaHoraria { get; set; }
            public PublicoAlvo PublicoAlvo { get; set; }
            public double Valor { get; set; }
            public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
            {
                if (string.IsNullOrEmpty(nome))
                    throw new ArgumentException("Nome inválido");
                if(cargaHoraria < 1)
                    throw new ArgumentException("Carga horária  inválida");
                if (valor < 1)
                    throw new ArgumentException("Valor inválido");

                Nome = nome;
                CargaHoraria = cargaHoraria;
                PublicoAlvo = publicoAlvo;
                Valor = valor;
            }
        }

        public enum PublicoAlvo
        {
            Estudante,
            Universitario, 
            Empregado,
            Empreendedor
        }

    }
}
