try {
    ns('SoftwareNet.Web.Operacion.RendimientoORH.Registro.Controller');
    SoftwareNet.Web.Operacion.RendimientoORH.Registro.Controller = function () {
        var base = this;
        base.Ini = function (opts) {
            'use strict';
            base.Control.DllOrgano().select2({});
            base.Control.DllUnidadOrganica().select2({});
            base.Control.DllProgramacionr().select2({});
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
            base.Control.BotonExportar().click(base.Event.BotonExportarClick);
            base.Function.CrearGrillaEvaluado();
            base.Function.BuscarGrilla();
            //base.Control.DllOrgano().change(base.Event.DllOrganoChange);
            base.Control.GridEvaluado().on('click', '.findone_mantenimiento', function (e) {
                var id = $(e.target).attr("idpk_M");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.AbrirModalRendimiento(id, this);
            });
        };

        base.Parameters = {
            TableEvaluado: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            BotonExportar: function () { return $("#btnExportar"); },
            ModalAgregarRendimiento: function () { return $("#modalRendimiento"); },
            GridEvaluado: function () { return $("#gridEvaluado"); },
            DllOrgano: function () { return $("#ID_AREA_CONSULTA"); },
            DllUnidadOrganica: function () { return $("#ID_OFICINA_CONSULTA"); },
            DllProgramacionr: function () { return $("#ID_EVALUADOR"); },
            hdnIdProyecto: function () { return $("#hdnIdProyecto"); },
            DllAnioConsulta: function () { return $("#ddlAnio_Consulta"); },
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
                var anio = base.Control.DllAnioConsulta().val();
                if ((id_area == undefined || id_area == "" || id_area == null)) {
                    id_area = 0;
                }
                if ((id_oficina == undefined || id_oficina == "" || id_oficina == null)) {
                    id_oficina = 0;
                }

                $.paramcustom = {
                    url: SoftwareNet.Web.Operacion.RendimientoORH.Actions.ExportarExcel,
                    values: {
                        ID_AREA: parseInt(id_area),
                        ID_OFICINA: parseInt(id_oficina),
                        ANIO: anio,
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
                action: SoftwareNet.Web.Operacion.RendimientoORH.Actions.BuscarUnidadOrganica,
                autoSubmit: false,
                onSuccess: base.Event.AjaxConsultarUnidadOrganicaSuccess
            }),
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.RendimientoORH.Actions.Buscar,
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
                        { "data": "", "title": "Detalle", "class": "text-center" },
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
                            'className': 'dt-body-center',
                            "render": function (data, type, row, meta) {
                                var html = "";
                                html = '<a href="javascript:void(0);" ><i class="icon icon-search icon-lg m-c-sm findone_mantenimiento" secuencial="' + row.INDICE + '" idpk_M="' + row.ID_PROYECTO + '"></i></a>';
                                return html;
                            }
                        },
                    ],
                });
            },
            BuscarGrilla: function () {
                var id_area = base.Control.DllOrgano().val();
                var id_oficina = base.Control.DllUnidadOrganica().val();
                var anio = base.Control.DllAnioConsulta().val();
                if ((id_area == undefined || id_area == "" || id_area == null)) {
                    id_area = 0;
                }
                if ((id_oficina == undefined || id_oficina == "" || id_oficina == null)) {
                    id_oficina = 0;
                }
                base.Ajax.AjaxBuscar.data = {
                    ID_AREA: parseInt(id_area),
                    ID_OFICINA: parseInt(id_oficina),
                    ANIO:anio,
                }
                base.Ajax.AjaxBuscar.submit();
            },
            AbrirModalRendimiento: function (id, parent) {
                base.Control.DllProgramacionr().val('');
                base.Control.DllProgramacionr().trigger("change");
                base.Control.hdnIdProyecto().val(0);
                if (id > 0) {
                    var padre = $(parent).parent().parent();
                    var indice = base.Parameters.TableEvaluado.row(padre).index();
                    var data = base.Parameters.TableEvaluado.row(indice).data();
                    if (data != null) {
                        base.Control.DllProgramacionr().val(data.ID_EVALUADOR);
                        base.Control.DllProgramacionr().trigger("change");
                    }
                    base.Control.hdnIdProyecto().val(id);
                    base.Function.AgregarRegistroPrioridad(id);
                    $('.btn_agr').hide(); 
                    $('.AddRegDet').hide();
                    $('.delete_mantenimiento').hide();
                    
                } 
                base.Control.ModalAgregarRendimiento().modal('show');
            },
            AgregarRegistroPrioridad: function (id) {
                var url = SoftwareNet.Web.Operacion.RendimientoORH.Actions.NuevaFila;
                var item = {
                    ItemsProyecto: null,
                    ItemsPrioridad: null,
                    ID_PROYECTO: base.Control.hdnIdProyecto().val(),
                    TIPO: "E",
                    ID_DETALLE_TEMP: parseInt(0),
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