var _urlRaiz = "/";
var _esperePorControle = false;
var _esperePorContador = 0;
var _zIndexMensagem = 999;
var _idMensagem = 0;
var _tamanhoMaxImagem = 2000000;//2MB
var _mensagemLimite = "Máximo de 2MB de arquivo excedido.";

var AJAX_HELPER = {

    //Requisição ajax com tratamento de erro para APIs
    ajaxRequestApi: function (config) {

        if (config.waitingForShow === undefined) {
            config.waitingForShow = true;
        }

        if (config.waitingForShow) {
            AJAX_HELPER.waitingForShow();
        }

        if (config.dataObj === undefined || config.dataObj === null) {
            config.dataObj = {
                __RequestVerificationToken: $("#" + config.idForm + " [name=__RequestVerificationToken]").val()
            };
        }
        else {
            if (config.dataObj.__RequestVerificationToken === undefined || config.dataObj.__RequestVerificationToken === null) {
                config.dataObj.__RequestVerificationToken = $("#" + config.idForm + " [name=__RequestVerificationToken]").val();
            }
        }

        try {
            $.ajax({
                dataType: config.dataType || 'json', //json Default
                data: config.dataObj,
                url: config.url, //Obrigatorio
                type: config.type || 'POST', //POST Default
                async: config.async === undefined || config.async, // true Default
                success: function (result) {
                    try {
                        if (result.tipoMensagem !== null) {
                            if (result.tipoMensagem.length !== 0) {
                                AJAX_HELPER.exibirMensagem(result.tipoMensagem, result.mensagem);
                            }
                        }

                        if (result.sucesso) {
                            if (config.success !== undefined) {
                                config.success(result);
                            }
                        }
                        else {
                            if (config.error !== undefined) {
                                config.error(result);
                            }
                        }
                        return result.sucesso;
                    }
                    catch (err) {
                        AJAX_HELPER.exibirMensagem('danger', 'Ocorreu um erro interno no sistema. Por favor, tente novamente mais tarde.');
                        console.log(err);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status === 400) {
                        if (xhr.responseJSON !== null && xhr.responseJSON !== undefined) {
                            AJAX_HELPER.DisplayModelStateErrors(xhr.responseJSON, config);
                            try {
                                for (let propName in xhr.responseJSON) {
                                    $("html, body").stop().animate({
                                        scrollTop: ($("#" + config.idForm + " [id=" + propName.capitalize() + "]").offset().top - 100)
                                    }, 1000, 'easeInOutExpo');
                                    break;
                                }
                            } catch (err) {
                                console.log(err);
                            }
                        }
                    }
                    else if (xhr.status === 401) {
                        AJAX_HELPER.exibirMensagem('warning', 'Você não tem permissão para executar a ação.');
                    }
                    else {
                        if (xhr.responseText !== null && xhr.responseText !== undefined && xhr.responseText !== '') {
                            try {
                                let responseTextObject = JSON.parse(xhr.responseText);
                                AJAX_HELPER.exibirMensagem(responseTextObject.tipoMensagem, responseTextObject.mensagem);
                                console.log(responseTextObject.exception);
                            } catch (err) {
                                console.log(err);
                            }
                        }
                        else {
                            console.log("xhr:");
                            console.log(xhr);
                            console.log("ajaxOptions:");
                            console.log(ajaxOptions);
                            console.log("thrownError:");
                            console.log(thrownError);
                            AJAX_HELPER.exibirMensagem('danger', 'Ocorreu um erro interno no sistema. Por favor, tente novamente mais tarde.');
                        }
                        if (config.error !== undefined) {
                            config.error();
                        }
                    }
                    return false;
                },
                complete: function () {
                    if (config.waitingForShow) {
                        AJAX_HELPER.waitingForHide();
                    }
                }
            }).done(function (result) {
                //Somente entra se for sucesso.
                if (config.done !== undefined) {
                    config.done();
                }
            });
        }
        catch (err) {
            console.log(err);
            if (config.waitingForShow) {
                AJAX_HELPER.waitingForHide();
            }
        }
    },

    DisplayModelStateErrors: function (modelState, config) {
        AJAX_HELPER.exibirMensagem('info', 'Campos obrigatórios não preenchidos.');
        if (modelState !== null && modelState !== undefined) {
            let propStrings = Object.keys(modelState);
            $.each(propStrings, function (i, propString) {
                let propErrors = modelState[propString];
                $.each(propErrors, function (j, propError) {
                    propString = propString.substring(propString.lastIndexOf(".") + 1, propString.length);
                    $("#" + config.idForm + " [data-valmsg-for=" + propString.capitalize() + "]").empty().text(propError);
                });
            });
        }
    },

    waitingForShow: function () {
        _esperePorContador++;
        if (_esperePorControle === false) {
            _esperePorControle = true;
            waitingDialog.show("Carregando...");
        }
    },

    waitingForHide: function () {
        //setTimeout(function () {
        _esperePorContador--;
        if (_esperePorControle === true && _esperePorContador === 0) {
            _esperePorControle = false;
            waitingDialog.hide();
        }
        //}, 500); // milliseconds
    },

    exibirMensagem: function (tipo, mensagem, tempo, functionDismissClick) {
        let caixaAlto = 'Informação!';

        // se houver outro alert desse sendo exibido, cancela essa requisição
        //if ($("#message").is(":visible")) {
        //    return false;
        //}

        // se não setar o tempo, o padrão é 5 segundos
        if (!tempo) {
            tempo = 5000;
        }
        // se não setar o tipo, o padrão é alert-info
        if (tipo === "" || tipo === null) {
            tipo = "info";
        }
        if (tipo === 'success') {
            caixaAlto = 'Sucesso!';
        }
        else if (tipo === 'danger') {
            caixaAlto = 'Erro!';
        }
        else if (tipo === 'warning') {
            caixaAlto = 'Atenção!';
        }

        let idAlerta = 'message' + tipo;

        // monta o css da mensagem para que fique flutuando na frente de todos elementos da página
        const cssMessage = "/* display: inline; top: 0; left: 10%; right: 10%; width: 80%; padding-top: 10px; z-index: " + _zIndexMensagem;
        // monta o html da mensagem com Bootstrap
        let dialogo = "";
        dialogo += '<div id="message' + _idMensagem + '" style="' + cssMessage + '">';
        dialogo += '    <div class="alert alert-' + tipo + ' alert-dismissable fade show" role="alert" id="' + idAlerta + '">';
        //dialogo += '    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>';
        //dialogo += mensagem;
        dialogo += '<button type="button" class="close" data-dismiss="alert" aria-label="Close">\
                    <span aria-hidden="true">&times;</span>\
                </button>';
        dialogo += '<strong>' + caixaAlto + '</strong><br />' + mensagem;
        dialogo += '</div>';
        dialogo += '</div>';

        // adiciona ao body a mensagem com o efeito de fade
        $("#mensagemDiv").append(dialogo);
        $("#message" + _idMensagem).hide();
        $("#message" + _idMensagem).fadeIn(300);

        _zIndexMensagem++;

        // contador de tempo para a mensagem sumir
        setTimeout(function (id) {
            $('#message' + id).fadeOut(300, function () {
                $(this).remove();
                _zIndexMensagem--;
            });
        }, tempo, _idMensagem); // milliseconds

        $('#' + idAlerta).on('closed.bs.alert', function () {
            if (functionDismissClick !== undefined) {
                functionDismissClick();
            }
        });
        _idMensagem++;
    }

};

