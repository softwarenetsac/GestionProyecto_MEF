using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Extensiones.Email
{
    public static class CorreoElectronico
    {
        private static string AsignarSeccionHtml(string urlSistema, string nombreButton = "Ingresar al Sistema")
        {
            string style = "", html = "";
            if (!string.IsNullOrEmpty(urlSistema))
            {
                style += "background-color:#757171;border:1px solid #757171;border-radius:3px;color:#ffffff;display:inline-block;font-family:sans-serif;font-size:16px;height:24px;text-align:center;text-decoration:none;width:auto;-webkit-text-size-adjust:none;";
                style += "text-decoration: none;padding:.375rem .75rem;";
                html += "<tr>";
                html += "<td align='center'>";
                html += "<div> <a  style='" + style + "' href='" + urlSistema + "' >" + nombreButton + "  →</a> </div>";
                html += "</td>";
                html += "</tr>";
            }
  
            return html;
        }



   
   


        public static string GenerarHtml_General(string mensaje, string urlSistema)
        {
           
        
            string color = "#757171";
            //string oficinaG = oficina;
            //string conceptoG = concepto;
            //int indicadorExterno = 1;
            string strHTML = string.Empty;
            strHTML += "<table width='100%' cellspacing='0' cellpadding='0' border='0'>";
            strHTML += "<tbody><tr><td height='30'></td></tr>";
            strHTML += "<tr bgcolor='#F1F2F7'>";
            strHTML += "<td width='100%' valign='top' bgcolor='#F1F2F7' align='center'>";
            strHTML += "<!--  top header -->";
            strHTML += "<table width='600' cellspacing='0' cellpadding='0' border='0' align='center' class='container'>";
            strHTML += "<tbody>";
            strHTML += "<tr bgcolor=" + color + "><td height='15'></td></tr>";
            strHTML += "<tr bgcolor=" + color + ">";
            strHTML += "<td align='center'>";
            strHTML += "<table width='560' cellspacing='0' cellpadding='0' border='0' align='center' class='container-middle'>";
            strHTML += "<tbody><tr>";
            strHTML += "<td>";
            strHTML += "<table cellspacing='0' cellpadding='0' border='0' align='left' class='top-header-left'>";
            strHTML += "<tbody><tr>";
            strHTML += "<td align='center'>";
            strHTML += "<table cellspacing='0' cellpadding='0' border='0' class='date'>";
            strHTML += "<tbody><tr>";
            strHTML += "<td>&nbsp;&nbsp;</td>";
            strHTML += "<td style='color: #fefefe; font-size: 11px; font-weight: normal; font-family: Helvetica, Arial, sans-serif;'>";
            strHTML += "<singleline><b>";
            strHTML += "SISTEMA - ORH";
            strHTML += "</b></singleline>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "";
            strHTML += "</tbody></table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</tbody></table>";
            strHTML += "<table cellspacing='0' cellpadding='0' border='0' align='left' class='top-header-right'>";
            strHTML += "<tbody>";
            strHTML += "\t\t\t\t\t\t\t\t<tr>";
            strHTML += "\t\t\t\t\t\t\t\t\t<td width='30' height='20'></td>";
            strHTML += "\t\t\t\t\t\t\t\t</tr>";
            strHTML += "</tbody>";
            strHTML += "\t\t\t\t\t\t</table>";
            strHTML += "<table cellspacing='0' cellpadding='0' border='0' align='right' class='top-header-right'>";
            strHTML += "<tbody>";
            strHTML += "\t\t\t\t\t\t\t<tr>";
            strHTML += "<td align='center'>";
            strHTML += "<table cellspacing='0' cellpadding='0' border='0' align='center' class='tel'>";
            strHTML += "<tbody><tr>";
            strHTML += "<td>&nbsp;&nbsp;</td>";
            strHTML += "<td style='color: #fefefe; font-size: 11px; font-weight: normal; font-family: Helvetica, Arial, sans-serif;'>";
            strHTML += "<singleline><b>";
            strHTML += "Ministerio de Economía y Finanzas";
            strHTML += "</b></singleline>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += " </tbody></table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</tbody>";
            strHTML += "\t\t\t\t\t\t</table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</tbody></table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr bgcolor=" + color + "><td height='10'></td></tr>";
            strHTML += "</tbody>";
            strHTML += "</table>";

            strHTML += "<table width='600' cellspacing='0' cellpadding='0' border='0' bgcolor='ffffff' align='center' class='container'>";
            strHTML += "<!--Header-->";
            strHTML += "<tbody><tr><td height='40'></td></tr>";
            strHTML += "<tr>";
            strHTML += "<td>";
            strHTML += "<table width='560' cellspacing='0' cellpadding='0' border='0' align='center' class='container-middle'>";
            strHTML += "<tbody><tr>";
            strHTML += "<td>";
            strHTML += "<table width='528' cellspacing='0' cellpadding='0' border='0' align='left', style='text-align: left;',class='mainContent'>";
            strHTML += "<tbody>";

            //if (!string.IsNullOrEmpty(import))
            //{
            //    strHTML += "                       <tr>";
            //    strHTML += "<td><span style='color: #484848 !important; font-size: 14px; font-bold: normal; font-family: Helvetica, Arial, sans-serif;' class='main-header'><B>Importante:</B></span><span style='color: #FF0000 !important; font-size: 11px; font-weight: bold; font-family: Helvetica, Arial, sans-serif;' class='main-header'>&nbsp;&nbsp;" + import + "<br><br></span></td></tr>";
            //}

            strHTML += "<tr>";
            strHTML += "<td style='color: #484848 !important; font-size: 16px; font-weight: normal; font-family: Helvetica, Arial, sans-serif;' class='main-header'>";

             strHTML += mensaje;

            // strHTML += "Estimado(a): ";
            //  strHTML += nombre_Destinatario + "<br><br>";
            // strHTML += "\t\t\t\t\t\t\t\t\t\t\t" + mensaje;
           //  strHTML += "\t\t\t\t\t\t\t\t\t\t\t<br>";

            //if (!string.IsNullOrEmpty(colaborador))
            //{
            //    strHTML += "\t\t\t\t\t\t\t\t\t\t\t<B>Colaborador: </B>\t\t\t";
            //    strHTML += colaborador + "<br>";
            //}

            //if (!string.IsNullOrEmpty(periodo))
            //{
            //    strHTML += "\t\t\t\t\t\t\t\t\t\t\t<B>Periodo: </B> ";
            //    strHTML += periodo + "<br>";
            //}

            //if (!string.IsNullOrEmpty(estado))
            //{
            //    strHTML += "\t\t\t\t\t\t\t\t\t\t\t<B>Estado Actual: </B>\t\t\t";
            //    strHTML += estado + "<br>";
            //}




            //strHTML += "\t\t\t\t\t\t\t\t\t\t\t<B>Órgano: </B>\t\t\t : ";
            //strHTML += area + "<br>";
            //strHTML += "\t\t\t\t\t\t\t\t\t\t\t<B>Unidad Orgánica: </B> ";
            //strHTML += oficina + "<br>";

            //if (!string.IsNullOrEmpty(observacion))
            //{
            //    strHTML += "\t\t\t\t\t\t\t\t\t\t\t<B>Detalle: </B> ";
            //    strHTML += observacion + "<br>";
            //}

          //  strHTML += "<br>";
            //if (flagArchivo > 0)
            //{
            //    strHTML += "Se adjunta el contrato\t\t\t\t\t\t\t\t\t\t\t<br>";
            //}
            strHTML += "</td>";
            strHTML += "</tr>";

            strHTML += AsignarSeccionHtml(urlSistema);


            strHTML += "</tbody></table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr><td height='25'></td></tr>";
            strHTML += "</tbody></table>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<!-- end main section -->";
            strHTML += "<tr>";
            strHTML += "<td height='30' style='text-align: center;'>";
            strHTML += "<div style='text-align: center;'>";
            strHTML += "<div class='btn-group btn-group-justified'>";

            //if (id_estado == Constantes.Enviado_Al_Colaborador)
            //{
            //    string url_externo = ConfigurationManager.AppSettings["Url_Externo"];
            //    if (!string.IsNullOrEmpty(url_externo))
            //    {
            //        string link = url_externo + "contrato/index?id=" + urlParametro;
            //        string estado = "Adjuntar aquí documento firmado";
            //        strHTML += " <a style='border-radius:4px !important;padding: 10px 20px !important;font-size:14px;color: #FFFFFF;background-color: " + color + ";text-decoration: none;' href='" + link + "'>" + estado + "</a>&nbsp;";
            //    }
            //}

            strHTML += "</div>";
            strHTML += "</div>";
            strHTML += "</td><td>";
            strHTML += "</td></tr><tr>";
            strHTML += "<!-- footer section -->";
            strHTML += "</tr><tr><td height='15'></td></tr>";
            strHTML += "<tr>";
            strHTML += "<td>";
            strHTML += "</td>";
            strHTML += "</tr>";

            strHTML += "<tr>";
            strHTML += "<td align='center' class='prefooter-subheader' style='color: #939393; font-size: 11px; font-weight: normal; font-family: Helvetica, Arial, sans-serif;'>";
            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "<tr><td height='30'></td></tr>";
            strHTML += "</tbody></table>";
            strHTML += "<!--end main Content -->";

            strHTML += "</td>";
            strHTML += "</tr>";
            strHTML += "</tbody>";
            strHTML += "</table>";
            return strHTML;
        }

    }
}
