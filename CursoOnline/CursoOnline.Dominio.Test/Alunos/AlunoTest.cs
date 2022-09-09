using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
    /*
    História: Cadastrar um aluno com nome, cpf, email e público albo para poder efetuar sua matrícula
           Poder editar apenas o nome do aluno em caso de erro
 */
    public class AlunoTest
    {
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _faker.Person.FullName,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Estudante,
                Email = _faker.Person.Email
            };

            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo, alunoEsperado.Email);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;

            var aluno = AlunoBuilder.Novo().Build();

            aluno.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, aluno.Nome);
        }

        [Theory]
        [InlineData("")]    
        [InlineData(null)]
        public void NaoDeveCriarAlunoComNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NomeInvalido);
        }

        [Theory]
        [InlineData("CPF invalido")]
        [InlineData("")]
        [InlineData("0122356456")]
        [InlineData(null)]
        public void NaoDeveCriarAlunoComCpfInvalido(string cpfInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder.Novo().ComCpf(cpfInvalido).Build())
                .ComMensagem(Resource.CpfInvalido);
        }

        [Theory]
        [InlineData("email invalid")]
        [InlineData("")]
        [InlineData("email.invalido")]
        [InlineData(null)]
        public void NaoDeveCriarAlunoComEmailInvalido(string emailInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() => AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
                .ComMensagem(Resource.EmailInvalido);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarNomeAlunoInvalido(string nomeInvalido)
        {
            var aluno = AlunoBuilder.Novo().Build();
            Assert.Throws<ExcecaoDeDominio>(() => aluno.AlterarNome(nomeInvalido))
                .ComMensagem(Resource.NomeInvalido);
        }
    }
}
