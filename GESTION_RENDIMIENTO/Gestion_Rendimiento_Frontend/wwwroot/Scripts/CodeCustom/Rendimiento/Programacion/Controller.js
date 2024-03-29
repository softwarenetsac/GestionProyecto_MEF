﻿try {
    ns('SoftwareNet.Web.Operacion.Programacion.Registro.Controller');
    SoftwareNet.Web.Operacion.Programacion.Registro.Controller = function () {
        var base = this;
        base.Ini = function (opts) {
            'use strict';
            base.Control.BotonAgregar().click(base.Event.BotonAgregarClick);
            base.Control.DllProgramacionr().select2({});
            base.Control.BotonAgregarPrioridad().click(base.Event.BotonAgregarPrioridadClick);
            base.Control.GridBody().on('click', '.AddRegDet', function (e) {
                var elemento = this;
                debugger;
                var id = elemento.getAttribute("idpk_e");
                if (typeof id !== 'undefined')
                    base.Function.AgregarRegistroPrioridadDet(id);
            });
            base.Control.GridBody().on('click', '.delete_mantenimiento', function (e) {
                $(this).closest('tr').remove();
            });
            base.Control.GridRendimiento().on('click', '.findone_mantenimiento', function (e) {
                debugger;
                var id = $(e.target).attr("idpk_M");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.AbrirModalRendimiento(parseInt(id), this);
            });
            base.Control.GridRendimiento().on('click', '.findone_verprioridad', function (e) {
                debugger;
                var id = $(e.target).attr("idpk_M");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.AbrirModalRendimientoVer(parseInt(id), this);
            });
            base.Control.GridRendimiento().on('click', '.delete_proyecto', function (e) {
                debugger;
                var id = $(e.target).attr("idpki_M");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.EliminarProyecto(parseInt(id), this);
            });
            base.Control.GridBody().on('click', '.AddEvidencia', function (e) {
                var elemento = this;
                debugger;
                var id_proy = elemento.getAttribute("idpk_proy");
                var id = elemento.getAttribute("idpk_detalle");
                
                if (typeof id !== 'undefined')
                    base.Function.AgregarRegistroPrioridadEvi(id_proy,id);
            });
            base.Control.BotonGuardar().click(base.Event.BotonGuardarClick);
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
            base.Function.CrearGrillaRendimiento();
            base.Function.BuscarGrilla();
            base.Control.BotonExportarPDF().click(base.Event.BotonExportarPDFClick);
            base.Control.BotonEnviarORH().click(base.Event.BotonGuardarActualizarEstadoClick);
            base.Control.GridRendimiento().on('click', '#chkAllSelection', function (e) {
                // debugger;

                $('#gridRendimiento tbody input[type="checkbox"]').prop('checked', this.checked);
                $('#gridRendimiento tbody tr').removeClass("odd selected check");
                if (this.checked) {
                    $('#gridRendimiento tbody tr input[type="checkbox"').parent().parent().parent().addClass('odd selected check');

                } else {
                    $('#gridRendimiento tbody tr').addClass("odd");
                }
            });
        };

        base.Parameters = {
            TableRendimiento: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonAgregar: function () { return $('#btnAgregar'); },
            ModalAgregarRendimiento: function () { return $("#modalRendimiento"); },
            FormMantenimiento: function () { return $("#frmModel"); },
            BotonAgregarPrioridad: function () { return $('#BtnAgregarPrioridad'); },
            GridBody: function () { return $("#gridBody"); },
            BotonGuardar: function () { return $("#btnGuardar"); },
            DllProgramacionr: function () { return $("#ID_EVALUADOR"); },
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            GridRendimiento: function () { return $("#gridRendimiento"); },
            hdnIdProyecto: function () { return $("#hdnIdProyecto"); },
            DllAnioConsulta: function () { return $("#ddlAnio_Consulta"); },
            BotonExportarPDF: function () { return $("#btnExportarPDF"); },
            BotonEnviarORH: function () { return $('#btnEnvioOrh'); },
            
        };

        base.Event = {
            BotonAgregarClick: function () {
                base.Function.AbrirModalRendimiento();
            },
            BotonAgregarDetalleRendimientoClick: function () {
                base.Function.AbrirModalDetalleRendimiento();
            },
            BotonAgregarPrioridadClick: function () {
                base.Function.AgregarRegistroPrioridad(0);
            },
            BotonGuardarClick: function () {
                var form = base.Control.FormMantenimiento();
                form.validate();
                if (!form.valid()) { return false; }
                else {

                    base.Control.Mensaje.Confirmation({
                        message: "¿Estás seguro de realizar el registro?",
                    }).ConfirmationAcept({
                        callback: function (opt) {
                            if (opt) {
                                var dataRequest = new Array();
                                var dataRequestD = new Array();
                                var dataRequestEvidencia = new Array();
                                $("#frmModel").find("#gridMantenimiento" + " tbody > tr").each(function (index, item) {
                                    var entorno = $(this);
                                    var ID_EVALUADOR = base.Control.DllProgramacionr().val();
                                    var DESCRIPCION = entorno.find(".tdDESCRIPCION", "id").val();
                                    var GRUPO = entorno.find(".tdP_G", "id").html();
                                    debugger;
                                    if (GRUPO == "1") {
                                        dataRequest.push({
                                            DESCRIPCION: DESCRIPCION,
                                            ID_EVALUADOR: ID_EVALUADOR,
                                            ID_PROYECTO: parseInt(base.Control.hdnIdProyecto().val()),
                                        });
                                    }

                                });
                                $("#frmModel").find("#gridMantenimiento" + " tbody > tr").each(function (index, item) {
                                    debugger
                                    var entorno = $(this);
                                    var GRUPO = entorno.find(".tdID_SUB_DETALLE", "id").html();
                                    var ID_DETALLE = entorno.find(".tdDetPrioridad", "id").html();
                                    var ID_PROYECTO = entorno.find(".tdID_PROYECTO_S", "id").html();  
                                    var EVIDENCIA = entorno.find(".tdEVIDENCIA", "id").val();
                                    var PLAZOS = entorno.find("input.tdPLAZOS", "id").val();
                                    var INDICADOR = entorno.find(".tdINDICADOR", "id").val();
                                    var VALOR = entorno.find("input.tdVALOR", "id").val();
                                    if (GRUPO == "1") {
                                        dataRequestD.push({
                                            EVIDENCIA: EVIDENCIA,
                                            INDICADOR: INDICADOR,
                                            VALOR: parseInt(VALOR),
                                            PLAZOS: PLAZOS,
                                            ID_DETALLE: parseInt(ID_DETALLE),
                                            ID_PROYECTO: parseInt(ID_PROYECTO),
                                        });
                                    }

                                });
                                $('.tdEVIDENCIA').each(
                                    function () {
                                        var elemento = this;
                                        debugger;
                                        var ID_ = elemento.getAttribute("idpk_detalle_");
                                        var ID_SUB = elemento.getAttribute("idpk_detallesub");
                                        if (parseInt(ID_)!=0) {
                                            var objdata = {
                                                ID_DETALLE_PROYECTO: parseInt(ID_),
                                                ID_DETALLE_SUB: parseInt(ID_SUB),
                                                EVIDENCIA: $(this).val(),
                                            };
                                            dataRequestEvidencia.push(objdata);
                                        }
                                  
                                    }
                                );
                                console.log(dataRequest);
                                console.log(dataRequestD);
                                console.log(dataRequestEvidencia);
                                base.Ajax.AjaxGuardar.data = {
                                    ItemsProyecto: dataRequest,
                                    ItemsPrioridad: dataRequestD,
                                    ItemsDetalleEvidencia: dataRequestEvidencia
                                }
                                base.Ajax.AjaxGuardar.submit();
                            }
                        }
                    });


                }
            },
            BotonGuardarActualizarEstadoClick: function () {
                var indexes = 0;
                $('.chkProyecto:checked').each(
                    function () {
                        indexes = 1;
                    }
                );
                if (indexes > 0) {
                    base.Control.Mensaje.Confirmation({
                        message: "¿Esta seguro que desea enviar para su revisión los registros seleccionados.?",
                    }).ConfirmationAcept({
                        callback: function (opt) {
                            if (opt) {
                                var dataRequest = new Array();
                                $('.chkProyecto:checked').each(
                                    function () {
                                        var objdata = {
                                            ID_PROYECTO: parseInt($(this).val()),
                                            ID_ESTADO: parseInt(4),
                                        };
                                        dataRequest.push(objdata);
                                    }
                                );
                                base.Ajax.AjaxActualizarEstado.data = {
                                    ItemsProyecto: dataRequest,
                                }
                                base.Ajax.AjaxActualizarEstado.submit();
                            }
                        }
                    });
                } else {
                    base.Control.Mensaje.Warning({ message: "Seleccione como mínimo un registro para enviar a la Oficina de Recursos Humanos." }).WarningClose();
                }
            },
            AjaxGuardarSuccess: function (data) {
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaGuardoExito })
                        .InformationClose({
                            callback: function (opt) {

                                if (opt) {
                                    base.Function.BuscarGrilla();
                                    base.Control.ModalAgregarRendimiento().modal("hide");
                                    base.Control.ModalAgregarRendimiento().html("");
                                }
                            }
                        });
                }
                else {
                    base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                }
            },
            AjaxEliminarSuccess: function (data) {
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: "El registro se eliminó correctamente." })
                        .InformationClose({
                            callback: function (opt) {
                                if (opt) {
                                    base.Function.BuscarGrilla();
                                }
                            }
                        });
                }
                else {
                    base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                }
            },
            AjaxActualizarEstadoSuccess: function (data) {
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: "Los registros seleccionados se enviaron correctamente." })
                        .InformationClose({
                            callback: function (opt) {
                                if (opt) {
                                    base.Function.BuscarGrilla();
                                }
                            }
                        });
                }
                else {
                    base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                }
            },
            BotonExportarPDFClick: function () {
                var anio = base.Control.DllAnioConsulta().val();

                $.paramcustom = {
                    url: SoftwareNet.Web.Operacion.Programacion.Actions.ExportarPDF,
                    values: {
                        ANIO: anio,
                    }
                }
                $.redirect();
            },
            BotonBuscarClick: function () {
                base.Function.BuscarGrilla();
            },
            AjaxBuscarSuccess: function (data) {

                if (data.Result) {
                    base.Parameters.TableRendimiento.clear().draw();
                    base.Parameters.TableRendimiento.rows.add(data.Result).draw();
                }
            },
        };
        base.Ajax = {
            AjaxGuardar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Programacion.Actions.Registrar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxGuardarSuccess
            }),
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Programacion.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
            }),
            AjaxEliminar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Programacion.Actions.Eliminar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxEliminarSuccess
            }),
            AjaxActualizarEstado: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Programacion.Actions.ActualizarEstado,
                autoSubmit: false,
                onSuccess: base.Event.AjaxActualizarEstadoSuccess
            }),
        };
        base.Function = {
            AbrirModalRendimiento: function (id, parent) {

                base.Control.DllProgramacionr().val('');
                base.Control.DllProgramacionr().trigger("change");
                base.Control.hdnIdProyecto().val(0);
                if (id > 0) {
                    var padre = $(parent).parent().parent();
                    var indice = base.Parameters.TableRendimiento.row(padre).index();
                    var data = base.Parameters.TableRendimiento.row(indice).data();
                    if (data != null) { 
                        base.Control.DllProgramacionr().val(data.ID_EVALUADOR);
                        base.Control.DllProgramacionr().trigger("change");
                    }
                    base.Control.hdnIdProyecto().val(id);
                    base.Function.AgregarRegistroPrioridad();
                    $('.btn_agr').hide();
                    
                } else {
                    $('.btn_agr').show();
                    base.Function.AgregarRegistroPrioridadM(0);
                }
                base.Control.ModalAgregarRendimiento().modal('show');
            },
            AbrirModalRendimientoVer: function (id, parent) {

                base.Control.DllProgramacionr().val('');
                base.Control.DllProgramacionr().trigger("change");
                base.Control.hdnIdProyecto().val(0);
                if (id > 0) {
                    var padre = $(parent).parent().parent();
                    var indice = base.Parameters.TableRendimiento.row(padre).index();
                    var data = base.Parameters.TableRendimiento.row(indice).data();
                    if (data != null) {
                        base.Control.DllProgramacionr().val(data.ID_EVALUADOR);
                        base.Control.DllProgramacionr().trigger("change");
                    }
                    base.Control.hdnIdProyecto().val(id);
                    base.Function.AgregarRegistroPrioridad();
                    $('.btn_agr').hide();
                    $('.AddRegDet').hide();
                    $('.AddEvidencia').hide();
                    $('#btnGuardar').hide();
                    $('.deletered').hide();
                    $("input").prop('disabled', true);
                    $("textarea").prop('disabled', true);    
                }
                //else {
                //    $('.btn_agr').show();
                //    base.Function.AgregarRegistroPrioridadM(0);
                //}
                base.Control.ModalAgregarRendimiento().modal('show');
            },
            GetData: function () {
                var dataRequest = new Array();
                var fila = 1;
                $("#frmModel").find("#gridMantenimiento" + " tbody > tr").each(function (index, item) {
                    var entorno = $(this);
                    var DESCRIPCION = entorno.find(".tdDESCRIPCION", "id").val();
                    var GRUPO = entorno.find(".tdP_G", "id").html();
                    if (GRUPO == "1") {
                        dataRequest.push({
                            DESCRIPCION: DESCRIPCION,
                            ID_PROYECTO: parseInt(base.Control.hdnIdProyecto().val()) 
                        });
                        fila++;
                    }
                   
                });
           
                return dataRequest;
            },
            GetDataDet: function (id) {
                var dataRequest = new Array();
                var fila = 1;
                $("#frmModel").find("#gridMantenimiento" + " tbody > tr").each(function (index, item) {
                    var entorno = $(this);
                    var GRUPO = entorno.find(".tdID_SUB_DETALLE", "id").html();
                    var ID_DETALLE = entorno.find(".tdDetPrioridad", "id").html();
                    var EVIDENCIA = entorno.find(".tdEVIDENCIA", "id").val();
                    var PLAZOS = entorno.find("input.tdPLAZOS", "id").val();
                    var INDICADOR = entorno.find(".tdINDICADOR", "id").val();
                    var VALOR = entorno.find("input.tdVALOR", "id").val();
                    if (GRUPO == "1") {
                        dataRequest.push({
                            EVIDENCIA: EVIDENCIA,
                            INDICADOR: INDICADOR,
                            VALOR: parseInt(VALOR),
                            PLAZOS: PLAZOS,
                            //ID_DETALLE_SUB: parseInt(fila),
                            ID_DETALLE: ID_DETALLE ,
                            ID_PROYECTO: parseInt(id),
                        });
                        fila++;
                    }
               
                });

                return dataRequest;
            },
            GetDataDetEvidencia: function (ID_DETALLE_PROYECTO,ID) {
                var dataRequest = new Array();
                $('.tdEVIDENCIA').each(
                    function () {
                        var elemento = this;
                        debugger;
                        var ID_ = elemento.getAttribute("idpk_detalle_");
                        var ID_DETALLE_SUB = elemento.getAttribute("idpk_detallesub");
                        var objdata = {
                            ID_DETALLE_PROYECTO: parseInt(ID_)  ,// == 0 ? ID_ : parseInt(ID_DETALLE_PROYECTO),
                            ID_DETALLE_SUB: ID_DETALLE_SUB > 0 ? parseInt(ID_DETALLE_SUB):0,
                            EVIDENCIA: $(this).val(),
                        };
                        dataRequest.push(objdata);
                    }
                );
                return dataRequest;
            },
            AgregarRegistroPrioridadM: function (id) {
                var url = SoftwareNet.Web.Operacion.Programacion.Actions.NuevaFila;
                var item = {
                    ItemsProyecto: null,
                    ItemsPrioridad: null,
                    ID_PROYECTO: parseInt(id),
                    TIPO: "E",
                    ID_DETALLE_TEMP: parseInt(0),
                }
                var respuesta = General.POST(url, item, false);
                var row = respuesta.Extra;
                $("#gridBody").html(row);

            },
            AgregarRegistroPrioridad: function () {
                var url = SoftwareNet.Web.Operacion.Programacion.Actions.NuevaFila;
                var itemPrioridad = base.Function.GetData();
                var itemPrioridadDet = base.Function.GetDataDet(0);
                var itemPrioridadEvidencia = base.Function.GetDataDetEvidencia(0);
                    var item = {
                        ItemsProyecto: itemPrioridad,
                        ItemsPrioridad: itemPrioridadDet,
                        ItemsDetalleEvidencia: itemPrioridadEvidencia,
                        ID_PROYECTO: base.Control.hdnIdProyecto().val(),
                        TIPO: "E",
                        ID_DETALLE_TEMP: parseInt(0),
                    }
                    var respuesta = General.POST(url, item, false);
                var row = respuesta.Extra;
                $("#gridBody").html(row);
     
            },
            AgregarRegistroPrioridadDet: function (id) {
                var url = SoftwareNet.Web.Operacion.Programacion.Actions.NuevaFila;
                var itemPrioridad = base.Function.GetData();
                var itemPrioridadDet = base.Function.GetDataDet(id);
                var itemPrioridadEvidencia = base.Function.GetDataDetEvidencia(id);
                var item = {
                    ItemsProyecto: itemPrioridad,
                    ItemsPrioridad: itemPrioridadDet,
                    ItemsDetalleEvidencia: itemPrioridadEvidencia,
                    ID_PROYECTO: parseInt(base.Control.hdnIdProyecto().val()),
                    TIPO: "D",
                    ID_DETALLE_TEMP: parseInt(id),
                }
                var respuesta = General.POST(url, item, false);
                var row = respuesta.Extra;
                $("#gridBody").html(row);
            },
            AgregarRegistroPrioridadEvi: function (ID_PROYECTO, ID_DETALLE_PROYECTO) {
                var url = SoftwareNet.Web.Operacion.Programacion.Actions.NuevaFila;
                var itemPrioridad = base.Function.GetData();
                var itemPrioridadDet = base.Function.GetDataDet(ID_PROYECTO);
                var itemPrioridadEvidencia = base.Function.GetDataDetEvidencia(ID_DETALLE_PROYECTO,1);
                debugger;
                var item = {
                    ItemsProyecto: itemPrioridad,
                    ItemsPrioridad: itemPrioridadDet,
                    ItemsDetalleEvidencia: itemPrioridadEvidencia,
                    ID_PROYECTO: parseInt(ID_PROYECTO),
                    TIPO: "EV",
                    ID_DETALLE_TEMP: parseInt(ID_PROYECTO),
                    ID_DETALLE_PROYECTO: parseInt(ID_DETALLE_PROYECTO),
                }
                var respuesta = General.POST(url, item, false);
                var row = respuesta.Extra;
                $("#gridBody").html(row);
            },
            CrearGrillaRendimiento: function () {
                General.configurarGrilla();
                var chk = "Sel.";
                chk += "</br>";
                chk += '<label class="custom-control custom-control-primary custom-checkbox">';
                chk += '<input class="custom-control-input" type="checkbox" name="chkAllSelection" id=chkAllSelection >';
                chk += '<span class="custom-control-indicator" style="border-color: #d9230f;"></span>';
                chk += '</label>';
                base.Parameters.TableRendimiento = base.Control.GridRendimiento().DataTable({
                    ordering: false,
                    select: true,
                    columns: [
                        { "data": "", "title": chk, "class": "text-center", 'orderable': false },
                        { "data": "", "title": "Editar", "class": "text-center" },
                        { "data": "", "title": "Anular", "class": "text-center" },
                        { "data": "ID_PROYECTO", "visible": false },
                        { "data": "ANIO", "title": "Año", "class": "text-center" },
                        { "data": "DESCRIPCION", "title": "Prioridades Anuales", "width": "20%" },
                        { "data": "NOMBRE_EVALUADO", "title": "Evaluado" },
                        { "data": "NOMBRE_EVALUADOR", "title": "Evaluador" },
                        { "data": "PLAZO", "title": "Plazo", "class": "text-center", "visible": false },
                        { "data": "NOMBRE_ESTADO", "title": "Estado", "class": "text-center" },
                        { "data": "ID_OFICINA", "visible": false },
                        { "data": "ID_PERSONAL", "visible": false },
                        { "data": "ID_ESTADO", "visible": false },
                        { "data": "ID_EVALUADOR", "visible": false },
                    ],
                    "pageLength": 30,
                    "columnDefs": [
                        {
                            'targets': 0,
                            'searchable': false,
                            'orderable': false,
                            'render': function (data, type, row, meta) {
                                var html = "";
                                if (row.ID_ESTADO == "1") {
                                    html += '<label class="custom-control custom-control-primary custom-checkbox">';
                                    html += '<input class="custom-control-input chkProyecto" type="checkbox"  name=chk_' + row.ID_PROYECTO + ' id=chk_' + row.ID_PROYECTO + ' value=' + row.ID_PROYECTO + '  >';
                                    html += '<span class="custom-control-indicator" style="border-color: #d9230f;font-size:20px;"></span>';
                                    html += '</label>';
                                }
                       
                                return html;
                            }
                        },
                        {
                            'targets': 1,
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-center',
                            "render": function (data, type, row, meta) {
                                var html = "";
                                switch (row.ID_ESTADO) {
                                    case "1":
                                        html = '<a href="javascript:void(0);" title="Editar Prioridad" ><i class="icon icon-edit icon-lg m-c-sm findone_mantenimiento" secuencial="' + row.INDICE + '" idpk_M="' + row.ID_PROYECTO + '"></i></a>';
                                        break;
                                    case "4":
                                        html = '<a href="javascript:void(0);" title="Ver Prioridad"><i class="icon icon-search-plus icon-lg m-c-sm findone_verprioridad" secuencial="' + row.INDICE + '" idpk_M="' + row.ID_PROYECTO + '"></i></a>';
                                        break;
                                }
                                return html;
                            }
                        },
                        {
                            'targets': 2,
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-center',
                            'render': function (data, type, row, meta) {
                                var html = "";
                                if (row.ID_ESTADO=="1") {
                                    html = '<a href="javascript:void(0);" title="Anular Prioridad" ><i class="icon icon-remove icon-lg m-c-sm delete_proyecto" secuencial="' + row.INDICE + '" idpki_M="' + row.ID_PROYECTO + '"></i></a>';
                                }
                                return html;
                            }
                        },
                    ],
                });
            },
            BuscarGrilla: function () {
                base.Ajax.AjaxBuscar.data = {
                    ANIO: base.Control.DllAnioConsulta().val(),
                }
                base.Ajax.AjaxBuscar.submit();
            },
            EliminarProyecto: function (ID_PROYECTO, parent) {
                base.Control.Mensaje.Confirmation({
                    message: "¿Estás seguro de elimiar el registro?",
                }).ConfirmationAcept({
                    callback: function (opt) {
                        if (opt) {
                    
                            base.Ajax.AjaxEliminar.data = {
                                ID_PROYECTO: ID_PROYECTO,
                            }
                            base.Ajax.AjaxEliminar.submit();
                        }
                    }
                });
            },
            loading: null,
            ShowLoading: function () {
                this.loading = new SoftwareNet.DJ.Web.Components.ProgressBar({ targetLoading: this.targetLoading });
            },
            HideLoading: function () {
                this.loading.hide();
            }

        };
    };
} catch (ex) {
    alert(ex.message);
}