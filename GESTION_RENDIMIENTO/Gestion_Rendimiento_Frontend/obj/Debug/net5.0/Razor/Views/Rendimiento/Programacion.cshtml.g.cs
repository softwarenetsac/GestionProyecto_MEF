#pragma checksum "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7e2ef67d4c9404900a6a1db935981ed426a3faa0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Rendimiento_Programacion), @"mvc.1.0.view", @"/Views/Rendimiento/Programacion.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\_ViewImports.cshtml"
using Gestion_Rendimiento_Frontend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\_ViewImports.cshtml"
using Gestion_Rendimiento_Frontend.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e2ef67d4c9404900a6a1db935981ed426a3faa0", @"/Views/Rendimiento/Programacion.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae21c9f595a50f67cba81ac593b6da05d7871619", @"/Views/_ViewImports.cshtml")]
    public class Views_Rendimiento_Programacion : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Gestion_Rendimiento_Frontend.Models.RendimientoViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("selected", new global::Microsoft.AspNetCore.Html.HtmlString("selected"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("frmModel"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", new global::Microsoft.AspNetCore.Html.HtmlString("frmModel"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("validator"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""wizard-container"">
    <div class=""card wizard-card ct-wizard-red"" id=""wizard"">

        <div class=""wizard-header"">
            <div class=""row m-x-sm"">
                <div class=""col-md-4"">
                    <h4 class=""m-t-sm"">Gestion Rendimiento</h4>
                </div>
            </div>
        </div>

        <div class=""bg-divider""></div>
        <div class=""card m-l-md m-r-md"">
            <div class=""card-header p-y-sm"">
                Buscar por:
                <div class=""card-actions"">
                    <button type=""button"" class=""card-action card-toggler"" title=""Collapse"" aria-expanded=""true""></button>

                </div>
            </div>
            <div class=""card-body p-b-sm m-b-sm"">
                <div class=""row"">
                   <div class=""col-md-4"">
                    <div class=""form-group"">
                    <label for=""ddl_Organo_consulta"">Año</label>
                    <select class=""form-control"" id=""ddlAnio_Consulta"" nam");
            WriteLiteral("e=\"ddlAnio_Consulta\">\r\n");
#nullable restore
#line 31 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                      
                    foreach (var item in Model.List_Anio)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e2ef67d4c9404900a6a1db935981ed426a3faa07798", async() => {
#nullable restore
#line 34 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                         Write(item.Text);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 34 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                      WriteLiteral(item.Value);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 35 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                    }
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </select>\r\n\r\n                    </div>\r\n                    </div>\r\n");
            WriteLiteral(@"                </div>
            </div>
        </div>
        <div class=""row text-left m-l-md m-r-md"">
            <div class=""col-md-6"">
                <div class=""row text-left"">
                    <button type=""button"" class=""btn btn-primary"" id=""btnAgregar"">
                        <i class=""icon icon-plus-circle icon-lg m-r-sm""></i>
                        Nueva Planificación
                    </button>
                    <button class=""btn btn-default m-l-sm"" id=""btnExportarEXCEl"" name=""btnExportarEXCEl"">
                        <i class=""icon icon-file-excel-o icon-lg m-r-sm""></i>
                        Exportar en EXCEL
                    </button>
                </div>
            </div>
            <div class=""col-md-6"">
                <div class=""row text-right"">
                    <button class=""btn btn-primary"" type=""button"" id=""btnBotonBuscar"">
                        <i class=""icon icon-search icon-lg m-r-sm""></i>
                        Buscar
               ");
            WriteLiteral(@"     </button>
                </div>
            </div>
        </div>

        <div class=""card-body"">

            <div class=""row"">
                <div class=""col-md-12"">
                    <div class=""table-responsive"">
                        <table id=""gridRendimiento"" class=""table table-bordered table-striped dataTable"">
                        </table>

                    </div>
                </div>

            </div>
        </div>
    </div>
</div>
");
            DefineSection("Modales", async() => {
                WriteLiteral(@"
    <div id=""modalRendimiento"" role=""dialog"" class=""modal fade"" data-backdrop=""static"" data-keyboard=""false"">
        <div class=""modal-dialog modal-xl"">
            <div class=""modal-content"">
                <div class=""modal-header bg-primary"" style=""cursor:pointer;"">
                    <h4 class=""modal-title"">Planificación</h4>
                </div>
                <div class=""modal-body"">
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e2ef67d4c9404900a6a1db935981ed426a3faa012291", async() => {
                    WriteLiteral(@"
                        <input type=""hidden"" id=""hdnIdProyecto"" name=""hdnIdProyecto"" value=""0"" />
                        <div class=""row"">
                            <div class=""col-md-12"">
                                <div class=""form-group"">
                                    <label>Evaluador/a (Jefe Directo)<span class=""text-primary m-l-sm"">(*)</span></label>
                                    <select class=""form-control"" required id=""ID_EVALUADOR"" name=""ID_EVALUADOR"">
                                        ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e2ef67d4c9404900a6a1db935981ed426a3faa013109", async() => {
                        WriteLiteral("---Seleccionar-----");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n");
#nullable restore
#line 108 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                          
                                            if (Model.Personas != null)
                                            {
                                                foreach (var item in Model.Personas)
                                                {

#line default
#line hidden
#nullable disable
                    WriteLiteral("                                                    ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7e2ef67d4c9404900a6a1db935981ed426a3faa015123", async() => {
#nullable restore
#line 113 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                                              Write(item.NOMBRE_COMPLETO);

#line default
#line hidden
#nullable disable
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    BeginWriteTagHelperAttribute();
#nullable restore
#line 113 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                      WriteLiteral(item.ID_PERSONA);

#line default
#line hidden
#nullable disable
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                    __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n");
#nullable restore
#line 114 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                }

                                            }

                                        

#line default
#line hidden
#nullable disable
                    WriteLiteral(@"                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""col-md-12 col-sm-12"">
                                <div class=""card"">
                                    <div class=""section-header"">
                                        <b>Prioridades Anuales</b>
                                    </div>
                                    <div class=""card-body"">
                                        <div class=""col-md-12 btn_agr"">
                                            <div class=""row text-left"">
                                                <a style=""font-weight: bold;font-size: 12px;"" href='javascript:void(0);' class=""text-info"" id=""BtnAgregarPrioridad""><i class='icon icon-plus m-r-sm'></i>Clic aquí para agregar una nueva prioridad</a>
                                            </div>
                         ");
                    WriteLiteral(@"               </div>
                                        <div class=""row"">
                                            <div class=""col-md-12"">
                                                <div class=""table-responsive"">
                                                    <table id=""gridMantenimiento"" class=""table table-striped table-bordered nowrap"" cellspacing=""0"" width=""100%"">
                                                        <thead>
                                                            <tr style='background-color:#FCE4D6;'>
                                                                <th colspan='5'>PRIORIDADES ANUALES DE GESTIÓN DEL ÓRGANO O UNIDAD ORGÁNICA</th>
                                                                <th style=""text-align:center;display:none""></th>
                                                                <th style=""text-align:center;display:none""></th>
                                                                <th style=""text-align:cente");
                    WriteLiteral(@"r;display:none""></th>
                                                                <th style=""text-align:center;display:none""></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id=""gridBody""></tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <br />
                        <div class=""row"">
                            <div class=""col-md-12"">
                                <label> <span class=""text-primary m-l-sm"">(*) Campo obligatoro</span></label>
                            </div>
                  ");
                    WriteLiteral("      </div>\r\n                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                </div>
                <div class=""modal-footer"">
                    <div class=""row"">
                        <div class=""m-t-lg text-center"">

                            <button id=""btnGuardar"" name=""btnGuardar"" class=""btn btn-primary"" type=""button"">Guardar</button>
                            <button class=""btn btn-default m-l-lg"" data-dismiss=""modal"" type=""button"">Cancelar</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class=""contenedoralerta""> </div>
    <div class=""contenedorconfirmacion""></div>

");
            }
            );
            WriteLiteral("<script type=\"text/javascript\">\r\n    var baseUrl = \"");
#nullable restore
#line 185 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
              Write(Url.Content("~"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\";\r\n</script>\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script type=\"text/javascript\">\r\n        ns(\'SoftwareNet.Web.Operacion.Programacion.Actions\')\r\n        SoftwareNet.Web.Operacion.Programacion.Actions.BuscarUnidadOrganica = \'");
#nullable restore
#line 193 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                                          Write(Url.Action("ListarUnidadOrganica", "Oficina"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n        SoftwareNet.Web.Operacion.Programacion.Actions.NuevaFila = \'");
#nullable restore
#line 194 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                               Write(Url.Action("GenerarHTML", "Rendimiento"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n        SoftwareNet.Web.Operacion.Programacion.Actions.NuevaFilaDetalle = \'");
#nullable restore
#line 195 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                                      Write(Url.Action("GenerarHTMLDET", "Rendimiento"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n        SoftwareNet.Web.Operacion.Programacion.Actions.Registrar = \'");
#nullable restore
#line 196 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                               Write(Url.Action("GuardarProyecto", "Rendimiento"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n        SoftwareNet.Web.Operacion.Programacion.Actions.Buscar = \'");
#nullable restore
#line 197 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
                                                            Write(Url.Action("GetAll_Proyecto", "Rendimiento"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n    </script>\r\n    <script");
                BeginWriteAttribute("src", " src=\"", 9834, "\"", 9880, 1);
#nullable restore
#line 199 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
WriteAttributeValue("", 9840, Url.Content("~/Scripts/jquery.form.js"), 9840, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    <script type=\"text/javascript\"");
                BeginWriteAttribute("src", " src=\"", 9927, "\"", 10015, 1);
#nullable restore
#line 200 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
WriteAttributeValue("", 9933, Url.Content("~/Scripts/CodeCustom/Rendimiento/Programacion/Controller.js?id=3.3"), 9933, 82, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n    <script type=\"text/javascript\"");
                BeginWriteAttribute("src", " src=\"", 10062, "\"", 10144, 1);
#nullable restore
#line 201 "D:\proyectos\MEF\GESTION_PROYECTO\FUENTE\GestionProyecto_MEF\GESTION_RENDIMIENTO\Gestion_Rendimiento_Frontend\Views\Rendimiento\Programacion.cshtml"
WriteAttributeValue("", 10068, Url.Content("~/Scripts/CodeCustom/Rendimiento/Programacion/Index.js?id=88"), 10068, 76, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral("></script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Gestion_Rendimiento_Frontend.Models.RendimientoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
