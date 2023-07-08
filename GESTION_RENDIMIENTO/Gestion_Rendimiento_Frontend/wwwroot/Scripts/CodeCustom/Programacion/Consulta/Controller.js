try {
    ns('SoftwareNet.Web.Operacion.Mantenimiento.Registro.Controller');
    SoftwareNet.Web.Operacion.Mantenimiento.Registro.Controller = function () {
        var base = this;
        init_contadorTa("txtNombre", "contadorTexto");

  
        base.Ini = function (opts) {
            'use strict';
   
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
                base.Control.BotonAgregar().click(base.Event.BotonAgregarClick);
                base.Control.BotonGuardar().click(base.Event.BotonGuardarClick);
         


            base.Control.GridMantenimiento().on('click', '.findone_mantenimiento', function (e) {
                var id = $(e.target).attr("idPk");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.AbrirModalMantenimiento(id, this);
            });
            base.Control.GridMantenimiento().on('click', '.delete_mantenimiento', function (e) {
             
                var id = $(e.target).attr("idPk");
                if (typeof id !== 'undefined' && id > 0)
                    base.Function.EliminarMantenimiento(this);
            });

            base.Function.CrearGrillaMantenimiento();
            base.Function.BuscarGrilla();
        };

        base.Parameters = {
            TableMantenimiento: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
     
            ModalAgregarMantenimiento: function () { return $("#modalMantenimiento"); },
            BotonAgregar: function () { return $('#btnAgregar'); },

            FormMantenimientoRegistro: function () { return $("#frmModel"); },
            FrmRegistroMantenimiento: {

                HdnId: function () { return $("#hdnId"); },
                TxtNombre: function () { return $("#txtNombre"); },

            },

            BotonGuardar: function () { return $("#btnGuardar"); },
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            GridMantenimiento: function () { return $("#gridMantenimiento"); },


        };

        base.Event = {
            BotonBuscarClick: function () {
                base.Function.BuscarGrilla();
            },
            BotonAgregarClick: function () {
                base.Function.AbrirModalMantenimiento();
            },
            BotonGuardarClick: function () {

                var form = base.Control.FormMantenimientoRegistro();
                form.validate();
                if (!form.valid()) { return false; }
                else {

                    //base.Control.Mensaje.Confirmation({
                    //    message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaConfirmacion,
                    //}).ConfirmationAcept({
                    //    callback: function (opt) {
                    //        if (opt) {

                    base.Ajax.AjaxMantenimientoGuardar.data = {
                        ID_GRUPO: base.Control.FrmRegistroMantenimiento.HdnId().val(),
                        DESCRIPCION: base.Control.FrmRegistroMantenimiento.TxtNombre().val(),


                    }
                    base.Ajax.AjaxMantenimientoGuardar.submit();
                    //        }
                    //    }
                    //});


                }
            },




            AjaxBuscarSuccess: function (data) {

                if (data.Result) {
                    base.Parameters.TableMantenimiento.clear().draw();
                    base.Parameters.TableMantenimiento.rows.add(data.Result).draw();

                    //Tabla_Listado.clear().draw();
                    //Tabla_Listado.rows.add(data).draw();
                }
            },
            AjaxEliminarSuccess: function (data) {
                switch (data.Code) {
                    case -1:
                        base.Control.Mensaje.Warning({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaEjecutoNingunProceso }).WarningClose();
                        break;

                    case 200:
                        base.Control.Mensaje.Information({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaAnuloExito, codePrinter: data.ValorExtra });

                        base.Control.Mensaje.InformationClose({
                            callback: function (opt) {
                                if (opt) {
                                    base.Function.BuscarGrilla();
                                }
                            }
                        });
                        break;

                    default:
                        base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                        break;
                }

            },
            AjaxMantenimientoGuardarSuccess: function (data) {

                switch (data.Code) {
                    case -1:
                        base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                        break;

                    case 200:
                        base.Control.Mensaje.Information({ message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaGuardoExito })
                            .InformationClose({
                                callback: function (opt) {
                                    if (opt) {

                                        base.Function.BuscarGrilla();
                                        base.Control.ModalAgregarMantenimiento().modal("hide");
                                    }
                                }
                            });

                        break;

                    default:
                        base.Control.Mensaje.Warning({ message: data.Message }).WarningClose();
                        break;
                }
            }

        };
        base.Ajax = {
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Mantenimiento.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
            }),
            AjaxEliminar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Mantenimiento.Actions.Eliminar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxEliminarSuccess
            }),
            AjaxMantenimientoGuardar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Mantenimiento.Actions.Registrar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxMantenimientoGuardarSuccess
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
            CrearGrillaMantenimiento: function () {
                General.configurarGrilla();

                base.Parameters.TableMantenimiento = base.Control.GridMantenimiento().DataTable({
                    ordering: false,
                    select: true,
                    columns: [

                        { "data": "" },
                        { "data": "" },
                        { "data": "ID_GRUPO", "visible": false },
                        { "data": "DESCRIPCION" },
                        { "data": "" },
                        { "data": "FLG_ESTADO", "visible": false }
                    ],
                    "pageLength": 60,
                    "columnDefs": [
                        {
                            'targets': 0,
                            'searchable': false,
                            'orderable': false,
                            'render': function (data, type, row, meta) {
                                var html = "";
                          
                                html = '<a href="javascript:void(0);" ><i class="icon icon-edit icon-lg m-c-sm findone_mantenimiento" idpk="' + row.ID_GRUPO + '" idpki="' + meta.row + '" ></i></a>';
                                return html;
                            }
                        },

                        {
                            'targets': 1,
                            'searchable': false,
                            'orderable': false,
                            'render': function (data, type, row, meta) {
                                var html = "";
                                 html = '<a href="javascript:void(0);" ><i class="icon icon-remove icon-lg m-c-sm delete_mantenimiento" idpk="' + row.ID_GRUPO + '" idpki="' + meta.row + '"></i></a>';
                                return html;
                            }
                        },

                        {
                            'targets': 4,
                            'searchable': false,
                            'orderable': false,
                            'render': function (data, type, row, meta) {
                                var html = "INACTIVO";
                                if (row.FLG_ESTADO == 1) {
                                    html = "ACTIVO";
                                }
                                return html;
                            }
                        }
                    ],

                });
                // TableContrato.buttons().container().appendTo('#gridContrato_wrapper .col-sm-12:eq(0)');

            },

            BuscarGrilla: function () {

                base.Ajax.AjaxBuscar.submit();
            },
            AbrirModalMantenimiento: function (id, parent) {
                limpiarValidacionForm("frmModel");
                base.Control.FrmRegistroMantenimiento.HdnId().val("0");
                base.Control.FrmRegistroMantenimiento.TxtNombre().val("");

                if (id > 0) {
                    var padre = $(parent).parent().parent();
                    var indice = base.Parameters.TableMantenimiento.row(padre).index();

                    var data = base.Parameters.TableMantenimiento.row(indice).data();

                    if (data != null) {
                      
                        base.Control.FrmRegistroMantenimiento.HdnId().val(data.ID_GRUPO);
                        base.Control.FrmRegistroMantenimiento.TxtNombre().val(data.DESCRIPCION);
                    }
                }

                base.Control.ModalAgregarMantenimiento().modal('show');
            },
            EliminarMantenimiento: function (parent) {
                var padre = $(parent).parent().parent();
                var indice = base.Parameters.TableMantenimiento.row(padre).index();
                var dataModel = base.Parameters.TableMantenimiento.row(indice).data();

                var mensaje = "¿Estás seguro de anular el registro ?";
                if (dataModel.FLG_ESTADO == "0") {
                    mensaje = "¿Estás seguro de activar el registro ?";
                }

                base.Control.Mensaje.Confirmation({
                    //   message: SoftwareNet.DJ.Web.Shared.Mensaje.Resources.EtiquetaConfirmacionAnular,
                    message: mensaje,
                }).ConfirmationAcept({
                    callback: function (opt) {
                        if (opt) {
                            var flg_estado = dataModel.FLG_ESTADO == "1" ? "0" : "1";
                            var item = {
                                ID_GRUPO: parseInt(dataModel.ID_GRUPO), 
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