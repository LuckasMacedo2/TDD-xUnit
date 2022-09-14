import { Selector } from "testcafe";
import Curso from './PageModel/curso'

const curso = new Curso();

// Nome do teste
fixture('Curso')
    .page(curso.url) // Local em que a página estará

test('Deve criar um novo curso', async t => {
    await t
        .typeText(curso.inputNome, `Curso TestCafé ${(new Date()).toString()}`)
        .typeText(curso.inputDescricao, 'DescricaoLegal')
        .typeText(curso.inputCargaHoraria, '10')
        .click(curso.selectPublicAlvo)
        .click(curso.opcaoEmpregado)
        .typeText(curso.inputValor, '1000');

    await t
        .click(curso.salvar);

    await t
        .expect(curso.tituloDaPagina.innerText).eql('Listagem de cursos - CursoOnline.Web')

    /*
    <input type="text" class="form-control" data-val="true" data-val-required="The Nome field is required." id="Nome" name="Nome" value="">
    <input type="text" class="form-control" data-val="true" data-val-number="The field CargaHoraria must be a number." data-val-required="The CargaHoraria field is required." id="CargaHoraria" name="CargaHoraria" value="0">
    <select class="form-control" data-val="true" data-val-required="The PublicoAlvo field is required." id="PublicoAlvo" name="PublicoAlvo">
                            <option value="Estudante">Estudante</option>
                            <option value="Universitário">Universitário</option>
                            <option value="Empregado">Empregado</option>
                            <option value="Empreendedor">Empreendedor</option>
                        </select>
    <input class="form-control" type="text" data-val="true" data-val-number="The field Valor must be a number." data-val-required="The Valor field is required." id="Valor" name="Valor" value="0">
    <button class="btn btn-success">Salvar</button>
    */

})