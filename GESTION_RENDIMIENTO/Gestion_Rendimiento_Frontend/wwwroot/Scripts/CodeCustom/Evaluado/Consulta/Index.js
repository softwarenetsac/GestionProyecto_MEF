try {
    ns('SoftwareNet.Web.Operacion.Evaluado.Registro');
    $(document).ready(function () {
        'use strict';
        SoftwareNet.Web.Operacion.Evaluado.Registro.Page = new SoftwareNet.Web.Operacion.Evaluado.Registro.Controller();
        SoftwareNet.Web.Operacion.Evaluado.Registro.Page.Ini();
    });
} catch (ex) {
    alert(ex.message);
}