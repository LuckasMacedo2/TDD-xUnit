using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Test.Matriculas
{
    public class CancelamentoMatricula
    {
        private readonly IMatriculaRepositorio _matriculaRepositorio;
        private IMatriculaRepositorio @object;

        public CancelamentoMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }

        public void Cancelar(int matriculaId)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorDeRegra.Novo()
                .Quando(matricula == null, Resource.MatriculaNaoEncontrada)
                .DispararExcecaoSeExistir();

            
            matricula.Cancelar();
        }
    }
}