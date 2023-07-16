try {
    ns('SoftwareNet.Web.Operacion.RendimientoORH.Registro');
    $(document).ready(function () {
        'use strict';
        SoftwareNet.Web.Operacion.RendimientoORH.Registro.Page = new SoftwareNet.Web.Operacion.RendimientoORH.Registro.Controller();
        SoftwareNet.Web.Operacion.RendimientoORH.Registro.Page.Ini();
    });
} catch (ex) {
    alert(ex.message);
}