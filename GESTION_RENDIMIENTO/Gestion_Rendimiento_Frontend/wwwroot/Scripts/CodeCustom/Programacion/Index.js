try {
    ns('SoftwareNet.Web.Operacion.Mantenimiento.Registro');
    $(document).ready(function () {
        'use strict';
        SoftwareNet.Web.Operacion.Mantenimiento.Registro.Page = new SoftwareNet.Web.Operacion.Mantenimiento.Registro.Controller();
        SoftwareNet.Web.Operacion.Mantenimiento.Registro.Page.Ini();
    });
} catch (ex) {
    alert(ex.message);
}