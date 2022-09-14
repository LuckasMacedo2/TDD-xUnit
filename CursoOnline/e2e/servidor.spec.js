import { Selector } from "testcafe";
import Page from './PageModel/page'

const page = new Page();

// Nome do teste
fixture('servidor')
    .page(page.urlBase); // Local em que a página estará

test('Validando se servidor está online', async t => {
    await t.expect(Selector('title').innerText).eql('Listagem de cursos - CursoOnline.Web')
})