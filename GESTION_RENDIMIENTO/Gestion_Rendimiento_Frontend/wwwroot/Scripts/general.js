General = {
    POST_Async: function (url, parameters, funcionSuccess) {
        var rsp;
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            async: true,
            data: JSON.stringify(parameters),
            success: function (response) {
                rsp = response;
                if (typeof (funcionSuccess) == 'function') {
                    funcionSuccess(response);
                }
            },
            failure: function (msg) {
                alert(msg);
                rsp = msg;
            },
            error: function (xhr, status, error) {
                alert(error);
                rsp = error;
            }
        });

        return rsp;
    },
    POST: function (url, parameters, funcionSuccess) {
        var rsp;
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            cache: false,
            dataType: "json",
            async: false,
            data: JSON.stringify(parameters),
            success: function (response) {
                rsp = response;
                if (typeof (funcionSuccess) == 'function') {
                    funcionSuccess(response);
                }
            },
            failure: function (msg) {
                alert(msg);
                rsp = msg;
            },
            error: function (xhr, status, error) {
                alert(error);
                rsp = error;
            }
        });

        return rsp;
    },
    GET: function (url, parameters, funcionSuccess) {
        var rsp;
        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            async: false,
            success: function (response) {
                rsp = response;
                if (typeof (funcionSuccess) == 'function') {
                    funcionSuccess(response);
                }
            },
            failure: function (msg) {
                alert(msg);
                rsp = msg;
            },
            error: function (xhr, status, error) {
                alert(error);
                rsp = error;
            }
        });
        return rsp;
    },
    configurarGrilla: function (searching, pageLength) {
        // Setting datatable defaults
        if (searching == null || searching == "" || searching == undefined) { searching = false };
        if (pageLength == null || pageLength == "" || pageLength == undefined) { pageLength = 60 };

        $.extend($.fn.dataTable.defaults,
        {
            searching: searching,
            autoWidth: false,

         //    pageLength: pageLength,

            /* columnDefs: [{
                 "targets": 'no-sort',
                 "orderable": false,
                 "order": []
             }],
             */
            //dom: '<"datatable-header"fpl><"datatable-scroll"t><"datatable-footer"ip>',        
            //dom: '<"datatable-scroll"t><"datatable-footer"fpl>',

            //"sPaginationType": "full_numbers",
            //"iDisplayLength": 100,

            //cambio nuevo 09/12/2020 inicio
            "bLengthChange": false,//esto muestra oculta eñ combo por encima del grid
            "bInfo": true,//esto oculta muestra el texto de la paginacion
            //"pagingType": "full_numbers",
            //cambio 09/12/2020 final

            //"sDom": '<"top"<"actions">fpi<"clear">><"clear">rt<"bottom">',

            //esto se estaba usando antes
            //dom: "<'row'<'col-sm-6'><'col-sm-6'f>>" + "<'table-responsive'tr>" + "<'row'<'col-sm-6'><'col-sm-6'p>>",

            //language: {
            //    search: '<span>Buscar:</span> _INPUT_',
            //    lengthMenu: '<span>Ver:</span> _MENU_',
            //    emptyTable: "No existen registros",
            //    sZeroRecords: "No se encontraron resultados",
            //    sInfoEmpty: "No existen registros que contabilizar",
            //    sInfoFiltered: "(filtrado de un total de _MAX_ registros)",
            //    sInfo: "Mostrando del registro _START_ al _END_ de un total de _TOTAL_ dato(s)",
            //    paginate: { 'first': 'First', 'last': 'Last', 'next': '&rarr;', 'previous': '&larr;' }

            //},
            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registro(s) del _START_ al _END_ de un total de _TOTAL_ registro(s)",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                },
                "select": {                    
                    "rows": {

                        //"_": "Seleccionado %d registros",
                        //"1": "Seleccionado 1 registro",
                        //"0": "",

                        "_": "",
                        "1": "",
                        "0": "",
                    }
                },

            }

        });

    },
    configurarGrilla_2: function (searching, pageLength) {
        // Setting datatable defaults
        if (searching == null || searching == "" || searching == undefined) { searching = false };
        if (pageLength == null || pageLength == "" || pageLength == undefined) { pageLength = 60 };

        $.extend($.fn.dataTable.defaults,
        {
            searching: searching,
            autoWidth: false,

                pageLength: pageLength,

            "bLengthChange": false,//esto muestra oculta eñ combo por encima del grid
            "bInfo": true,//esto oculta muestra el texto de la paginacion
 
            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando registro(s) del _START_ al _END_ de un total de _TOTAL_ registro(s)",
                "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "Último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                },
                "select": {
                    "rows": {

                        //"_": "Seleccionado %d registros",
                        //"1": "Seleccionado 1 registro",
                        //"0": "",

                        "_": "",
                        "1": "",
                        "0": "",
                    }
                },

            }

        });

    },
    seleccionarGrid: function (grid) {
        grid.ControlGrid.find('tr').eq(grid.Fila).addClass("selected");

        event = {
            data: function () {
                var rows = grid.DataGrid.rows('.selected').indexes();
                data = grid.DataGrid.rows(rows).data()[0];
                debugger;
                return (data == undefined ? [] : data);

            }
        }
        return event;
    },
    retornarDataGrid: function (grid) {

        event = {
            data: function () {
                var data = grid.DataGrid.rows().data();
                return data;
            }

        }
        return event;
    },
    Alerta: function (succes, message) {
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
    },
    Confirmar: function (pregunta, nombreboton, mensajeProcesando) {

        if (pregunta == null || pregunta == "" || pregunta == undefined) {
            pregunta = "ops! no has definido el mensaje";
        }
        if (mensajeProcesando == null || mensajeProcesando == "" || mensajeProcesando == undefined) {
            mensajeProcesando = "Procesando espere por favor...";
        }
        if (nombreboton == null || nombreboton == "" || nombreboton == undefined) {
            nombreboton = "Guardar";
        }



        var html = "";
        html += '<form id="frmModelAlert" name="frmModelAlert" data-toggle="validator" method="post" enctype="multipart/form-data">';
        html += '<div id="modalConfirmacion"  role="dialog" class="modal fade" data-backdrop="static" data-keyboard="false">';
        html += '<div class="modal-dialog">';
        html += '<div class="modal-content">';
        html += '<div class="modal-body">';
        
        html += '<div id="divProcesandoConfirmar" style="display:none">';
        html += '<div class="panel panel-body text-center" data-toggle="match-height">';
        html += '<h3>' + mensajeProcesando + '</h3>';
        html += '<div class="spinner spinner-primary spinner-lg pos-r sq-100"></div>';
        html += '</div>';
        html += '</div>';
        html += '<div id="divPreguntaConfirmar" class="text-center">';
        html += '<span class="text-primary icon icon-question-circle icon-5x"></span>';
        html += '<h3 class="text-primary" id="lblPreguntaGuardar"> ' + pregunta + ' </h3>';

        html += '<div class="m-t-lg">';
        html += '<button  name= "btnConfirmarConfirmar" id="btnConfirmarConfirmar" class="btn btn-primary" type="button">' + nombreboton + '</button>';
        html += '<button class="btn btn-default m-l-sm" data-dismiss="modal" type="button">Cancelar</button>';
        html += '</div>';
        html += '</div>';
        html += '<div id="divResultadoConfirmar" style="display:none">';
        html += '<div class="text-center">';
        html += '<span id="iconoRespuestaconfirmar" class="text-success icon  icon-check icon-5x"></span>';
        html += '<h3 class="text-success" id="lblResultadoConfirmar"></h3>';

        html += '<div class="m-t-lg">';
        html += '<button class="btn btn-default" data-dismiss="modal" type="button">Cerrar</button>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += ' </div>';  
        html += '<!--<div class="modal-footer"></div>-->';
        html += '</div>';
        html += ' </div>';
        html += '</div>';
        html += '</form>';
        $(".contenedorconfirmacion").html(html);
        $("#modalConfirmacion").modal();
        $("#modalConfirmacion").modal('show');


        // $('#btnConfirmarConfirmar').attr('onClick', 'ejecutar_firma();');

    },
    RespuestaConfirmacion: function (Success, message, funcion_consulta) {

        $("#divPreguntaConfirmar").hide();
        $("#divProcesandoConfirmar").hide();
        $("#divResultadoConfirmar").show();


        if (Success) {
            $("#lblResultadoConfirmar").html(message);
            $("#iconoRespuestaconfirmar").attr('class', 'text-success icon icon-check icon-5x');
            $("#lblResultadoConfirmar").attr('class', 'text-success');

            if (typeof (funcion_consulta) == 'function') {
                funcion_consulta();
            }
        }
        else {
            $("#lblResultadoConfirmar").html(message);
            $("#iconoRespuestaconfirmar").attr('class', 'text-danger icon icon-warning icon-5x');
            $("#lblResultadoConfirmar").attr('class', 'text-danger');

        }
    },

    ProcesandoConfirmacion: function (valor) {
        if (valor == 1) {
            $("#divPreguntaConfirmar").hide();
            $("#divProcesandoConfirmar").show();
            $("#divResultadoConfirmar").hide();
        }
        else {
            $("#modalConfirmacion").modal('hide');
        }

    },

    Escape: {
        PatternDDMMYYYY: /^([0-9]{2})\/([0-9]{2})\/([0-9]{4})$/
    },
    ValidaFecha_DDMMYYYY: function (valor) {
        var response = this.Escape.PatternDDMMYYYY.test(valor);

        return response;

    },

    addLoading: function (mensaje) {
        if (mensaje == null || mensaje == undefined || mensaje == "") {
            mensaje = "Cargando datos...";
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
    },
    removeLoading: function (e) {
        setTimeout(function () {
            $(e).remove();
        }, 500);

    },
    removerComas: function (cadena) {
        var nuevoValor = 0;
        var valor = (cadena == null || cadena == undefined || cadena == "") ? 0 : cadena;
        if (valor != 0) {
            nuevoValor = $.trim(cadena.replace(/,/g, ""));
        }
        return nuevoValor;
    },
    cargarUnidadOrganica: function (url, parametro, cargando, idoficina, seleccionar) {


        if (cargando == false) {
            var resultado = General.POST(url, parametro, false);
            General.cargarCombobox(resultado, idoficina);
        }
        else {
            var loading = General.addLoading("cargando datos");
            setTimeout(function () {
                var resultado = General.POST(url, parametro, false);
                General.cargarCombobox(resultado, idoficina, seleccionar);
                General.removeLoading(loading);
            }, 100);
        }
    },
    cargarCombobox: function (data, idCombobox, seleccionar) {
        var items = "";
        if (data != null && data != undefined) {
            if (data.length == 0) {
                var selecionado = seleccionar;
                if ((selecionado == undefined || selecionado == null || selecionado == "")) {
                    selecionado = "-----Todos-----";
                }
                items += "<option value='' selected='selected'>" + selecionado + " </option>";
            }
            else {
                if ((seleccionar == null || seleccionar == undefined || seleccionar == "")) {
                    items += "<option value='' selected='selected'>-----Seleccionar-----</option>";
                }
                else {
                    items += "<option value='' selected='selected'>" + seleccionar + "</option>";
                }
            }

            $.each(data, function (i, v) {
                items += "<option value=\"" + v.Codigo + "\">" + v.Nombre + "</option>";
            });
        }
        else {
            selecionado = "------Todos--------------";
            items += "<option value='' selected='selected'>" + selecionado + " </option>";
        }

        $("#" + idCombobox).html(items);
    },
    validaSunat: function (value) {
        debugger;
        var ruc = value.replace(/[-.,[\]()\s]+/g, ""),
            valido;

        //Es entero?    
        if ((ruc = Number(ruc)) && ruc % 1 === 0
            && validoRuc(ruc)) { // ⬅️ Acá se comprueba
            valido = true;
            //obtenerDatosSUNAT(ruc);
        } else {
            valido = false;
        }
        return valido;

        function validoRuc(ruc) {
            //11 dígitos y empieza en 10,15,16,17 o 20
            if (!(ruc >= 1e10 && ruc < 11e9
               || ruc >= 15e9 && ruc < 18e9
               || ruc >= 2e10 && ruc < 21e9))
                return false;

            for (var suma = -(ruc % 10 < 2), i = 0; i < 11; i++, ruc = ruc / 10 | 0)
                suma += (ruc % 10) * (i % 7 + (i / 7 | 0) + 1);
            return suma % 11 === 0;

        }
    },
    validaMayorEdad: function (value, separador, mayorEdad) {
        if (typeof eparador === 'undefined' || separador == null)
            separador = "/";


        var from = value.split(separador); // DD MM YYYY

        var day = from[0];
        var month = from[1];
        var year = from[2];
        var age = (mayorEdad == "" || mayorEdad == null || typeof mayorEdad === 'undefined' ? 18 : parseInt(mayorEdad));

        var mydate = new Date();
        mydate.setFullYear(year, month - 1, day);

        var currdate = new Date();
        var setDate = new Date();

        setDate.setFullYear(mydate.getFullYear() + age, month - 1, day);

        if ((currdate - setDate) > 0) {
            return true;
        } else {
            return false;
        }
    },
    BlockPageInspecSolugen: function (flag) {
            if (flag) {
                document.onkeypress = function (event) {
                    event = (event || window.event);
                    if (event.keyCode == 123) {
                        //alert('No F-12');
                        return false;
                    }
                }
                document.onmousedown = function (event) {
                    event = (event || window.event);
                    if (event.keyCode == 123) {
                        //alert('No F-keys');
                        return false;
                    }
                }
                document.onkeydown = function (event) {
                    event = (event || window.event);
                    if (event.keyCode == 123) {
                        //alert('No F-keys');
                        return false;
                    }
                }
                $(document).keydown(function (event) {
                    if (event.keyCode == 123) {
                        return false;
                    }
                    else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
                        return false;
                    }
                });

                $(document).on("contextmenu", function (e) {
                    e.preventDefault();
                });
                $(document).keydown(function (event) {
                    if (event.keyCode == 123) {
                        return false;
                    }
                    else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
                        return false;
                    }
                });
                $(document).on("contextmenu", function (e) {
                    e.preventDefault();
                });
            }
        }
}
function ValidarNumerosEnteros(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    // if (charCode == 45) return false;
    if (charCode == 8) return true;
    //if (charCode == 46) return true;

    var patron = /[0-9]/; ///^[0-9]+$/
    var te = String.fromCharCode(charCode);
    return patron.test(te);
}

