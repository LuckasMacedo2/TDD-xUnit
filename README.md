# TDD-xUnit

Projeto de estudos de Test Driven Development - TDD

Ideia do TDD: Criar o teste -> falha -> corrigir -> refatorar

## Tipos de teste:

* Teste de unidade: Testa uma determinada funcionalidade / módulo por vez. São baratos e simples;
* Teste e2e:	Simula comportamentos do usuário. Front, HTML, banco de dados ...
* Testes de integração: testa os módulos do sistema.

## Padrões / conceitos empregados:
* Mock: Apenas verifica, verifica algo;
* Stub: Dá comportamento ao mock;
* Ioc: Contém as referências para os projetos. Busca desacoplar o front da camada de infraestrutura
* UnitOfWork: Salvar os dados a partir de um middleware;
Desgin pattern: Builder
Separar a construção de objeto de forma que a sua contrução tenha diferentes representações
Uso: Adição de um novo campo em um objeto
Sempre começa com um método estático instanciando ele mesmo -> Novo()
Após criar os métodos especificando o que será utilizado -> ComNome(), ComDescricao ...
Ao final o objeto deve ser construído -> Build()

## Packages
* Moq
* Faker
* xUnit


## TesCafé

Ferramenta para testar simulando a interação com o usuário

Para instalar e executar:
```
npm install testecafe --save-dev
npm run testcafe
```
