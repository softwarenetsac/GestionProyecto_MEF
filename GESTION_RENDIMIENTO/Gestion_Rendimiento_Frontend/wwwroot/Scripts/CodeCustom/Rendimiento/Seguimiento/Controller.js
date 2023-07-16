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

        };

        base.Parameters = {
            TableSeguimiento: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            BotonExportar: function () { return $("#btnExportar"); },
            GridSeguimiento: function () { return $("#gridSeguimiento"); },
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
                        { "data": "ID_PROYECTO", },
                        { "data": "DESCRIPCION", "title": "Proyecto","width": "20%" },
                        { "data": "ANIO", "title": "Año", "class": "text-center" },
                        { "data": "NOMBRE_EVALUADO", "title": "Evaluado" },
                        { "data": "NOMBRE_EVALUADOR", "title": "Evaluador" },
                        { "data": "PLAZO", "title": "Plazo", "class": "text-center" },
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
                                html += '<input class="custom-control-input custom-control-input-danger custom-control-input-outline chkProyecto_"  type="checkbox"  name="chkProyecto" id=chk_' + row.ID_PROYECTO + ' style="position:unset;opacity:100" value=' + row.ID_PROYECTO + '>';
                                return html;
                            }
                        },
                    ],
                });
            },
            BuscarGrilla: function () {
                base.Ajax.AjaxBuscar.data = {
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