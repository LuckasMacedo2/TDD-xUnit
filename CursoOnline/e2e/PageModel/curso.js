
import Page from "./page";
import { Selector } from "testcafe";

export default class Curso extends Page {
    constructor(){
        super();
        this.url = `${this.urlBase}/Curso/Novo`;
        this.inputNome = Selector('[name="Nome"]');
        this.inputDescricao = Selector('[name="Descricao"]');
        this.inputCargaHoraria = Selector('[name="CargaHoraria"]');
        this.selectPublicAlvo = Selector('[name="PublicoAlvo"]');
        this.opcaoEmpregado = Selector('option[value="Empregado"]');
        this.inputValor = Selector('[name="Valor"]');
        this.salvar = Selector('.btn-success');
    }
}