function deshabilitarControles(nameContenedor) {
    $('#' + nameContenedor).find('input, select, textarea').attr('disabled', true);
    $('#' + nameContenedor).find('checkbox, radio').attr('disabled', true);
    $('#' + nameContenedor + '').find('input').datepicker('disable');
    $('#' + nameContenedor).parent().find("button").attr("style", "display:none");
    $.each($('#' + nameContenedor + ' a[id=aEditar]'), function (i, obj) {
        obj.style = "display:none";
    });
    $.each($('#' + nameContenedor + ' a[id=aEliminar]'), function (i, obj) {
        obj.style = "display:none";
    });
}
function deshabilitarControles_v2(nameContenedor) {
    $('#' + nameContenedor).find('input, select, textarea').attr('disabled', true);
    $('#' + nameContenedor).find('checkbox, radio').attr('disabled', true);
    $('#' + nameContenedor + '').find('input').datepicker('disable');
    //$('#' + nameContenedor).parent().find("button").attr("style", "display:none");
    $.each($('#' + nameContenedor + ' a[id=aEditar]'), function (i, obj) {
        obj.style = "display:none";
    });
    $.each($('#' + nameContenedor + ' a[id=aEliminar]'), function (i, obj) {
        obj.style = "display:none";
    });
}

/*
function addLoading(mensaje) {
    if (mensaje == null || mensaje == undefined) {
        mensaje = "Cargando datos...";
    }
    var bgLoading = `<div id="cnt_loading" class="modal-backdrop fade show">
    <div style="display: flex;align-items:center;justify-content:center;height:400px">
   <div style="width:200px;;background-color:#ffffff" class="text-center"><br>
   <div>`+ mensaje + `</div>
   <div class ="spinner spinner-primary spinner-lg pos-r sq-100"></div>
    </div></div></div>`;
    $(bgLoading).appendTo('body');
    var e = document.getElementById("cnt_loading");
    return e;
}

function removeLoading(e) {
    setTimeout(function () {
        $(e).remove();
    }, 500);
}
*/

