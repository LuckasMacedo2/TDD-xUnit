using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorioMock;
        private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;
        private readonly Faker _faker;
        public ArmazenadorDeAlunoTest()
        {
            _faker = new Faker();
            _alunoDto = new AlunoDto
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Empregado.ToString()
            };

            // Cria a instância do objeto mockado
            _alunoRepositorioMock = new Mock<IAlunoRepositorio>();
            _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            // Injetando a dependencia
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorioMock.Object, _conversorDePublicoAlvo.Object);
        }

        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Armazenar(_alunoDto);
            _alunoRepositorioMock.Verify(r => r.Adicionar(It.Is<Aluno>(a => a.Nome == _alunoDto.Nome)));
        }

        [Fact]
        public void NaoDeveAdiocionarAlunoQuandoCpfJaCadastrado()
        {
            var alunoComMesmoCpf = AlunoBuilder.Novo().ComId(34).Build();
            _alunoRepositorioMock.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(alunoComMesmoCpf);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.CpfJaCadastrado);
        }

        [Fact]
        public void DeveEditarNomeDoAluno()
        {
            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equal(_alunoDto.Nome, alunoJaSalvo.Nome);
        }

        [Fact]
        public void NaoDeveAdicionarQuadoForEdicao()
        {
            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }

        [Fact]
        public void NaoDeveEditarDemaisInformacoesDoAluno()
        {
            var alunoJaSalvo = AlunoBuilder.Novo().Build();

            var cpfEsperado = alunoJaSalvo.Cpf;

            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;
            _alunoDto.Cpf = _faker.Person.Cpf();

            _alunoRepositorioMock.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equal(alunoJaSalvo.Cpf, cpfEsperado);
        }

    }
}
