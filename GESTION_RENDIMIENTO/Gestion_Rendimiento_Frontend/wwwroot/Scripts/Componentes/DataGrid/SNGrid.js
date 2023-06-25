ns('SoftwareNet.DJ.Web.Components');
SoftwareNet.DJ.Web.Components.SGrid = function (opts) {
    this.init(opts);
};
SoftwareNet.DJ.Web.Components.SGrid.prototype = {
    crearGrillaSupervision: function () {
        Tabla_Supervision = $('#gridProcedimiento_Supervision').DataTable
           ({
               columns:
               [
                { "data": "Id", "visible": false },
                { "data": "" },//editarº
                { "data": "" },//quitar
                { "data": "Id" },
                { "data": "FechaAudiencia" },
                { "data": "Usuario" },
                { "data": "ItemEstado.Id" },
                { "data": "Observacion" }             
               ],
               "columnDefs": [
                 {
                     "targets": 1,
                     "data": null,
                     "render": function (data, type, row, meta) {
                         var html = "";
                         if (lectura == "1") {
                             html = '<a  href="javascript:void(0)"  onclick="venatana_procedimiento_supervision(' + row.IdDetalleMaestra + ',1)" ><i class="icon icon-edit icon-lg m-r-sm"></i>Editar</a>'
                             html += '<a class="m-l-md" href="javascript:void(0)" onclick="eliminar_supervision(this,' + row.IdDetalleMaestra + ')"><i class="icon icon-remove icon-lg m-r-sm"></i></a>';
                         }
                         else {
                             html = '<a  href="javascript:void(0)"  onclick="venatana_procedimiento_supervision(' + row.IdDetalleMaestra + ',1)" ><i class="icon icon-edit icon-lg m-r-sm"></i>Ver</a>'

                         }


                         return html;
                     }
                 }
                 ,
                      {
                          "targets": 5,
                          "data": null,
                          "render": function (data, type, row, meta) {
                              if (row.NumeroExpediente != null && row.NumeroExpediente != "") {
                                  var expediente = urlExpepdiente + "?nro_exp=" + row.NumeroExpediente;
                                  expediente += "&tipo=" + row.TipoExpediente;
                                  var html = '<a   target="_blank" href=' + expediente + '>' + row.NumeroExpediente + ' </a>';
                                  return html;
                              }

                          }
                      }

               ]
           });
        Tabla_Supervision.buttons().container().appendTo('#gridProcedimiento_Supervision_wrapper .col-sm-6:eq(0)');
    }
}