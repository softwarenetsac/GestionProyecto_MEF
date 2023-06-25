var TableEstado;
var nombre_estado = "";
$(document).ready(function () {

    General.configurarGrilla();
    crearGrillaEstado();
    llenargrilla(true);

    init_contadorTa("txtNombre", "contadorTexto",100);
});

function crearGrillaEstado() {
    TableEstado = $('#gridEstado').DataTable({
        ordering: false,
        select: true,
        columns: [

            { "data": "" },
            { "data": "" },
            { "data": "iD_ESTADO", "visible": false },
            { "data": "nombre" },
            { "data": "" },
            { "data": "flG_ESTADO", "visible": false }
        ],
        "pageLength": 60,
        "columnDefs": [
            {
                'targets': 0,
                'searchable': false,
                'orderable': false,
                'render': function (data, type, row, meta) {
                    var html = "";
                    var parametro = row.iD_ESTADO + "," + meta.row;
                    html = '<a href="javascript:void(0)" onclick="ventanaEstado(' + parametro + ');" ><i class="icon icon-edit icon-lg m-c-sm"></i></a>';
                    return html;
                }
            },

            {
                'targets': 1,
                'searchable': false,
                'orderable': false,
                'render': function (data, type, row, meta) { 
                    var html = "";
                    var parametro = row.iD_ESTADO + "," + meta.row;
                    html = '<a href="javascript:void(0)" onclick="Anular_Estado(' + parametro + ');" ><i class="icon icon-remove icon-lg m-c-sm"></i></a>';
                    return html;
                }
            },

            {
                'targets': 4,
                'searchable': false,
                'orderable': false,
                'render': function (data, type, row, meta) {
                    var html = "INACTIVO";
                    if (row.flG_ESTADO == 1) {
                        html = "ACTIVO";
                    }
                    return html;
                }
            }
        ],

    });
    // TableContrato.buttons().container().appendTo('#gridContrato_wrapper .col-sm-12:eq(0)');

}




function llenargrilla(mostrarLoading) {

  

    var url = "/Estado/ListarEstado";


    /*
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            // body: JSON.stringify({ "a": 1, "b": 2 }),
            cache: 'no-cache'
        })
            .then(function (response) {
                debugger;
                return response.json();
            })
            .then(function (data) {
                var datax = data.result;
                TableEstado.rows.add(datax).draw();

            })
            .catch(function (err) {
                console.error(err);
            });
        */


    TableEstado.clear().draw();

    if ((mostrarLoading == true)) {
        var loading = General.addLoading();
        setTimeout(function () {
            var respuesta = General.POST(url, false);
            var data = respuesta.result;
            TableEstado.rows.add(data).draw();
            General.removeLoading(loading);
        }, 100);
    }
    else {
        var respuesta = General.POST(url, false);
        var data = respuesta.result;
        TableEstado.rows.add(data).draw();

    }


}


function ventanaEstado(id, row) {
    limpiarValidacionForm("frmModel");
    $("#hdnIdEstado").val("0");

    if (id > 0) {
        var data = TableEstado.rows(row).data();
        if (data != null) {
            var datax = data[0];
            $("#hdnIdEstado").val(datax.iD_ESTADO);
            $("#txtNombre").val(datax.nombre);
        }
    }

    $("#div_footer").show();
    $("#divRespuestaNotificacion").hide();
    $("#divProcesandoNotificacion").hide();
    $("#divNotificacion").show();
    $('#modalFirmadorJefe').modal('show');

}


function btnGuardarNotificacion_click() {
    var form = $("#frmModel");
    form.validate();
    if (!form.valid()) { return false; }
    else {
        GuardarEstado();
    }
}


function GuardarEstado() {
    $("#divNotificacion").hide();
    $("#div_footer").hide();
    $("#divProcesandoNotificacion").show();
    var url = "Estado/ProcearEstado";
    var item = {
        ID_ESTADO: $("#hdnIdEstado").val(),
        NOMBRE: $("#txtNombre").val()
    };
    var resultado = General.POST(url, item);
    if (resultado != null) {
        $("#divRespuestaNotificacion").html("");
        $("#divRespuestaNotificacion").html(resultado.message);
        $("#divRespuestaNotificacion").show();
        $("#divProcesandoNotificacion").hide();
        if (resultado.success == true) {
            $("#hdnIdEstado").val(resultado.result)
            llenargrilla(false);
        }
    }
}



function Anular_Estado(id, row) {

    var data = TableEstado.rows(row).data();
    if (data != null) {
        var texto = "Activar";
        var datax = data[0];
        nombre_estado = datax.nombre;
        if (datax.flG_ESTADO == 1) {
            texto = "Anular";
        }
        var flg_estado = datax.flG_ESTADO == 1 ? 0 : 1;
        var funcion = "Anular(" + datax.iD_ESTADO + "," + flg_estado +");";
        var mensaje = "¿ Estas seguro de " + texto + "?";
        General.Confirmar(mensaje, "Estado", "anulando espere por favor...");
        $('#btnConfirmarConfirmar').attr('onClick', funcion);
    }

}
function Anular(id,flg_estado) {


    $("#divPreguntaConfirmar").hide();
    $("#divProcesandoConfirmar").show();
    $("#divResultadoConfirmar").hide();

    var url = "Estado/AnularEstado";
    var item = {
        ID_ESTADO: id,
        NOMBRE: nombre_estado,
        FLG_ESTADO: flg_estado
    };



    var options = {};
    options.url = url;
    options.type = "POST";
    var obj = {};
    obj.ID_ESTADO = id;
    obj.NOMBRE = nombre_estado;
    obj.FLG_ESTADO = flg_estado;
   
    options.data = JSON.stringify(obj);
    options.contentType = "application/json";
    options.dataType = "json";

    options.beforeSend = function (xhr) {
        xhr.setRequestHeader("MY-XSRF-TOKEN",
            $('input:hidden[name="__RequestVerificationToken"]').val());
    };
    options.success = function (msg) {
        $("#msg").html(msg);
    };
    options.error = function () {
        $("#msg").html("Error while making Ajax call!");
    };
    $.ajax(options);

   
    /*
    var resultado = General.POST(url, item);
    if (resultado != null) {
       // $("#AnularEstado").html("");
       // $("#AnularEstado").html(resultado.message);
        if (resultado.success == true) {
            llenargrilla(false);
        }
    }
    */
}

function cerrarModal() {
    $('#modalFirmadorJefe').modal('hide');
}