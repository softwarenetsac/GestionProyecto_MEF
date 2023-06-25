function addLoading(mensaje) {
    if (mensaje == null || mensaje == undefined || mensaje == "") {
        mensaje = "Validadndo datos...";
    }
    var bgLoading = '<div id="cnt_loading" class="modal-backdrop fade show">';
    bgLoading += '<div style="display: flex;align-items:center;justify-content:center;height:400px">';
    bgLoading += '<div style="width:200px;;background-color:#ffffff" class="text-center"><br>';
    bgLoading += '<div>' + mensaje + '</div>';
    bgLoading += '<div class ="spinner spinner-primary spinner-lg pos-r sq-100"></div>';
    bgLoading += '</div></div></div>';
    $(bgLoading).appendTo('body');
    var e = document.getElementById("cnt_loading");
    return e;
}
function removeLoading(e) {
    setTimeout(function () {
        $(e).remove();
    }, 500);

}


function Alerta(succes, message) {
    if (message == null || message == "" || message == undefined) {
        message = "ops! no has definido el mensaje";
    }

    var html = "";

    html += '<div id="infoModalAlert" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog" class="modal fade">';
    html += '<div class="modal-dialog">';
    html += '<div class="modal-content">';
    html += '<div class="modal-header">';
    //html += '<button type="button" class="close" data-dismiss="modal">';
    //html += '<span aria-hidden="true">×</span>';
    //html += '<span class="sr-only">Close</span>';
    //html += '</button>';
    html += '</div>';
    html += '<div class="modal-body">';
    html += '<div class="text-center">';
    html += '<span class="text-primary icon icon-info-circle icon-5x"></span>';
    html += '<h3 class="text-primary">Aviso</h3>';
    html += '<p class="h4">';
    html += message;
    html += '.</p>';
    //html += '<p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.';
    // html += 'Animi ducimus id itaque totam saepe reiciendis corporis consectetur.</p>';
    html += '<div class="m-t-lg">';
    //html += '<button class="btn btn-primary" data-dismiss="modal" type="button">Continue</button>';
    html += '<button class="btn btn-default" data-dismiss="modal" type="button">Cerrar</button>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    html += '<div class="modal-footer"></div>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    $(".contenedoralerta").html(html);
    $("#infoModalAlert").modal();
    $("#infoModalAlert").modal('show');
    return;
};

$(document).ready(function () {


    $('#btnIngresar').click(function (e) {
        var form = $("#frmModel");
        form.validate().settings.ignore = ':hidden';
        if (!form.valid()) {
            return false;
        }
        else {
            $("#btnIngresar").text("Procesando...");
            $("#btnIngresar").attr("disabled", "true");

            var url = baseUrl +'Login/Iniciar';
            var loading = addLoading("Validando Datos");
            setTimeout(function () {
                $.post(url, $("#frmModel").serialize(), function (response) {
                    var respuesta = response;
                    if (respuesta != null) {
                        if (respuesta.Success) {
                            var url = baseUrl + 'Panel/index';
                            window.location = url;
                        }
                        else {
                            $("#btnIngresar").text("Ingresar");
                            $("#btnIngresar").removeAttr("disabled");
                            Alerta(false, respuesta.Message)
                        }
                    }
                });
                removeLoading(loading);
            }, 100);
        }
        //  e.preventDefault();
    });


});





