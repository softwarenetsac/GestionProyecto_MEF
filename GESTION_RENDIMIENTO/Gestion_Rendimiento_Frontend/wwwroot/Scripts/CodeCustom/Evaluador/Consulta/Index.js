try {
    ns('SoftwareNet.Web.Operacion.Evaluador.Registro');
    $(document).ready(function () {
        'use strict';
        SoftwareNet.Web.Operacion.Evaluador.Registro.Page = new SoftwareNet.Web.Operacion.Evaluador.Registro.Controller();
        SoftwareNet.Web.Operacion.Evaluador.Registro.Page.Ini();
    });
} catch (ex) {
    alert(ex.message);
}