var URL_HELPER = {
    criarUrlApi: function (nomeController, nomeAcao, api) {
        if (api)
            return _urlRaiz + 'api/' + nomeController + '/' + nomeAcao;
        else
            return _urlRaiz + nomeController + '/' + nomeAcao;
    }
};

var MODEL_HELPER = {

    popularTodosCampos: function (objectModel, idForm) {

        if (idForm !== undefined) {
            idForm = "#" + idForm;
        }

        for (let propName in objectModel) {
            if ($(idForm + " [id=" + propName.capitalize() + "]").attr('type') === 'checkbox') {
                $(idForm + " [id=" + propName.capitalize() + "]").prop('checked', objectModel[propName]);
            }
            if ($(idForm + " [id=" + propName.capitalize() + "]").attr('type') === 'date') {
                if (objectModel[propName] !== null && objectModel[propName].length >= 10) {
                    $(idForm + " [id=" + propName.capitalize() + "]").val(objectModel[propName].substring(0, 10));
                }
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").is('select')) {
                $(idForm + " [id=" + propName.capitalize() + "]").val(objectModel[propName]).change();
                if ($(idForm + " [id=" + propName.capitalize() + "]").val() === null) {
                    $(idForm + " [id=" + propName.capitalize() + "]").val(0).change();
                }
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").is('img')) {
                $(idForm + " [id=" + propName.capitalize() + "]").prop('src', objectModel[propName]);
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").attr('alt') === 'decimal') {
                $(idForm + " [id=" + propName.capitalize() + "]").val(objectModel[propName]);
                if ($(idForm + " [id=" + propName.capitalize() + "]").val().length !== 0) {
                    if ($(idForm + " [id=" + propName.capitalize() + "]").val().indexOf('.') === -1) {
                        $(idForm + " [id=" + propName.capitalize() + "]").val($(idForm + " [id=" + propName.capitalize() + "]").val() + ',00');
                    }
                    $(idForm + " [id=" + propName.capitalize() + "]").val($(idForm + " [id=" + propName.capitalize() + "]").val().replace('.', ','));
                }
            }
            else {
                $(idForm + " [id=" + propName.capitalize() + "]").val(objectModel[propName]);
            }

            if (propName.capitalize() === "FotoBase64") {
                if (objectModel[propName] !== null && objectModel[propName] !== '') {
                    $(idForm + " [id=" + idForm.replace('#', '') + "FotoBase64Img]").attr('src', objectModel[propName]);
                    $(idForm + " [id=" + idForm.replace('#', '') + "FotoBase64Label]").text('imagem.jpg');
                }
            }
        }
    },

    recuperarDataSalvar: function (idForm) {

        if (idForm !== undefined) {
            idForm = "#" + idForm;
        }

        let objectModel = new Array();
        jQuery.map($(idForm).serializeArray(), function (n, i) {
            objectModel[n['name']] = n['value'];
        });

        let obj = new Object();

        for (let propName in objectModel) {
            if ($(idForm + " [id=" + propName.capitalize() + "]").is(':checkbox')) {
                obj[propName] = $(idForm + " [id=" + propName.capitalize() + "]").is(':checked');
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").is('select')) {
                if ($(idForm + " [id=" + propName.capitalize() + "]").val() === "0" || $(idForm + " [id=" + propName.capitalize() + "]").val() === 0) {
                    obj[propName] = null;
                }
                else {
                    obj[propName] = $(idForm + " [id=" + propName.capitalize() + "]").val();
                }
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").is('img')) {
                obj[propName] = $(idForm + " [id=" + propName.capitalize() + "]").attr('src');
            }
            else {
                obj[propName] = $(idForm + " [id=" + propName.capitalize() + "]").val();
            }
        }

        obj.__RequestVerificationToken = $(idForm + " [name=__RequestVerificationToken]").val();

        return obj;
    },

    limparMensagemCampos: function (idForm) {

        if (idForm !== undefined) {
            idForm = "#" + idForm;
        }

        let objectModel = new Array();
        jQuery.map($(idForm).serializeArray(), function (n, i) {
            if (n['name'] !== '__RequestVerificationToken') {
                objectModel[n['name']] = n['value'];
            }
        });

        for (var propName in objectModel) {
            $(idForm + " [data-valmsg-for=" + propName.capitalize() + "]").empty();
        }
    },

    limparTodosCampos: function (idForm) {
        let idFormOriginal = '';

        if (idForm !== undefined) {
            idFormOriginal = idForm;
            idForm = "#" + idForm;
        }

        let objectModel = new Array();
        jQuery.map($(idForm).serializeArray(), function (n, i) {
            if (n['name'] !== '__RequestVerificationToken') {
                objectModel[n['name']] = n['value'];
            }
        });

        for (let propName in objectModel) {
            if ($(idForm + " [id=" + propName.capitalize() + "]").attr('type') === 'checkbox') {
                $(idForm + " [id=" + propName.capitalize() + "]").prop('checked', false);
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").attr('type') === 'number') {
                $(idForm + " [id=" + propName.capitalize() + "]").val(0);
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").attr('alt') === 'decimal') {
                $(idForm + " [id=" + propName.capitalize() + "]").val("0,00");
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").is('select')) {
                //$(idForm + " [id=" + propName.capitalize() + "]").val(0).change();
                //$(idForm + " [id=" + propName.capitalize() + "] option:first").attr('selected', 'selected');
                $(idForm + " [id=" + propName.capitalize() + "]").val($(idForm + " [id=" + propName.capitalize() + "] option:first").val()).change();
            }
            else if ($(idForm + " [id=" + propName.capitalize() + "]").is('img')) {
                $(idForm + " [id=" + propName.capitalize() + "]").attr('src', './images/no-image.jpg');
            }
            else {
                $(idForm + " [id=" + propName.capitalize() + "]").val('');
                if ($(idForm + " [id=" + idFormOriginal + propName.capitalize() + "Temp]").length) {
                    $(idForm + " [id=" + idFormOriginal + propName.capitalize() + "Temp]").val('');
                }
            }
        }

        try {
            $(idForm + " [id=" + idForm.replace('#', '') + "FotoBase64Img]").attr('src', './images/no-image.jpg');
        } catch{
            $(idForm + " [id=" + idForm.replace('#', '') + "FotoBase64Img]")[0].src = './images/no-image.jpg';
        }
        $(idForm + " [id=" + idForm.replace('#', '') + "FotoBase64Label]").text('Escolha uma imagem...');
        $(idForm + " [id=Id]").val(0);
        $(idForm + " [id=Excluido]").val(false);
        $(idForm + " [id=IdEmpresa]").val(0);
        $(idForm + " [id=DataCadastro]").val('01/01/2020 00:00:00');
    },

    popularDropDown: function (value, text, id, idForm) {
        if (idForm !== undefined) {
            idForm = "#" + idForm + " ";
        }
        else {
            idForm = "";
        }

        $(idForm + " [id=" + id + "]").append('<option value="' + value + '">' + text);
    },

    readFile: function (fileInput, idForm, idInput) {
        if (fileInput.files && fileInput.files[0]) {
            if (fileInput.files[0].type.toString().includes("image/")) {
                if (fileInput.files[0].size < _tamanhoMaxImagem) { //500KB
                    $("#" + idForm + " [id=" + idForm + idInput + "Label]").text(fileInput.files[0].name);
                    let FR = new FileReader();
                    FR.addEventListener("load", function (e) {
                        //$element.closest("#" + idForm).find("#" + idInput + "Img")[0].src = e.target.result;
                        //$element.closest("#" + idForm).find("#" + idInput).val(e.target.result);
                        $("#" + idForm + " [id=" + idForm + idInput + "Img]")[0].src = e.target.result;
                        $("#" + idForm + " [id=" + idInput + "]").val(e.target.result);
                    });
                    FR.readAsDataURL(fileInput.files[0]);
                }
                else {
                    $("#" + idForm + " [id=" + idForm + idInput + "Label]").text(_mensagemLimite);
                    $("#" + idForm + " [id=" + idForm + idInput + "Img]")[0].src = "./images/no-image.jpg";
                    $("#" + idForm + " [id=" + idInput + "]").val("");
                }
            }
            else {
                $("#" + idForm + " [id=" + idForm + idInput + "Label]").text("Formato de arquivo incorreto.");
                $("#" + idForm + " [id=" + idForm + idInput + "Img]")[0].src = "./images/no-image.jpg";
                $("#" + idForm + " [id=" + idInput + "]").val("");
            }
        }
    },

    OnBlurValoresInteiro: function (obj) {
        if (obj.value === '') {
            obj.value = '0';
        }
    },

    OnBlurValoresDecimal: function (obj) {
        if (obj.value.length !== 0) {
            if (obj.value.indexOf(',') === -1) {
                obj.value = obj.value + ',00';
            }
            obj.value = obj.value.replace('.', ',');
        }
        else if (obj.value === '' || obj.value === '0' || obj.value === '00') {
            obj.value = '0,00';
        }
    }
};