function Configurar_Modal(div) {
    $("#" + div).draggable({
        handle: ".modal-header", cursor: "move"
    })
}

function limpiarControles(contenedor) {
    var frm = contenedor == null ? "" : (contenedor == "" ? "" : ("#" + contenedor + " "));
    $(frm + " input").val('');
    $(frm + " select").val('');
    $(frm + " textarea").val('');
}

function Configurar_Ventana(div) {
    $("#" + div).draggable({
        handle: ".modal-header", cursor: "move"
    })
}



function ValidarDecimales(evt, input) {
    // Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filterDecimal(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 46) {
            if (filterDecimal(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}
function filterDecimal(__val__) {
    var preg = /^([0-9]+\.?[0-9]{0,2})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }

}

function limpiarValidacionForm(form) {
    var validator = $("#" + form).validate()
    validator.resetForm();
    $("#" + form + " .has-error").removeClass("has-error");
    $("#" + form + " .has-success").removeClass("has-success");
}

function validaLetras(event) {
    var regex = new RegExp("^[a-zA-Z ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function validaAlfanumerico(event) {
    var regex = new RegExp("^[a-zA-Z0-9 ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function init_contadorTa(idtextarea, idcontador) {

    var max = $("#" + idtextarea).attr("maxlength");
    if (parseInt(max) > 0) {
        $("#" + idtextarea).keyup(function () {
            updateContadorTa(idtextarea, idcontador, max);
        });
        $("#" + idtextarea).change(function () {
            updateContadorTa(idtextarea, idcontador, max);
        });
    }
}
function updateContadorTa(idtextarea, idcontador, max) {

    var contador = $("#" + idcontador);
    var ta = $("#" + idtextarea);
    if (typeof max === "undefined") {
        max = ta.attr("maxlength");
        if (typeof max === "undefined") {
            max = 0;
        }
    }
    if (max > 0) {
        contador.html("0/" + max);
        var areacontrol = "Cantidad de caracteres: " + ta.val().length + "/" + max;
        // contador.html(ta.val().length + "/" + max);
        contador.html(areacontrol);
        if (parseInt(ta.val().length) > max) {
            ta.val(ta.val().substring(0, max - 1));
            contador.html(max + "/" + max);
        }
    }

}