using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using System.Text.RegularExpressions;

namespace CursoOnline.Dominio.Alunos
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string PublicoAlvo { get; set; }
        public string Email { get; set; }
    }
}
