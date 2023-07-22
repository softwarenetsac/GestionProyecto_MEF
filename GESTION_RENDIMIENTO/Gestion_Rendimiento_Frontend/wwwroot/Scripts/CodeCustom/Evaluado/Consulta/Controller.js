try {
    ns('SoftwareNet.Web.Operacion.Evaluado.Registro.Controller');
    SoftwareNet.Web.Operacion.Evaluado.Registro.Controller = function () {
        var base = this;
        base.Ini = function (opts) {
            'use strict';
            base.Control.DllOrgano().select2({});
            base.Control.DllUnidadOrganica().select2({});
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
            base.Control.BotonExportar().click(base.Event.BotonExportarClick);
            base.Function.CrearGrillaEvaluado();
            base.Function.BuscarGrilla();
            base.Control.DllOrgano().change(base.Event.DllOrganoChange);
        };

        base.Parameters = {
            TableEvaluado: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            BotonExportar: function () { return $("#btnExportar"); },
            GridEvaluado: function () { return $("#gridEvaluado"); },
            DllOrgano: function () { return $("#ID_AREA_CONSULTA"); },
            DllUnidadOrganica: function () { return $("#ID_OFICINA_CONSULTA"); },
        };

        base.Event = {
            DllOrganoChange: function () {

                base.Control.DllUnidadOrganica().find('option').remove();
                base.Control.DllUnidadOrganica().append("<option value='' selected>" + "---Todos--" + "</option>");
                var id_oficina = base.Control.DllOrgano().val();

                if ((id_oficina == null || id_oficina == "0")) {
                    id_oficina = "0";
                }
                if (id_oficina != "0") {
                    base.Ajax.AjaxConsultarUnidadOrganica.data = {
                        ID: id_oficina
                    }
                    base.Ajax.AjaxConsultarUnidadOrganica.submit();
                }
            },
            BotonBuscarClick: function () {
                base.Function.BuscarGrilla();
            },
            BotonExportarClick: function () {

            var id_area = base.Control.DllOrgano().val();
             var id_oficina = base.Control.DllUnidadOrganica().val();

                if ((id_area == undefined || id_area == "" || id_area == null)) {
                    id_area = 0;
                }
                if ((id_oficina == undefined || id_oficina == "" || id_oficina == null)) {
                    id_oficina = 0;
                }
                $.paramcustom = {
                    url: SoftwareNet.Web.Operacion.Evaluado.Actions.ExportarExcel,
                    values: {
                        ID_AREA: parseInt(id_area),
                        ID_OFICINA: parseInt(id_oficina),
                    }
                }
                $.redirect();

            },
            AjaxBuscarSuccess: function (data) {

                if (data.Result) {
                    base.Parameters.TableEvaluado.clear().draw();
                    base.Parameters.TableEvaluado.rows.add(data.Result).draw();
                }
            },
            AjaxConsultarUnidadOrganicaSuccess: function (data) {
                if (data) {
                    $.each(data.Result, function (index, item) {
                        base.Control.DllUnidadOrganica().append($("<option>", { value: item.ID_OFICINA, text: item.NOMBRE_OFICINA }));
                    })
                }
            },
        };
        base.Ajax = {
            AjaxConsultarUnidadOrganica: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Evaluado.Actions.BuscarUnidadOrganica,
                autoSubmit: false,
                onSuccess: base.Event.AjaxConsultarUnidadOrganicaSuccess
            }),
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Evaluado.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
            }),
        };
        base.Function = {
            CrearGrillaEvaluado: function () {
                General.configurarGrilla(true);
                base.Parameters.TableEvaluado = base.Control.GridEvaluado().DataTable({
                    ordering: false,
                    select: true,
                    search:true,
                    columns: [
                        { "data": "NOMBRE_COMPLETO", "title": "Apellidos y Nombres" },
                        { "data": "NUMERO_DNI", "title": "N° Documento" },
                        { "data": "FECHA_INGRESO", "title": "F. Ingreso" },
                        { "data": "NOMBRE_CARGO", "title": "Puesto" },
                        { "data": "NOMBRE_CATEGORIA", "title": "Categoría" },
                        { "data": "CORREO_INSTITUCIONAL", "title": "Correo" },
                        { "data": "NOMBRE_OFICINA", "title": "Unidad Orgánica" },
                        { "data": "NOMBRE_AREA", "title": "Órgano" },
                        { "data": "ANIO", "title": "Año" },
                        { "data": "PROYECTO", "title": "Proyecto", "visible": false },
                        { "data": "NOMBRE_EVALUADOR", "title": "Evaluador" },
                        { "data": "ID_PROYECTO", "visible": false },
                        { "data": "ID_EVALUADOR", "visible": false },
                        { "data": "ID_SITUACION_LABORAL", "visible": false },
                        { "data": "ID_OFICINA", "visible": false },
                        { "data": "ID_PERSONAL", "visible": false },
                        { "data": "ID_AREA", "visible": false },
                    ],
                    "pageLength": 30,
                    "columnDefs": [
                        //{
                        //    'targets': 0,
                        //    'searchable': false,
                        //    'orderable': false,
                        //    'render': function (data, type, row, meta) {
                        //        var html = "";
                        //        html += '<input class="custom-control-input custom-control-input-danger custom-control-input-outline chkProyecto_"  type="checkbox"  name="chkProyecto" id=chk_' + row.ID_PROYECTO + ' style="position:unset;opacity:100" value=' + row.ID_PROYECTO + '>';
                        //        return html;
                        //    }
                        //},
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