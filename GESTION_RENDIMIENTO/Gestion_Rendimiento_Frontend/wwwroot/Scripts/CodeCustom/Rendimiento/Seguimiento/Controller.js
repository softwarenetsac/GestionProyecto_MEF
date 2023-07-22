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
        };

        base.Parameters = {
            TableSeguimiento: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            BotonExportar: function () { return $("#btnExportar"); },
            GridSeguimiento: function () { return $("#gridSeguimiento"); },
            DllOrgano: function () { return $("#ID_AREA_CONSULTA"); },
            DllUnidadOrganica: function () { return $("#ID_OFICINA_CONSULTA"); },
            ModalSeguimiento: function () { return $("#modalSeguimiento"); },
            hdnIdPersonal: function () { return $("#hdnIdPersonal"); },
            hdnAnio: function () { return $("#hdnAnio"); },
        };

        base.Event = {
            BotonBuscarClick: function () {
                base.Function.BuscarGrilla();
            },
            BotonExportarClick: function () {

                //var id_area = base.Control.FrmConsulta.DllOrgano().val();
                //var id_oficina = base.Control.FrmConsulta.DllUnidadOrganica().val();

                //if ((id_area == undefined || id_area == "" || id_area == null)) {
                //    id_area = 0;
                //}
                //if ((id_oficina == undefined || id_oficina == "" || id_oficina == null)) {
                //    id_oficina = 0;
                //}
                $.paramcustom = {
                    url: SoftwareNet.Web.Operacion.Seguimiento.Actions.ExportarExcel,
                    values: {
                        //ID_AREA_JEFE: parseInt(id_area),
                        //ID_OFICINA_JEFE: parseInt(id_oficina),
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
        };
        base.Ajax = {
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Seguimiento.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
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