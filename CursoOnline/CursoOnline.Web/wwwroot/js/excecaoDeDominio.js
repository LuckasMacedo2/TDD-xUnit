function formQuandoFalha(erro){

    if (erro.status == 500)
        toastr.error(erro.responseText);
    else if (erro.status = 502)
        erro.responseJSON.forEach(function (mensagemDeErro) {
            toastr.erro(mensagemDeErro);
        }
        );
}