var DATATABLE_HELPER = {
    montarTabela: function (config) {
        try {
            //let tabela = $("#" + config.idForm + " [id=" + config.idTabela + "]").empty().DataTable({
            let tabela = $('#' + config.idTabela).empty().DataTable({
                "oLanguage": {
                    "sSearch": config.search || "Busca:",
                    "sLengthMenu": config.lengthMenu || 'Mostrando _MENU_ registros por página',
                    "sZeroRecords": config.msgNenhumRegistroEncontrado || 'Nenhum resultado encontrado',
                    "sInfo": config.info || 'Mostrando _START_ a _END_ de _TOTAL_ resultados',
                    "sInfoEmpty": config.infoEmpty || 'Mostrando 0 a 0 de 0 resultados',
                    "sInfoFiltered": config.infoFiltered || '(filtrados _MAX_ registros)',
                    "sLoadingRecords": config.loadingRecords || "Carregando...",
                    "sProcessing": "Processando...",
                    "oPaginate": {
                        "sFirst": config.first || '←',
                        "sLast": config.last || '→',
                        "sNext": config.next || '»',
                        "sPrevious": config.previous || '«'
                    },
                    "select": {
                        "rows": {
                            "_": config.selecionados || "Selecionada %d linhas",
                            "0": config.cliqueSelecionar || "Clique em uma linha para selecioná-la",
                            "1": config.selecionado || "Selecionada 1 linha"
                        }
                    }
                },
                "orderCellsTop": !config.orderCellsTop === undefined || config.orderCellsTop,
                "select": config.select,
                "dom": config.dom,
                "processing": config.processing === undefined || config.processing,
                "buttons": config.buttons,
                "data": config.data,
                "columns": config.columns,
                "destroy": config.destroy === undefined || config.destroy,
                "fixedHeader": config.fixedHeader === undefined || config.fixedHeader,
                "scrollX": config.scrollX === undefined || config.scrollX,
                "scrollY": config.scrollY || "",
                "scrollCollapse": config.scrollCollapse === undefined || config.scrollCollapse,
                "order": config.order || [[0, 'asc']],
                "lengthMenu": config.lengthMenu || [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
                "pageLength": config.pageLength || 10,
                "autoWidth": !config.bAutoWidth === undefined || config.bAutoWidth,
                "bFilter": config.bFilter === undefined || config.bFilter,
                "fnDrawCallback": config.fnDrawCallback,
                "rowCallback": config.rowCallback,
                "searching": config.searching === undefined || config.searching,
                "paginate": config.paginate === undefined || config.paginate,
                "paging": config.paging === undefined || config.paging,
                //"serverSide": !config.serverSide === undefined || config.serverSide,
                "fixedColumns": {
                    "leftColumns": config.leftColumns || 0,
                    "rightColumns": config.rightColumns || 0
                },
                "columnDefs": config.columnDefs
            });
            $($('#' + config.idTabela + ' .sorting')[0]).click();
            return tabela;
        }
        catch (err) {
            console.log(err);
            return null;
        }
    }
};

String.prototype.capitalize = function () {
    return this.charAt(0).toUpperCase() + this.slice(1);
};

$(document).ready(function () {

    //Definir linguagem e estyle padrão do plugin select2.js
    $.fn.select2.defaults.set("theme", "bootstrap4");
    $.fn.select2.defaults.set('language', 'pt-BR');
    $('select').select2();

    //Definir linguagem padrão do plugin moment.js
    moment.locale("pt-br");

    /*
     * Localized default methods for the jQuery validation plugin.
     * Locale: PT_BR
     * https://jqueryvalidation.org/jQuery.validator.methods/
     * https://jqueryvalidation.org/number-method/
    */
    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    };
    $.validator.methods.range = function (value, element, param) {
        return this.optional(element) || value.replace(',', '.') >= param[0] && value.replace(',', '.') <= param[1];
    };


    //Outra opção: https://github.com/RobinHerbots/Inputmask
    $.jMaskGlobals.watchDataMask = true;
    $('.cep').mask('00000-000');
    $('.uf').mask('SS');
    $('.phone').mask('(00) 0000-0000');
    $('.phone_mobile').mask('(00) 00000-0000');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true });
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
    $('.money2').mask("0000000,00", { reverse: true, selectOnFocus: true });
    $('.percent').mask('Z000000,00', {
        translation: {
            'Z': {
                pattern: /[1]/, optional: true
            }
        }, onKeyPress: function (percent, event, currentField, options) {
            if (Number(percent.replace(',', '.')) > 1000000) {
                $(currentField).val("1000000,00");
            }
        }, reverse: true, selectOnFocus: true
    });
    $('.website').mask('https://00000000000000000000000000000000000000000000000000000000000000', {
        translation: {
            0: {
                pattern: /[a-zA-Z0-9./_-]/
            }
        }
    });
    //$('.percent').mask('##0,00', { reverse: true });
    //$('.date').mask('00/00/0000');
    //$('.time').mask('00:00:00');
    //$('.date_time').mask('00/00/0000 00:00:00');
    //$('.phone_with_ddd').mask('(00) 0000-0000');
    //$('.phone_us').mask('(000) 000-0000');
    //$('.mixed').mask('AAA 000-S0S');
    //$('.ip_address').mask('0ZZ.0ZZ.0ZZ.0ZZ', {
    //    translation: {
    //        'Z': {
    //            pattern: /[0-9]/, optional: true
    //        }
    //    }
    //});
    //$('.ip_address').mask('099.099.099.099');
    //$('.clear-if-not-match').mask("00/00/0000", { clearIfNotMatch: true });
    //$('.placeholder').mask("00/00/0000", { placeholder: "__/__/____" });
    //$('.fallback').mask("00r00r0000", {
    //    translation: {
    //        'r': {
    //            pattern: /[\/]/,
    //            fallback: '/'
    //        },
    //        placeholder: "__/__/____"
    //    }
    //});
    //$('.selectonfocus').mask("00/00/0000", { selectOnFocus: true });
});