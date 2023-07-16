try {
    ns('SoftwareNet.Web.Operacion.Seguimiento.Registro');
    $(document).ready(function () {
        'use strict';
        SoftwareNet.Web.Operacion.Seguimiento.Registro.Page = new SoftwareNet.Web.Operacion.Seguimiento.Registro.Controller();
        SoftwareNet.Web.Operacion.Seguimiento.Registro.Page.Ini();
    });
} catch (ex) {
    alert(ex.message);
}