try {
    ns('SoftwareNet.Web.Operacion.Programacion.Registro');
    $(document).ready(function () {
        'use strict';
        SoftwareNet.Web.Operacion.Programacion.Registro.Page = new SoftwareNet.Web.Operacion.Programacion.Registro.Controller();
        SoftwareNet.Web.Operacion.Programacion.Registro.Page.Ini();
    });
} catch (ex) {
    alert(ex.message);
}