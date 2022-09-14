using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
    /*
     História: Criar a matrícula para ter um aluno matriculado
     Um aluno deve ser do mesmo tipo do publico alvo do curso
     Saber se na matrícula houve desconto

     História: Informar a nota do aluno para concluir o curso
     * Matrícula ter uma nota
     * Indicar que curso foi concluído
     
     História: Cancelar a matrícula do aluno para o mesmo não faça mais parte do curso
     * Matrícula deve ser cancelada
     * Não pode quando matrícula estiver concluída
     
     */

    public class MatriculaTest
    {

        [Fact]
        public void DeveCriarMatricula()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build();
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };


            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);

            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            Aluno alunoInvalido = null;

            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo()
            .ComAluno(alunoInvalido).Build())
                .ComMensagem(Resource.AlunoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            Curso cursoInvalido = null;

            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo()
            .ComCurso(cursoInvalido).Build())
                .ComMensagem(Resource.CursoInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoInvalido()
        {
            const double valorInvalido = 0;

            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo()
            .ComValorPago(valorInvalido).Build())
                .ComMensagem(Resource.ValorInvalido);
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            var valorPagoMaiorQueValorCurso = curso.Valor + 1;

            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo().ComCurso(curso)
            .ComValorPago(valorPagoMaiorQueValorCurso).Build())
                .ComMensagem(Resource.ValorPagoMaiorQueValorCurso);
        }

        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).ComValor(100).Build();
            var valorPagoComDesconto = curso.Valor - 1;

            var matricula = MatriculaBuilder.Novo().ComCurso(curso)
            .ComValorPago(valorPagoComDesconto).Build();

            Assert.True(matricula.TemDesconto);
        }

        [Fact]
        public void NaoDevePublicoAlvoDeAlunoECursoSeremDiferentes()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Universitario).Build();

            Assert.Throws<ExcecaoDeDominio>(() => MatriculaBuilder.Novo().ComCurso(curso).ComAluno(aluno).Build())
            .ComMensagem(Resource.PublicoAlvoDiferente);
        }

        [Fact]
        public void DeveInformarANotaDoAlunoParaMatricula()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaDoAlunoEsperada);

            Assert.Equal(notaDoAlunoEsperada, matricula.NotaDoAluno);
        }

        [Fact]
        public void DeveIndicarQueCursoFoiConcluido()
        {
            const double notaDoAlunoEsperada = 9.5;
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.InformarNota(notaDoAlunoEsperada);

            Assert.True(matricula.CursoConcluido);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void NaoDeveInformarComNotaInvalida(double notaDoAlunoInvalida)
        {
            var matricula = MatriculaBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => matricula.InformarNota(notaDoAlunoInvalida))
            .ComMensagem(Resource.NotaDoAlunoInvalida);

        }

        [Fact]
        public void DeveCancelarMatricula()
        {
            var matricula = MatriculaBuilder.Novo().Build();

            matricula.Cancelar();

            Assert.True(matricula.Cancelada);
        }

        [Fact]
        public void NaoDeveInformarNotaQuandoMatriculaEstiverCancelada()
        {
            const double notaDoAluno = 3;
            var matricula = MatriculaBuilder.Novo().ComCancelada(true).Build();

            Assert.Throws<ExcecaoDeDominio>(() => matricula.InformarNota(notaDoAluno))
            .ComMensagem(Resource.MatriculaCancelada);
        }

        [Fact]
        public void NaoDeveCancelarQuandoMatriculaEstiverConcluida()
        {
            const double notaDoAluno = 3;
            var matricula = MatriculaBuilder.Novo().ComConcluido(true).Build();

            Assert.Throws<ExcecaoDeDominio>(() => matricula.Cancelar())
            .ComMensagem(Resource.MatriculaConcluida);
        }
    }
}
