﻿using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using System.Text.RegularExpressions;

namespace CursoOnline.Dominio.Alunos
{
    public class Aluno : Entidade
    {
        private readonly Regex _emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex _cpfRegex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public string Email { get; set; }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();
            Nome = nome;
        }

        public Aluno(string nome, string cpf, PublicoAlvo publicoAlvo, string email)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(string.IsNullOrEmpty(cpf) || !_cpfRegex.Match(cpf).Success, Resource.CpfInvalido)
                .Quando(string.IsNullOrEmpty(email) || !_emailRegex.Match(email).Success, Resource.EmailInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
            Email = email;
        }
    }
}
