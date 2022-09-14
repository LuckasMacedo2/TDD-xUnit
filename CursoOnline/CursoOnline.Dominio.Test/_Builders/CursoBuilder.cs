using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using System.Reflection;
using static CursoOnline.Dominio.Test.Cursos.CursoTest;

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
    public class CursoBuilder
    {
        private string _nome = "Informártica básica";
        private double _cargaHoraria = 80.00;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Empregado;
        private double _valor = 950.00;
        private string _descricao = "Curso legal";
        private int _id;

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

       public CursoBuilder ComNome(string nome)
       {
            _nome = nome;
            return this;
       }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Curso Build()
        {
            var curso = new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);

            if(_id > 0)
            {
                var propertyInfo = curso.GetType().GetProperty("Id");
                propertyInfo.SetValue(curso, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }
            

            return curso;
        }

        
    }
}