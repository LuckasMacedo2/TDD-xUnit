import { Selector } from "testcafe";
export default class Page {
    constructor(){
        this.urlBase = 'localhost:3000';
        this.tituloDaPagina = Selector('title');
    }
}