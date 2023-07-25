try {
    ns('SoftwareNet.Web.Operacion.Seguimiento.Registro.Controller');
    SoftwareNet.Web.Operacion.Seguimiento.Registro.Controller = function () {
        var base = this;
        base.Ini = function (opts) {
            'use strict';
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
            base.Control.BotonExportar().click(base.Event.BotonExportarClick);
            base.Function.CrearGrillaSeguimiento();
            base.Function.BuscarGrilla();
            base.Control.GridSeguimiento().on('click', '.findone_Actividad', function (e) {
                debugger;
                var ID_PERSONAL = $(e.target).attr("idpk_per");
                var ANIO = $(e.target).attr("idpk_anio");
                if (typeof ID_PERSONAL !== 'undefined' && ID_PERSONAL > 0)
                    base.Function.AbrirModalSeguimiento(ID_PERSONAL, ANIO);
            });
            base.Control.GridBody().on('click', '.mantenimiento_seguimiento', function (e) {
                var elemento = this;
                debugger;
                var id = elemento.getAttribute("idpk_p");
                if (typeof id !== 'undefined')
                    base.Function.AbrirModalManSegui(id);
            });
            base.Control.GridSeguimientoRegistro().on('click', '.findone_DeleteSeguimiento', function (e) {
                var elemento = this;
                debugger;
                var id = elemento.getAttribute("idpk_Seg");
                if (typeof id !== 'undefined')
                    base.Function.DeleteSegRegistro(id);
            });
            base.Control.BotonRegistrarSeg().click(base.Event.BotonRegistrarSegClick);
            base.Function.CrearGrillaSeguimientoRegistro();
        };

        base.Parameters = {
            TableSeguimiento: null,
            TableSeguimientoRegistro: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonRegistrarSeg: function () { return $("#btnRegistrarSeg"); },
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            BotonExportar: function () { return $("#btnExportar"); },
            GridSeguimiento: function () { return $("#gridSeguimiento"); },
            DllOrgano: function () { return $("#ID_AREA_CONSULTA"); },
            DllUnidadOrganica: function () { return $("#ID_OFICINA_CONSULTA"); },
            ModalSeguimiento: function () { return $("#modalSeguimiento"); },
            modalRegistroSeg: function () { return $("#modalRegistroSeg"); },
            hdnIdPersonal: function () { return $("#hdnIdPersonal"); },
            hdnAnio: function () { return $("#hdnAnio"); },
            hdnIdProyecto: function () { return $("#hdnIdProyecto"); },
            GridBody: function () { return $("#gridBody"); },
            FormRegSeguimiento: function () { return $("#frmModelRegSeguimiento"); },
            DllTipoNivel: function () { return $("#ID_TIPO_NIVEL"); },
            txtDescSeguimiento: function () { return $("#TXT_DES_SEGUIMIENTO"); },
            GridSeguimientoRegistro: function () { return $("#gridSeguimientoRegistro"); },
            
        };

        base.Event = {
            BotonBuscarClick: function () {
                base.Function.BuscarGrilla();
            },
            BotonExportarClick: function () {
                $.paramcustom = {
                    url: SoftwareNet.Web.Operacion.Seguimiento.Actions.ExportarExcel,
                    values: {
                    }
                }
                $.redirect();

            },
            AjaxBuscarSuccess: function (data) {

                if (data.Result) {
                    base.Parameters.TableAutorizador.clear().draw();
                    base.Parameters.TableAutorizador.rows.add(data.Result).draw();
                }
            },
            BotonRegistrarSegClick: function () {
                var form = base.Control.FormRegSeguimiento();
                form.validate();
                if (!form.valid()) { return false; }
                else {

                    base.Control.Mensaje.Confirmation({
                        message: "¿Estás seguro de realizar el registro?",
                    }).ConfirmationAcept({
                        callback: function (opt) {
                            if (opt) {
                                var ID_SEGUIMIENTO = 0;
                                var ID_PROYECTO = base.Control.hdnIdProyecto().val();
                                var DETALLE_NOTA = base.Control.txtDescSeguimiento().val();
                                var ID_TIPO_NIVEL = base.Control.DllTipoNivel().val();
                                debugger;
                                base.Ajax.AjaxRegistrarSeg.data = {
                                    ID_SEGUIMIENTO: parseInt(ID_SEGUIMIENTO),
                                    ID_PROYECTO: parseInt(ID_PROYECTO),
                                    DETALLE_NOTA: DETALLE_NOTA,
                                    ID_TIPO_NIVEL: parseInt(ID_TIPO_NIVEL),
                                }
                                base.Ajax.AjaxRegistrarSeg.submit();
                            }
                        }
                    });


                }
            },
            AjaxGuardarSeguimientoSuccess: function (data) {
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaGuardoExito })
                        .InformationClose({
                            callback: function (opt) {

                                if (opt) {
                                    base.Control.txtDescSeguimiento().val('');
                                    base.Control.DllTipoNivel().val('');
                                    base.Ajax.AjaxBuscarSeguimientoRegistro.data = {
                                        ID_PROYECTO: parseInt(base.Control.hdnIdProyecto().val()),
                                    }
                                    base.Ajax.AjaxBuscarSeguimientoRegistro.submit();
                                }
                            }
                        });
                }
                else {
                    base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                }
            },
            AjaxBuscarSeguimientoRegistroSuccess: function (data) {

                if (data.Result) {
                    base.Parameters.TableSeguimientoRegistro.clear().draw();
                    base.Parameters.TableSeguimientoRegistro.rows.add(data.Result).draw();
                }
            },
            AjaxDeleteSeguimientoSuccess: function (data) {
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: "El registro se eliminó correctamente." })
                        .InformationClose({
                            callback: function (opt) {

                                if (opt) {
                                    base.Control.txtDescSeguimiento().val('');
                                    base.Control.DllTipoNivel().val('');
                                    base.Ajax.AjaxBuscarSeguimientoRegistro.data = {
                                        ID_PROYECTO: parseInt(base.Control.hdnIdProyecto().val()),
                                    }
                                    base.Ajax.AjaxBuscarSeguimientoRegistro.submit();
                                }
                            }
                        });
                }
                else {
                    base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                }
            },
        };
        base.Ajax = {
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Seguimiento.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
            }),
            AjaxRegistrarSeg: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Seguimiento.Actions.GuardarSeguimiento,
                autoSubmit: false,
                onSuccess: base.Event.AjaxGuardarSeguimientoSuccess
            }),
            AjaxBuscarSeguimientoRegistro: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Seguimiento.Actions.BuscarSeguimientoProyecto,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSeguimientoRegistroSuccess
            }),
            AjaxDeleteSegRegistro: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Seguimiento.Actions.DeleteSeguimiento,
                autoSubmit: false,
                onSuccess: base.Event.AjaxDeleteSeguimientoSuccess
            }),
        };
        base.Function = {
            CrearGrillaSeguimiento: function () {
                General.configurarGrilla();
                base.Parameters.TableAutorizador = base.Control.GridSeguimiento().DataTable({
                    ordering: false,
                    select: true,
                    columns: [
                        { "data": "ID_PERSONAL", "visible": false },
                        { "data": "NOMBRE_COMPLETO", "title": "Apellidos y Nombres","width": "20%" },
                        { "data": "NUMERO_DNI", "title": "N° Documento", "class": "text-center" },
                        { "data": "ANIO", "title": "Año", "class": "text-center" },
                        { "data": "NUM_ACTIVIDADES", "title": "N° de Actividades </br> Anuales", "class": "text-center" },
                        { "data": "FECHA_INGRESO", "title": "F. Ingreso" },
                        { "data": "NOMBRE_CARGO", "title": "Puesto" },
                        { "data": "NOMBRE_OFICINA", "title": "Unidad Orgánica", "class": "text-center" },
                        { "data": "ID_OFICINA", "visible": false },
                        { "data": "ID_PERSONAL", "visible": false },
                        //{ "data": "ID_ESTADO", "visible": false },
                        { "data": "ID_EVALUADOR", "visible": false },
                    ],
                    "pageLength": 30,
                    "columnDefs": [
                        {
                            'targets': 4,
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-center',
                            'render': function (data, type, row, meta) {
                                var html = ""
                                if (row.NUM_ACTIVIDADES > 0) {
                                    html += '<a href="javascript:void(0);" ><span class="label arrow-left arrow-primary findone_Actividad" secuencial="' + row.INDICE + '" idpk_per="' + row.ID_PERSONAL + '" idpk_anio="' + row.ANIO + '">' + row.NUM_ACTIVIDADES + '</span></a>';
                                }
                                else {
                                    html += '<span class="label arrow-left arrow-info">' + row.NUM_ACTIVIDADES + '</span>';


                                }


                                return html;
                            }
                        },
                    ],
                });
            },
            BuscarGrilla: function () {
                var id_area = base.Control.DllOrgano().val();
                var id_oficina = base.Control.DllUnidadOrganica().val();
                if ((id_area == undefined || id_area == "" || id_area == null)) {
                    id_area = 0;
                }
                if ((id_oficina == undefined || id_oficina == "" || id_oficina == null)) {
                    id_oficina = 0;
                }
                base.Ajax.AjaxBuscar.data = {
                    ID_AREA: parseInt(id_area),
                    ID_OFICINA: parseInt(id_oficina),
                }
                base.Ajax.AjaxBuscar.submit();
            },
            AbrirModalSeguimiento: function (id_personal, anio) {
                    base.Control.hdnIdPersonal().val(id_personal);
                    base.Control.hdnAnio().val(anio);
                base.Function.RegistrosProyecto(id_personal, anio);
                base.Control.ModalSeguimiento().modal('show');
            },
            AbrirModalManSegui: function (id_proyect) {
             base.Control.hdnIdProyecto().val('');
                base.Control.txtDescSeguimiento().val('');
                base.Control.DllTipoNivel().val('');

                base.Control.hdnIdProyecto().val(id_proyect);
                base.Ajax.AjaxBuscarSeguimientoRegistro.data = {
                    ID_PROYECTO: parseInt(id_proyect),
                }
                base.Ajax.AjaxBuscarSeguimientoRegistro.submit();
                base.Control.modalRegistroSeg().modal('show');
            },
            RegistrosProyecto: function (ID_PERSONAL,ANIO) {
                var url = SoftwareNet.Web.Operacion.Seguimiento.Actions.NuevaFila;
                var item = {
                    ANIO: ANIO,
                    ID_PERSONAL: ID_PERSONAL,
                }
                var respuesta = General.POST(url, item, false);
                var row = respuesta.Extra;
                $("#gridBody").html(row);

            },
            CrearGrillaSeguimientoRegistro: function () {
                General.configurarGrilla();
                base.Parameters.TableSeguimientoRegistro = base.Control.GridSeguimientoRegistro().DataTable({
                    ordering: false,
                    select: true,
                    columns: [
                        { "data": "ID_SEGUIMIENTO", "title": "Eliminar", "class": "text-center" },
                        { "data": "DES_PROYECTO", "title": "Proyecto", "width": "20%", "visible": false },
                        { "data": "DETALLE_NOTA", "title": "Detalle"},
                        { "data": "EVALUADOR", "title": "Evaluador"},
                        { "data": "DES_NIVEL", "title": "Nivel de Logro", "class": "text-center" },
                        { "data": "ID_SEGUIMIENTO", "visible": false },
                        { "data": "ID_PROYECTO", "visible": false },
                        { "data": "ID_EVALUADOR", "visible": false },
                        { "data": "ID_ARCHIVO", "visible": false },
                        { "data": "ID_TIPO_NIVEL", "visible": false },
                        
                    ],
                    "pageLength": 30,
                    "columnDefs": [
                        {
                            'targets': 0,
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-center',
                            'render': function (data, type, row, meta) {
                                var html = ""
                                html += '<a href="javascript:void(0);" ><span class="icon icon-remove icon-lg  findone_DeleteSeguimiento" secuencial="' + row.INDICE + '" idpk_Seg="' + row.ID_SEGUIMIENTO + '"> </span></a>';
                                return html;
                            }
                        },
                    ],
                });
            },
            DeleteSegRegistro: function (ID_SEGUIMIENTO) {

                base.Control.Mensaje.Confirmation({
                    message: "¿Estás seguro que desea eliminar el registro?",
                }).ConfirmationAcept({
                    callback: function (opt) {
                        if (opt) {
                            base.Ajax.AjaxDeleteSegRegistro.data = {
                                ID_SEGUIMIENTO: parseInt(ID_SEGUIMIENTO),
                            }
                            base.Ajax.AjaxDeleteSegRegistro.submit();
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