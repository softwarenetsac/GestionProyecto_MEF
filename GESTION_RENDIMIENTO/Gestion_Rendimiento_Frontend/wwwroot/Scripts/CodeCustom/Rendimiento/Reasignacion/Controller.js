try {
    ns('SoftwareNet.Web.Operacion.Reasignacion.Registro.Controller');
    SoftwareNet.Web.Operacion.Reasignacion.Registro.Controller = function () {
        var base = this;
        base.Ini = function (opts) {
            'use strict';
            base.Control.DllOrgano().select2({});
            base.Control.DllUnidadOrganica().select2({});
            base.Control.DllProgramacionr().select2({});

            base.Control.DllAreaReas().select2({});
            base.Control.DllOficinaReas().select2({});
            base.Control.DllEvaluadoReas().select2({});
            base.Control.DllAreaReas().change(base.Event.DllOrganoChange);
            base.Control.DllOficinaReas().change(base.Event.DllOficinaChange);
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
            base.Control.BotonExportar().click(base.Event.BotonExportarClick);
            base.Function.CrearGrillaEvaluado();
            base.Function.BuscarGrilla();
            base.Control.GridEvaluado().on('click', '.findone_mantenimiento', function (e) {
                var id = $(e.target).attr("idpk_M");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.AbrirModalRendimiento(id, this);
            });
            base.Control.GridEvaluado().on('click', '#chkAllSelection', function (e) {
                // debugger;

                $('#gridEvaluado tbody input[type="checkbox"]').prop('checked', this.checked);
                $('#gridEvaluado tbody tr').removeClass("odd selected check");
                if (this.checked) {
                    $('#gridEvaluado tbody tr input[type="checkbox"').parent().parent().parent().addClass('odd selected check');

                } else {
                    $('#gridEvaluado tbody tr').addClass("odd");
                }
            });
            base.Control.BotonReasignar().click(base.Event.BotonAgregarReasignacionClick);
            base.Control.BotonGuardarReasignar().click(base.Event.BotonGuardarReasignacionClick);
        };

        base.Parameters = {
            TableEvaluado: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            BotonExportar: function () { return $("#btnExportar"); },
            ModalAgregarRendimiento: function () { return $("#modalRendimiento"); },
            ModalAgregarReasignacion: function () { return $("#modalReasignacion"); },
            GridEvaluado: function () { return $("#gridEvaluado"); },
            DllOrgano: function () { return $("#ID_AREA_CONSULTA"); },
            DllUnidadOrganica: function () { return $("#ID_OFICINA_CONSULTA"); },
            DllProgramacionr: function () { return $("#ID_EVALUADOR"); },
            hdnIdProyecto: function () { return $("#hdnIdProyecto"); },
            DllAnioConsulta: function () { return $("#ddlAnio_Consulta"); },
            BotonReasignar: function () { return $("#btnReasignar"); },
            DllAreaReas: function () { return $("#ID_AREA_REAS"); },
            DllOficinaReas: function () { return $("#ID_OFICINA_REAS"); },
            DllEvaluadoReas: function () { return $("#ID_EVALUADO_REAS"); },
            BotonGuardarReasignar: function () { return $("#btnReasignarGuardar"); },
            FormReasignacion: function () { return $("#frmReasignacion"); },
            
        };

        base.Event = {
            DllOrganoChange: function () {

                base.Control.DllOficinaReas().find('option').remove();
                base.Control.DllOficinaReas().append("<option value='' selected>" + "---Todos--" + "</option>");
                var id_oficina = base.Control.DllAreaReas().val();

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
            DllOficinaChange: function () {

                base.Control.DllEvaluadoReas().find('option').remove();
                base.Control.DllEvaluadoReas().append("<option value='' selected>" + "---Todos--" + "</option>");
                var id_area = base.Control.DllAreaReas().val();
                var id_oficina = base.Control.DllOficinaReas().val();

                if ((id_oficina == null || id_oficina == "0")) {
                    id_oficina = "0";
                }
                if (id_oficina != "0") {
                    base.Ajax.AjaxConsultarEvaluado.data = {
                        ID_AREA: parseInt(id_area),
                        ID_OFICINA: parseInt(id_oficina),
                    }
                    base.Ajax.AjaxConsultarEvaluado.submit();
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
                    url: SoftwareNet.Web.Operacion.Reasignacion.Actions.ExportarExcel,
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
                        base.Control.DllOficinaReas().append($("<option>", { value: item.ID_OFICINA, text: item.NOMBRE_OFICINA }));
                    })
                }
            },
            AjaxConsultarEvaluadoSuccess: function (data) {
                if (data) {
                    $.each(data.Result, function (index, item) {
                        base.Control.DllEvaluadoReas().append($("<option>", { value: item.ID_PERSONAL, text: item.NOMBRE_COMPLETO }));
                    })
                }
            },
            BotonAgregarReasignacionClick: function () {
                base.Function.AbrirModalReasignacion();
            },
            BotonGuardarReasignacionClick: function () {
                var form = base.Control.FormReasignacion();
                form.validate();
                if (!form.valid()) { return false; }
                else {

                    base.Control.Mensaje.Confirmation({
                        message: "¿Esta seguro que desea realizar la asignación ?",
                    }).ConfirmationAcept({
                        callback: function (opt) {
                            if (opt) {
                                var dataRequest = new Array();
                                $('.chkProyecto:checked').each(
                                    function () {
                                        var objdata = {
                                            ID_PROYECTO: parseInt($(this).val()),
                                        };
                                        dataRequest.push(objdata);
                                    }
                                );
                     

                                base.Ajax.AjaxGuardarReasignar.data = {
                                    ItemsProyecto: dataRequest,
                                    ID_PERSONAL: base.Control.DllEvaluadoReas().val(),
                                }
                                base.Ajax.AjaxGuardarReasignar.submit();
                            }
                        }
                    });


                }
            },
            AjaxGuardarReasignarSuccess: function (data) {
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaGuardoExito })
                        .InformationClose({
                            callback: function (opt) {

                                if (opt) {
                                    base.Function.BuscarGrilla();
                                    base.Control.ModalAgregarReasignacion().modal("hide");
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
            AjaxConsultarUnidadOrganica: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Reasignacion.Actions.BuscarUnidadOrganica,
                autoSubmit: false,
                onSuccess: base.Event.AjaxConsultarUnidadOrganicaSuccess
            }),
            AjaxConsultarEvaluado: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Reasignacion.Actions.BuscarPersonalOficina,
                autoSubmit: false,
                onSuccess: base.Event.AjaxConsultarEvaluadoSuccess
            }),
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Reasignacion.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
            }),
            AjaxGuardarReasignar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Reasignacion.Actions.ReasignarEvaluado,
                autoSubmit: false,
                onSuccess: base.Event.AjaxGuardarReasignarSuccess
            }),
        };
        base.Function = {
            CrearGrillaEvaluado: function () {
                General.configurarGrilla(true);
                var chk = "Sel.";
                chk += "</br>";
                chk += '<label class="custom-control custom-control-primary custom-checkbox">';
                chk += '<input class="custom-control-input" type="checkbox" name="chkAllSelection" id=chkAllSelection >';
                chk += '<span class="custom-control-indicator" style="border-color: #d9230f;"></span>';
                chk += '</label>';
                base.Parameters.TableEvaluado = base.Control.GridEvaluado().DataTable({
                    ordering: false,
                    select: true,
                    search:false,
                    columns: [
                        { "data": "", "title": chk, "class": "text-center", 'orderable': false },
                        { "data": "", "title": "Detalle", "class": "text-center" },
                        { "data": "ID_PROYECTO", "visible": false },
                        { "data": "ANIO", "title": "Año", "class": "text-center" },
                        { "data": "DESCRIPCION", "title": "Prioridades Anuales", "width": "20%" },
                        { "data": "NOMBRE_EVALUADO", "title": "Evaluado" },
                        { "data": "NOMBRE_OFICINA", "title": "Oficina", "width": "20%" },
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
                                html += '<label class="custom-control custom-control-primary custom-checkbox">';
                                html += '<input class="custom-control-input chkProyecto" type="checkbox"  name=chk_' + row.ID_PROYECTO + ' id=chk_' + row.ID_PROYECTO + ' value=' + row.ID_PROYECTO + '  >';
                                html += '<span class="custom-control-indicator" style="border-color: #d9230f;font-size:20px;"></span>';
                                html += '</label>';
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
                var url = SoftwareNet.Web.Operacion.Reasignacion.Actions.NuevaFila;
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
            AbrirModalReasignacion: function () {
                var indexes = 0;
                $('.chkProyecto:checked').each(
                    function () {
                        indexes = 1;
                    }
                );
                base.Control.DllAreaReas().val(0);
                base.Control.DllAreaReas().trigger("change"); 
                base.Control.DllOficinaReas().val(0);
                base.Control.DllOficinaReas().trigger("change"); 
                base.Control.DllEvaluadoReas().val(0);
                base.Control.DllEvaluadoReas().trigger("change");
                if (indexes>0) {
                    base.Control.ModalAgregarReasignacion().modal('show');
                } else {
                    base.Control.Mensaje.Warning({ message: "Seleccione como mínimo un registro."}).WarningClose();
                }
            
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