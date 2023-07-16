try {
    ns('SoftwareNet.Web.Operacion.Programacion.Registro.Controller');
    SoftwareNet.Web.Operacion.Programacion.Registro.Controller = function () {
        var base = this;
        base.Ini = function (opts) {
            'use strict';
            base.Control.BotonBuscar().click(base.Event.BotonBuscarClick);
            base.Function.CrearGrillaProgramacion();
            base.Function.BuscarGrilla();

        };

        base.Parameters = {
            TableProgramacion: null
        };

        base.Control = {
            Mensaje: new SoftwareNet.DJ.Web.Components.Message(),
            BotonBuscar: function () { return $("#btnBotonBuscar"); },
            GridProgramacion: function () { return $("#gridProgramacion"); },
        };

        base.Event = {
            BotonBuscarClick: function () {
                base.Function.BuscarGrilla();
            },
            AjaxBuscarSuccess: function (data) {

                if (data.Result) {
                    base.Parameters.TableProgramacion.clear().draw();
                    base.Parameters.TableProgramacion.rows.add(data.Result).draw();
                }
            },
        };
        base.Ajax = {
            AjaxBuscar: new SoftwareNet.DJ.Web.Components.Ajax({
                action: SoftwareNet.Web.Operacion.Programacion.Actions.Buscar,
                autoSubmit: false,
                onSuccess: base.Event.AjaxBuscarSuccess
            }),
        };
        base.Function = {
            CrearGrillaProgramacion: function () {
                General.configurarGrilla();
                base.Parameters.TableProgramacion = base.Control.GridProgramacion().DataTable({
                    ordering: false,
                    select: true,
                    columns: [
                        { "data": "ID_PROYECTO", "visible": false  },
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