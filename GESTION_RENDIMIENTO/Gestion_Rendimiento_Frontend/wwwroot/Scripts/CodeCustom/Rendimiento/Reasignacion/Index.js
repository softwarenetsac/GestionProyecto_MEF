try {
    ns('SoftwareNet.Web.Operacion.Reasignacion.Registro');
    $(document).ready(function () {
        'use strict';
        SoftwareNet.Web.Operacion.Reasignacion.Registro.Page = new SoftwareNet.Web.Operacion.Reasignacion.Registro.Controller();
        SoftwareNet.Web.Operacion.Reasignacion.Registro.Page.Ini();
    });
} catch (ex) {
    alert(ex.message);
}