using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Dominio.Test._Builders
{
    /*
        Desgin pattern: Builder
        Separar a construção de objeto de forma que a sua contrução tenha diferentes representações
        Uso: Adição de um novo campo em um objeto

        Sempre começa com um método estático instanciando ele mesmo -> Novo()
        Após criar os métodos especificando o que será utilizado -> ComNome(), ComDescricao ...
        Ao final o objeto deve ser construído -> Build()
    */
    public class MatriculaBuilder
    {
        protected Aluno Aluno;
        protected Curso Curso;
        protected double ValorPago;
        protected bool Cancelada;
        protected bool Concluido;

        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build();
            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(PublicoAlvo.Empregado).Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };
        }

       public MatriculaBuilder ComAluno(Aluno aluno)
       {
            Aluno = aluno;
            return this;
       }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            Curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(double valorPago)
        {
            ValorPago = valorPago;
            return this;
        }

        public MatriculaBuilder ComCancelada(bool cancelada)
        {
            Cancelada = cancelada;
            return this;
        }

        public MatriculaBuilder ComConcluido(bool concluido)
        {
            Concluido = concluido;
            return this;
        }

        public Matricula Build()
        {
            var matricula = new Matricula(Aluno, Curso, ValorPago);

            if (Cancelada)
                matricula.Cancelar();

            if(Concluido)
            {
                const double notaDoAluno = 7;
                matricula.InformarNota(notaDoAluno);
            }

            return matricula;
        }
    }
}