using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test.Alunos;
using System.Security.Cryptography;

namespace CursoOnline.Dominio.Test._Builders
{
    public class AlunoBuilder
    {
        private int _id;
        private string _nome = "Teste";
        private string _cpf = "052.058.129-66";
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private string _email = "email@gmail.com";

        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public AlunoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }


        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _cpf, _publicoAlvo, _email);

            if (_id > 0)
            {
                var propertyInfo = aluno.GetType().GetProperty("Id");
                propertyInfo.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return aluno;
        }


    }
}
