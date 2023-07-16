try {
    ns('SoftwareNet.Web.Operacion.Evaluador.Registro.Controller');
    SoftwareNet.Web.Operacion.Evaluador.Registro.Controller = function () {
        var base = this;
        base.Ini = function (opts) {
            'use strict';
 
                base.Control.FrmConsulta.DllOrgano().select2({});
                base.Control.FrmConsulta.DllUnidadOrganica().select2({});

                base.Control.FrmRegistroAutorizador.DllOrgano().select2({});
                base.Control.FrmRegistroAutorizador.DllUnidadOrganica().select2({});
              //  base.Control.FrmRegistroAutorizador.DdlGrupoAsistencia().select2({});
                base.Control.FrmRegistroAutorizador.ddlTitular().select2({});
              //  base.Control.FrmRegistroAutorizador.ddlAlterno().select2({});
                base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
                base.Control.FrmConsulta.DllOrgano().change(base.Event.DllOrganoChange);
                base.Control.FrmRegistroAutorizador.DllOrgano().change(base.Event.DllOrganoChangeRegistro);
                base.Control.BotonExportar().click(base.Event.BotonExportarClick);
                base.Control.BotonCargaMasivo().click(base.Event.BotonAgregarMasivoClick);
                base.Control.BotonAgregarResponable().click(base.Event.BotonAgregarResponsableClick);
                base.Control.BotonGuardarMasivo().click(base.Event.BotonGuardarMasivoClick);
                base.Control.FrmRegistroAutorizador.BotonGuardar().click(base.Event.BotonGuardarClick);
                base.Control.FrmConsulta.GridAutorizador().on('click', '.delete_mantenimiento', function (e) {
                    var id = $(e.target).attr("idPk");
                    if (typeof id !== 'undefined' && id > 0)
                        base.Function.EliminarMantenimiento(this);
                });

                base.Control.FrmConsulta.GridAutorizador().on('click', '.findone_mantenimiento', function (e) {
                    var id = $(e.target).attr("idPk");
                    if (typeof id !== 'undefined' && id > 0)
                        base.Function.AbrirModalMantenimientoResponsable(id, this);
                });

                base.Function.CrearGrillaAutorizador();
                base.Function.BuscarGrilla();
        };

        base.Parameters = {
            TableAutorizador: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            ModalAgregarMantenimientoMasivo: function () { return $("#modalMantenimientoMasivo"); },
            ModalAgregarMantenimientoResponsable: function () { return $("#modalMantenimientResponsable"); },
            FormAutorizadorRegistro: function () { return $("#frmModelResponsalbe"); },
            FormMantenimientoRegistroMasivo: function () { return $("#frmModelMasivo"); },
            FrmConsulta: {
                DllOrgano: function () { return $("#ID_AREA_CONSULTA"); },
                DllUnidadOrganica: function () { return $("#ID_OFICINA_CONSULTA"); },
                GridAutorizador: function () { return $("#gridAutorizador"); },
            },
            FrmRegistroAutorizador: {
                hdnIdAutorizador: function () { return $("#hdnIdAutorizador"); },
                ddlTitular: function () { return $("#ddlTitular"); },
                BotonGuardar: function () { return $("#btnConfirmarGuardar"); },
                DdlGrupoAsistencia: function () { return $("#ddlGrupoAsistencia"); },
                DllOrgano: function () { return $("#ID_AREA_REGISTRO"); },
                DllUnidadOrganica: function () { return $("#ID_OFICINA_REGISTRO"); }
             },


            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            BotonExportar: function () { return $("#btnExportar"); },
            BotonCargaMasivo: function () { return $("#btnCargaMasivo"); },
            BotonAgregarResponable: function () { return $("#btnAgregarResponable"); },
            BotonGuardarMasivo: function () { return $("#btnGuardarMasivo"); },
            BotonGuardarGrupo: function () { return $("#btnGuardarGrupo"); },
        };

        base.Event = {
            DllOrganoChange: function () {

                base.Control.FrmConsulta.DllUnidadOrganica().find('option').remove();
                base.Control.FrmConsulta.DllUnidadOrganica().append("<option value='' selected>" + "---Todos--" + "</option>");
                var id_oficina = base.Control.FrmConsulta.DllOrgano().val();

                if ((id_oficina == null || id_oficina=="0")) {
                    id_oficina = "0";
                }
                if (id_oficina != "0") {
                    base.Ajax.AjaxConsultarUnidadOrganica.data = {
                        ID: id_oficina
                    }
                    base.Ajax.AjaxConsultarUnidadOrganica.submit();
                }
            },

            DllOrganoChangeRegistro: function ()
            {
                base.Control.FrmRegistroAutorizador.DllUnidadOrganica().find('option').remove();
                base.Control.FrmRegistroAutorizador.DllUnidadOrganica().append("<option value='' selected>" + "--Seleccionar--" + "</option>");
                var id_oficina = base.Control.FrmRegistroAutorizador.DllOrgano().val();
                if ((id_oficina == null || id_oficina == "0" || id_oficina == "")) {
                    id_oficina = 0;
                }
                if (id_oficina>0) {
                    base.Ajax.AjaxConsultarUnidadOrganicaRegistro.data = {
                        ID: id_oficina
                    }
                    base.Ajax.AjaxConsultarUnidadOrganicaRegistro.submit();
                }
            },



   
            BotonBuscarClick: function () {
                base.Function.BuscarGrilla();
            },
            BotonExportarClick: function () {

                var id_area = base.Control.FrmConsulta.DllOrgano().val();
                var id_oficina = base.Control.FrmConsulta.DllUnidadOrganica().val();

                if ((id_area == undefined || id_area == "" || id_area == null)) {
                    id_area = 0;
                }
                if ((id_oficina == undefined || id_oficina == "" || id_oficina == null)) {
                    id_oficina = 0;
                }
                $.paramcustom = {
                    url: SoftwareNet.Web.Operacion.Evaluador.Actions.ExportarExcel,
                    values: {
                        ID_AREA_JEFE: parseInt(id_area),
                        ID_OFICINA_JEFE: parseInt(id_oficina),
                    }
                }
                $.redirect();

            },
            AjaxBuscarSuccess: function (data) {

                if (data.Result) {
                    base.Parameters.TableAutorizador.clear().draw();
                    base.Parameters.TableAutorizador.rows.add(data.Result).draw();

                    //Tabla_Listado.clear().draw();
                    //Tabla_Listado.rows.add(data).draw();
                }
            },
            AjaxConsultarUnidadOrganicaSuccess: function (data) {
                if (data) {
                    $.each(data.Result, function (index, item) {
                        base.Control.FrmConsulta.DllUnidadOrganica().append($("<option>", { value: item.ID_OFICINA, text: item.NOMBRE_OFICINA }));
                    })
                }
            },


            
            AjaxConsultarUnidadOrganicaRegistroSuccess: function (data) {
                if (data != null) {
                    var id_oficina = $("#hdnIdOficina").val();
                   var id_autorizador = base.Control.FrmRegistroAutorizador.hdnIdAutorizador().val();
                    $.each(data.Result, function (index, item) {
                        base.Control.FrmRegistroAutorizador.DllUnidadOrganica().append($("<option>", { value: item.ID_OFICINA, text: item.NOMBRE_OFICINA }));
                        
                        if ((item.ID_OFICINA == id_oficina) && id_autorizador > 0) {
                            base.Control.FrmRegistroAutorizador.DllUnidadOrganica().append($("<option>", { value: item.ID_OFICINA, text: item.NOMBRE_OFICINA, selected: true }));
                        } else {
                            base.Control.FrmRegistroAutorizador.DllUnidadOrganica().append($("<option>", { value: item.ID_OFICINA, text: item.NOMBRE_OFICINA }));
                        }
                        
                    })
                }
            },


            AjaxAutorizadorGuardarSuccess: function (data) {
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaGuardoExito })
                        .InformationClose({
                            callback: function (opt) {

                                if (opt) {
                                    base.Function.BuscarGrilla();
                                    base.Control.ModalAgregarMantenimientoResponsable().modal("hide");
                                }
                            }
                        });
                }
                else {
                    base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                }
            },


      


            BotonAgregarMasivoClick: function () {
                base.Function.AbrirModalMantenimientoMasivo();
            },
            BotonAgregarResponsableClick: function () {
                base.Function.AbrirModalMantenimientoResponsable();
            },
 
            BotonGuardarMasivoClick: function () {
                var form = base.Control.FormMantenimientoRegistroMasivo();
                form.validate();
                if (!form.valid()) { return false; }
                else {
                    base.Control.Mensaje.Confirmation({
                        message: "Esta seguro de registrar el Responsable ?",
                    }).ConfirmationAcept({
                        callback: function (opt) {1
                            if (opt) {
                                base.Ajax.AjaxMantenimientoGuardarForm.submitForm();
                            }
                        }
                    });
                }
            },
            AjaxMantenimientoGuardarFormSuccess: function (data) {
                base.Function.BuscarGrilla();
                if (data.Success) {
                    base.Control.Mensaje.Information({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaGuardoExito })
                        .InformationClose({
                            callback: function (opt) {
                                if (opt) {
                                    base.Control.ModalAgregarMantenimientoMasivo().modal("hide");
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

                    base.Control.Mensaje.Information({ message: data.Message });
                    base.Control.Mensaje.InformationClose({
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

       
      
            BotonGuardarClick: function () {


                var form = base.Control.FormAutorizadorRegistro();
                form.validate();
                if (!form.valid()) { return false; }
                else {

                    base.Control.Mensaje.Confirmation({
                        message: "¿Estás seguro de realizar la transacción?",
                    }).ConfirmationAcept({
                        callback: function (opt) {
                            if (opt) {
                                var id_oficina = base.Control.FrmRegistroAutorizador.DllUnidadOrganica().val();
                                var id_area = base.Control.FrmRegistroAutorizador.DllOrgano().val();
                                var id_autorizador = base.Control.FrmRegistroAutorizador.hdnIdAutorizador().val();
                                if ((id_autorizador == null || id_autorizador == "" || id_autorizador == undefined)) {
                                    id_autorizador = 0;
                                }

                                if ((id_area == null || id_area == "" || id_area == undefined)) {
                                    id_area = "0";
                                }
                                if ((id_oficina == null || id_oficina == "" || id_oficina == undefined)) {
                                    id_oficina = "0";
                                }
                 
                                base.Ajax.AjaxAutorizadorGuardar.data = {
                                    ID_EVALUADOR: parseInt(id_autorizador),
                                    FLG_AUTORIZADOR: "1",
                                    ID_PERSONA_JEFE: base.Control.FrmRegistroAutorizador.ddlTitular().val(),
                                    ID_PERSONA_ALTERNO: "",
                                    ID_AREA_JEFE: id_area,
                                    ID_OFICINA_JEFE: id_oficina,
                                    FECHA_INICIO_JEFE_CADENA: "",
                                    FECHA_FINAL_JEFE_CADENA: "",
                                    FECHA_INICIO_ALTERNO_CADENA: "",
                                    FECHA_FINAL_ALTERNO_CADENA: "",
                                    FLG_INDEFINADO_ALTERNO: "",
                                    FLG_INDEFINADO_JEFE: "",
                                    FECHA_DOCUMENTO_JEFE_CADENA: "",
                                    NUMERO_DOCUMENTO_JEFE: "",
                                    FLG_TIPO: "1",
                                    ID_GRUPO: 0

                                }
                                base.Ajax.AjaxAutorizadorGuardar.submit();
                            }
                        }
                    });


                }
            },


        };
        base.Ajax = {
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Evaluador.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
            }),
            AjaxConsultarUnidadOrganica: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Evaluador.Actions.BuscarUnidadOrganica,
                autoSubmit: false,
                onSuccess: base.Event.AjaxConsultarUnidadOrganicaSuccess
            }),
  

            AjaxConsultarUnidadOrganicaRegistro: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Evaluador.Actions.BuscarUnidadOrganica,
                autoSubmit: false,
                onSuccess: base.Event.AjaxConsultarUnidadOrganicaRegistroSuccess
            }),

            AjaxMantenimientoGuardarForm: new SoftwareNet.DJ.Web.Components.AjaxForm({
                action: SoftwareNet.Web.Operacion.Evaluador.Actions.RegistrarMasivo,
                autoSubmit: false,
                idForm: "frmModelMasivo",
                onSuccess: base.Event.AjaxMantenimientoGuardarFormSuccess
            }),

            AjaxEliminar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Evaluador.Actions.Eliminar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxEliminarSuccess
            }),

            AjaxAutorizadorGuardar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Evaluador.Actions.RegistrarAutorizador,
                autoSubmit: false,
                onSuccess: base.Event.AjaxAutorizadorGuardarSuccess
            }),
 

         };
        base.Function = {
            AplicarBinding: function (model, contenedor) {
                var esValido = (typeof model !== 'undefined');
                if (esValido) {
                    var contenedorDom = (contenedor) ? contenedor[0] : contenedor;

                    esValido = true;// (model.Error.Code == 0);
                    if (esValido) {
                        ko.applyBindings(model, contenedorDom);
                    } else {
                        base.Control.Mensaje.Warning({ message: model.Error.Message }).WarningClose();
                    }
                } else {
                    base.Control.Mensaje.Error({ message: SoftwareNet.DJ.Web.Shared.Message.Resources.ErrorCargarViewModel });
                }
                return esValido;
            },



            CrearGrillaAutorizador: function () {
                General.configurarGrilla();
                base.Parameters.TableAutorizador = base.Control.FrmConsulta.GridAutorizador().DataTable({
                    ordering: false,
                    select: true,
                    // scrollY: 300,
                    //   paging: false,
                    columns: [

                        { "data": "", "title": "Editar", "class": "text-center"},
                        { "data": "", "title": "Anular", "class": "text-center" },
                        { "data": "NOMBRE_COMPLETO", "title": "Apellidos y Nombres" },
                        { "data": "", "title": "N°<br>Evaluado", "class": "text-center" },
                        { "data": "NOMBRE_OFICINA", "title": "Unidad Orgánica" },
                        { "data": "NOMBRE_AREA", "title": "Órgano" },
                        { "data": "CORREO_INSTITUCIONAL", "title": "Correo Electrónico" },
                        { "data": "NOMBRE_CARGO", "title": "Puesto" },
                        { "data": "NOMBRE_CATEGORIA", "title": "Categpría" },
                        { "data": "ID_OFICINA", "visible": false, "title": "ID_OFICINA" },
                        { "data": "ID_AREA", "visible": false, "title": "ID_AREA" },
                        { "data": "ID_EVALUADOR", "visible": false, "title": "ID_EVALUADOR" },
                       
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
                                html = '<a href="javascript:void(0);" ><i class="icon icon-edit icon-lg m-c-sm findone_mantenimiento" secuencial="' + row.INDICE + '" idpk="' + row.ID_EVALUADOR + '"></i></a>';
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
                                html = '<a href="javascript:void(0);" ><i class="icon icon-remove icon-lg m-c-sm delete_mantenimiento" idpk="' + row.ID_EVALUADOR + '" idpki="' + meta.row + '"></i></a>';
                                return html;
                            }
                        },

                        {
                            'targets': 3,
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-center',
                            'render': function (data, type, row, meta) {
                                var html = ""
                              /*
                                if (row.FLG_AUTORIZADOR == 1) {
                                    valor = "Responsable Principal";
                                    html += '<button class="btn btn-default btn-icon m-r-sm" title = "' + valor + '" data-container="body" data-placement="bottom" data-toggle="tooltip" type="button">';
                                    html += '<span class="icon icon-bell-o sq-24"></span>';
                                    html += '</button>';
                                }
                                else {
                                    var valor = "Alterno como responsable principal";
                                    html += '<button class="btn btn-info btn-icon m-r-sm" title = "' + valor + '" data-container="body" data-placement="bottom" data-toggle="tooltip" type="button">';
                                    html += '<span class="icon icon-bell-o sq-24"></span>';
                                    html += '</button>';
                                }

                                html += '<span class="icon-with-child hidden-xs">';
                                html += '<span class="icon icon-bell icon-lg icon-fw"></span>';
                                html += '<span class="badge badge-info badge-above right">8</span>';
                                html += '</span>';
     
                                */

                               
                                if (row.TOTAL_EVALUADO > 0) {
                                    html += '<span class="badge badge-info">' + row.TOTAL_EVALUADO+ '</span>';
                                }
                                else {
                                    html += '<span class="badge badge-danger">' + row.TOTAL_EVALUADO + '</span>';
                                }
                               
                              
                                return html;
                            }
                        },
      
                    ],

                });
               

            },
            BuscarGrilla: function () {
                var id_area = base.Control.FrmConsulta.DllOrgano().val();
                var id_oficina = base.Control.FrmConsulta.DllUnidadOrganica().val();
                if ((id_area == undefined || id_area == "" || id_area == null)) {
                    id_area = 0;
                }
                if ((id_oficina == undefined || id_oficina == "" || id_oficina == null)) {
                    id_oficina = 0;
                }
                base.Ajax.AjaxBuscar.data = {
                    ID_AREA_JEFE: parseInt(id_area),
                    ID_OFICINA_JEFE: parseInt(id_oficina)
                }
                base.Ajax.AjaxBuscar.submit();
            },

            GetParamnetroDetalleAutorizador: function (ID_EVALUADOR) {
                var parametro = {
                    ID_EVALUADOR: ID_EVALUADOR,
                };
                return parametro;
            },


            AbrirModalMantenimientoResponsable: function (id, parent) {

                base.Control.FrmRegistroAutorizador.DdlGrupoAsistencia().val('');
                base.Control.FrmRegistroAutorizador.DllOrgano().val('');
                base.Control.FrmRegistroAutorizador.DllUnidadOrganica().val('');
                base.Control.FrmRegistroAutorizador.ddlTitular().val('');
               // base.Control.FrmRegistroAutorizador.ddlAlterno().val('');
              //  base.Control.FrmRegistroAutorizador.ddlTipo().val('');
                base.Control.FrmRegistroAutorizador.DllOrgano().trigger("change");
                base.Control.FrmRegistroAutorizador.DllUnidadOrganica().trigger("change");
              //  base.Control.FrmRegistroAutorizador.DdlGrupoAsistencia().trigger("change");
                base.Control.FrmRegistroAutorizador.ddlTitular().trigger("change");
               // base.Control.FrmRegistroAutorizador.ddlAlterno().trigger("change");
                base.Control.FrmRegistroAutorizador.hdnIdAutorizador().val(0);
              //  $("#div_grupo").hide();
             //   $("#div_organica").hide();
                limpiarValidacionForm("frmModelResponsalbe");
               $("#hdnIdOficina").val("0");
                if (id > 0) {
                    var padre = $(parent).parent().parent();
                    var indice = base.Parameters.TableAutorizador.row(padre).index();
                    var data = base.Parameters.TableAutorizador.row(indice).data();
                    if (data != null) {
                        var parametro = base.Function.GetParamnetroDetalleAutorizador(data.ID_EVALUADOR);
                        var url = SoftwareNet.Web.Operacion.Evaluador.Actions.ObtenerDetalleAutorizador;
                           data = General.POST(url, parametro);
                        if (data != undefined && data != null) {
                            base.Control.FrmRegistroAutorizador.hdnIdAutorizador().val(data.ID_EVALUADOR);
                            base.Control.FrmRegistroAutorizador.DllOrgano().val(data.ID_AREA_JEFE);
                            base.Control.FrmRegistroAutorizador.DllOrgano().trigger("change");
                            base.Control.FrmRegistroAutorizador.DllUnidadOrganica().val(data.ID_OFICINA_JEFE);
                            $("#hdnIdOficina").val(data.ID_OFICINA_JEFE);
                            base.Control.FrmRegistroAutorizador.DllUnidadOrganica().trigger("change");
                            base.Control.FrmRegistroAutorizador.ddlTitular().val(data.ID_PERSONA_JEFE);
                            base.Control.FrmRegistroAutorizador.ddlTitular().trigger("change");
                        }
                    }
                } else { 
                    base.Control.FrmRegistroAutorizador.ddlTitular().val(''); 
                    base.Control.FrmRegistroAutorizador.hdnIdAutorizador().val(0);                
                }
                base.Control.ModalAgregarMantenimientoResponsable().modal('show');
            },

            AbrirModalMantenimientoMasivo: function (id, parent) {
                limpiarValidacionForm("frmModelMasivo");
                base.Control.ModalAgregarMantenimientoMasivo().modal('show');
            },
            /*
            HabilitarOpciones: function () {
                var flg_tipo = base.Control.FrmRegistroAutorizador.ddlTipo().val();
                base.Control.FrmRegistroAutorizador.DllUnidadOrganica().trigger("change");
                base.Control.FrmRegistroAutorizador.DllOrgano().trigger("change");
                base.Control.FrmRegistroAutorizador.DdlGrupoAsistencia().trigger("change");
                if (flg_tipo == "3") {
                    $("#div_grupo").show();
                    $("#div_organica").hide();
                }
                else {
                    $("#div_grupo").hide();
                    $("#div_organica").show();
                }
            },
            */
            EliminarMantenimiento: function (parent) {
                var padre = $(parent).parent().parent();
                var indice = base.Parameters.TableAutorizador.row(padre).index();
                var dataModel = base.Parameters.TableAutorizador.row(indice).data();



                var mensaje = "¿Estás seguro de anular el registro ?";
                //if (dataModel.FLG_ESTADO == "0") {
                //    mensaje = "¿Estás seguro de activar el registro ?";
                //}

                base.Control.Mensaje.Confirmation({
                    message: mensaje,
                }).ConfirmationAcept({
                    callback: function (opt) {
                        if (opt) {
                            var flg_estado =  "0" ;
                            var item = {
                                ID_EVALUADOR: parseInt(dataModel.ID_EVALUADOR),
                                FLG_ESTADO: flg_estado
                            };
                            base.Ajax.AjaxEliminar.data = item;
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

/*
function desabilitarAlterno(valor) {

    if (valor == 1) {
        $("#ddlAlterno").attr("disabled", true);
        $("#txtFechaInicioAlterno").attr("disabled", true);
        $("#txtFechaFinalAlterno").attr("disabled", true);
        $("#chkIndefinidoAlterno").attr("disabled", true);
    }
    else {
        $("#ddlAlterno").removeAttr("disabled");
        $("#txtFechaInicioAlterno").removeAttr("disabled");
        $("#txtFechaFinalAlterno").removeAttr("disabled");
        $("#txtFechaFinalAlterno").attr("disabled", true);
        $("#chkIndefinidoAlterno").removeAttr("disabled");

    }

}

$("input:radio[name=rbnJefe]").click(function () {
    var valor = $(this).val();
    if (valor == 2) {
        desabilitarAlterno(2);
    }
});

$("#chkIndefinidoTitular").click(function () {
    if ($(this).is(":checked")) {
        $("#txtFechaFinalTitular").attr("disabled", true);
    } else {
        $("#txtFechaFinalTitular").removeAttr("disabled");
    }
});


$("#chkIndefinidoAlterno").click(function () {
    if ($(this).is(":checked")) {
        $("#txtFechaFinalAlterno").attr("disabled", true);
    } else {
        $("#txtFechaFinalAlterno").removeAttr("disabled");
    }
});

*/
