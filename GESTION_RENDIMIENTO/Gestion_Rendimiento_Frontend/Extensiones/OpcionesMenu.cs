
using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_Rendimiento_Frontend.Extensiones
{
    public static class OpcionesMenu
    {


        public static string GetMenu(List<Modulo> itemsMenu, string baseUrl)
        {

            string html = "";
            html = "<li class='sidenav-heading'>Opciones</li>";
            try
            {
                if (itemsMenu.Count > 0)
                {

                    html += "<li class='sidenav-item has'>";
                    html += "<a asp-area='' asp-controller='Panel' asp-action='Index' role='button'>";
                    html += "<span class='sidenav-icon icon icon-home'></span>";
                    html += "<span class='sidenav-label'>Inicio</span>";
                    html += "</a>";
                    html += "</li>";
                    var modulos = itemsMenu.Where(p => p.ID_SISTEMA_MODULO_PADRE == 0).ToList();
                    foreach (var item in modulos)
                    {
                        string nombreMenu = item.DESC_MODULO;
                        html += "<li class='sidenav-item has-subnav'>";
                        html += "<a href='javascript:void(0);' aria-haspopup='true'>";
                        if (!string.IsNullOrEmpty(item.IMAGEN))
                        {
                            html += "<span class='" + item.IMAGEN + "'></span>";
                        }
                        else
                        {
                            html += "<span class='sidenav-icon icon icon-edit'></span>";
                        }
                        html += "<span class='sidenav-label'>" + nombreMenu + "</span>";
                        html += "</a>";
                        html += "<ul class='sidenav-subnav collapse'>";
                        html += "<li class='sidenav-subheading'>" + nombreMenu + "</li>";
                        var hijos = itemsMenu.Where(m => m.ID_SISTEMA_MODULO_PADRE == item.ID_SISTEMA_MODULO).ToList();

                        if (hijos.Count() == 0)
                        {
                            Log.CreateLogger("item.Modulos_Hijos  devuelve cero registro");
                        }

                        foreach (var hijo in hijos)
                        {
                            var url = baseUrl + hijo.URL_MODULO;
                            html += "<li><a href= '" + url + "'>" + hijo.DESC_MODULO + "</a></li>";
                        }
                        html += "</ul>";
                        html += "</li>";
                    }

                }
                else
                {
                    Log.CreateLogger("itemsMenu.Length devuelve cero registro");
                }
            }
            catch (Exception ex)
            {
                html = "";
                Log.CreateLogger("menu html=" + ex.Message);
            }
            return html;
        }


    }
}
