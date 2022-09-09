using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;
        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000, 2000)
            };


            // Cria a instância do objeto mockado
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();

            // Injetando a dependencia
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object, _conversorDePublicoAlvo.Object);
        }
        [Fact]
        public void DeveAdicionarCurso()
        {


            _armazenadorDeCurso.Armazenar(_cursoDto);

            // Equivalente ao assert, verifica se o comportamento foi assionado
            // Qualquer objeto do tipo curso: It.IsAny<Curso>()
            _cursoRepositorioMock.Verify(r => r.Adicionar(It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome &&
                         c.Descricao == _cursoDto.Descricao
                ))//, Times.AtLeast(2) // Quantidade de vezes que o método deve ser chamado para que o teste seja válido
            );
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComId(432).ComNome(_cursoDto.Nome).Build();

            // Setup do mock
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto)).ComMensagem(Resource.NomeDoCursoJaExiste);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never); // Nunca deve chamar o método adicionar quando for salvar

            
        }
    }
}
