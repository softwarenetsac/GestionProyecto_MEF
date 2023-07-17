try {
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
                var id = $(e.target).attr("idpk_M");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.AbrirModalRendimiento(id, this);
            });
            base.Control.BotonGuardar().click(base.Event.BotonGuardarClick);
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
            base.Function.CrearGrillaRendimiento();
            base.Function.BuscarGrilla();
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
                                            ID_PROYECTO: base.Control.hdnIdProyecto().val(),
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

                                base.Ajax.AjaxGuardar.data = {
                                    ItemsProyecto: dataRequest,
                                    ItemsPrioridad: dataRequestD,
                                }
                                base.Ajax.AjaxGuardar.submit();
                            }
                        }
                    });


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
                                }
                            }
                        });
                }
                else {
                    base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                }
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
                    }
                    base.Control.hdnIdProyecto().val(id);
                    base.Function.AgregarRegistroPrioridad(id);
                    $('.btn_agr').hide();
                    
                } else {
                    $('.btn_agr').show();
                    base.Function.AgregarRegistroPrioridadM(0);
                }
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
                            ID_DETALLE: parseInt(ID_DETALLE),
                            ID_PROYECTO: parseInt(id),
                        });
                        fila++;
                    }
               
                });

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
            AgregarRegistroPrioridad: function (id) {
                var url = SoftwareNet.Web.Operacion.Programacion.Actions.NuevaFila;
                var itemPrioridad = base.Function.GetData();
                var itemPrioridadDet = base.Function.GetDataDet(0);
                    var item = {
                        ItemsProyecto: itemPrioridad,
                        ItemsPrioridad: itemPrioridadDet,
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
                var item = {
                    ItemsProyecto: itemPrioridad,
                    ItemsPrioridad: itemPrioridadDet,
                    ID_PROYECTO: parseInt(base.Control.hdnIdProyecto().val()),
                    TIPO: "D",
                    ID_DETALLE_TEMP: parseInt(id),
                }
                var respuesta = General.POST(url, item, false);
                var row = respuesta.Extra;
                $("#gridBody").html(row);
            },
            CrearGrillaRendimiento: function () {
                General.configurarGrilla();
                base.Parameters.TableRendimiento = base.Control.GridRendimiento().DataTable({
                    ordering: false,
                    select: true,
                    columns: [
                        { "data": "", "title": "Editar", "class": "text-center" },
                        { "data": "", "title": "Anular", "class": "text-center" },
                        { "data": "ID_PROYECTO", "visible": false },
                        { "data": "ANIO", "title": "Año", "class": "text-center" },
                        { "data": "DESCRIPCION", "title": "Prioridades Anuales", "width": "20%" },
                 
                        { "data": "NOMBRE_EVALUADO", "title": "Programacion" },
                        { "data": "NOMBRE_EVALUADOR", "title": "Programacionr" },
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
                            'className': 'dt-body-center',
                            "render": function (data, type, row, meta) {
                                var html = "";
                                html = '<a href="javascript:void(0);" ><i class="icon icon-edit icon-lg m-c-sm findone_mantenimiento" secuencial="' + row.INDICE + '" idpk_M="' + row.ID_PROYECTO + '"></i></a>';
                                return html;
                            }
                        },
                        {
                            'targets': 1,
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-center',
                            'render': function (data, type, row, meta) {
                                var html = "";
                                html = '<a href="javascript:void(0);" ><i class="icon icon-remove icon-lg m-c-sm delete_mantenimiento" idpk="' + row.ID_PROYECTO + '" idpki="' + meta.ID_PROYECTO + '"></i></a>';
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