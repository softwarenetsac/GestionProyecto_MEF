using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Laserfiche.DocumentServices;
using Laserfiche.RepositoryAccess;
using System.IO;
using static System.Net.WebRequestMethods;

namespace Gestion_Rendimiento_Laserfiche.Repositorio
{
    public class UtilLaserfiche
    {
        private static Session IniciarSesion()
        {
            String usuarioLF1 = "";
            String clave1 = "";
            String Repositorio = "";
            String Servidor = "";
            Session session = null;

            try
            {
                Servidor = WebConfigurationManager.AppSettings["IpRepositorio"].ToString();
                Repositorio = WebConfigurationManager.AppSettings["Repositorio"].ToString();
                usuarioLF1 = WebConfigurationManager.AppSettings["UserLaserfiche"].ToString();
                clave1 = WebConfigurationManager.AppSettings["ClaveLaserfiche"].ToString();
                session = new Session();
                session.LogIn(usuarioLF1, clave1, new RepositoryRegistration(Servidor, Repositorio));
            }
            catch (Exception ex)
            {
                AplicacionLog.Mensaje(ex.ToString());
            }
            return session;
        }

        public static byte[] ExportarDocumentoPDF(int IdArchivo,string nombreArchivo)
        {
            bool valorDevuelve = true;
            Session session = new Session();
            String Repositorio = WebConfigurationManager.AppSettings["IpRepositorio"].ToString();
            String Servidor = WebConfigurationManager.AppSettings["Repositorio"].ToString();
            String Carpeta = HttpContext.Current.Request.PhysicalApplicationPath + "\\Archivo_Temporales";
            byte[] archivo = null;
            // Exports electronic document.
            try
            {
                session = IniciarSesion();
                if (session != null)
                {
                    DocumentExporter DocEx = new DocumentExporter();
                    DocumentInfo documentInfo = Document.GetDocumentInfo(IdArchivo, session);
                    Console.Write(documentInfo.Name);
                    nombreArchivo = Guid.NewGuid() + "." + documentInfo.Extension;
                    string ruta_oficial = Carpeta + "\\" + nombreArchivo;
                    if (System.IO.File.Exists(ruta_oficial))
                    {
                        System.IO.File.Delete(ruta_oficial);
                        DocEx.ExportElecDoc(documentInfo, Carpeta + nombreArchivo);
                    }
                    else
                    {
                        try
                        {
                            DocEx.ExportElecDoc(documentInfo, Carpeta + nombreArchivo);
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            throw;
                        }

                    }
                     archivo = System.IO.File.ReadAllBytes(Carpeta + nombreArchivo);
                }
            }
            catch (Exception err)
            {              
                AplicacionLog.Mensaje(err.ToString());
                session.LogOut();
            }
            finally
            {
                try
                {
                    session.LogOut();
                }
                catch (Exception ex)
                {
                    AplicacionLog.Mensaje(ex.ToString());
                }
            }

            return archivo;
        }

        //public static String ExportarDocumentoPDF(int IdArchivo)
        //{
        //    String nombreArchivo = "";
        //    Guid myName = Guid.NewGuid();
        //    Session session = new Session();
        //    String Repositorio = WebConfigurationManager.AppSettings["IpRepositorio"].ToString();
        //    String Servidor = WebConfigurationManager.AppSettings["Repositorio"].ToString();
        //    String Carpeta = HttpContext.Current.Request.PhysicalApplicationPath + "\\Archivo_Temporales";
        //    try
        //    {
        //        session = IniciarSesion();
        //        if (session != null)
        //        {
        //            DocumentExporter DocEx = new DocumentExporter();
        //            DocumentInfo documentInfo = Document.GetDocumentInfo(IdArchivo, session);
        //            nombreArchivo = documentInfo.Id + "." + documentInfo.Extension;
        //            string ruta_oficial = Carpeta + '\\' + nombreArchivo;
        //            if (File.Exists(ruta_oficial))
        //            {
        //                File.Delete(ruta_oficial);
        //                DocEx.ExportElecDoc(documentInfo, Carpeta + nombreArchivo);
        //            }
        //            else
        //            {
        //                DocEx.ExportElecDoc(documentInfo, Carpeta + nombreArchivo);
        //            }
        //        }
        //        else
        //        {
        //            nombreArchivo = "";
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        AplicacionLog.Mensaje(err.ToString());
        //        nombreArchivo = "";
        //    }
        //    finally
        //    {
        //        try
        //        {
        //            session.LogOut();
        //        }
        //        catch (Exception ex)
        //        {
        //            AplicacionLog.Mensaje(ex.ToString());
        //        }
        //    }

        //    return nombreArchivo;
        //}

        public static int SubirArchivoSubSubCarpeta(string vFile, string SubCarpeta, string SubSubCarpeta, string NombreArchivo)
        {
            int IdLaserFiche = 0;
            String Servidor  = WebConfigurationManager.AppSettings["IpRepositorio"].ToString();
            String Repositorio = WebConfigurationManager.AppSettings["Repositorio"].ToString();
            String Volumen = WebConfigurationManager.AppSettings["VolumenLaserfiche"].ToString();
            String Carpeta = WebConfigurationManager.AppSettings["RutaLaserfiche"].ToString();
            Session session = new Session();
            try
            {
                vFile = vFile.Trim();
                session = IniciarSesion();
                if (session != null)
                {
                    DocumentImporter importer = new DocumentImporter() { Document = new DocumentInfo(session), OverwritePages = true, ExtractTextFromEdoc = true };
                    Search lfSearch1 = new Search(session);
                    lfSearch1.Command = "{LF:Name=\"" + SubCarpeta + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\"}";
                    lfSearch1.Run();
                    SearchListingSettings searchSettings1 = new SearchListingSettings();
                    searchSettings1.SortDirection = SortDirection.Ascending;
                    SearchResultListing results1 = lfSearch1.GetResultListing(searchSettings1);

                    if (results1.RowCount <= 0)
                    {
                        //No Existe, Creamos la SubCarpeta
                        FolderInfo carpetaContent = Folder.GetFolderInfo("\\" + Carpeta, session);
                        FolderInfo carpetaExpediente = new FolderInfo(session);
                        carpetaExpediente.Create(carpetaContent, SubCarpeta, EntryNameOption.None);
                        carpetaExpediente.Unlock();
                        carpetaExpediente.Refresh(true);
                    }

                    /* VALIDAMOS LA SUB-SUBCARPETA*/
                    if (SubSubCarpeta.Contains("\\"))
                    {
                        String[] carpetas = SubSubCarpeta.Split('\\');
                        String SubSubAuxiliar = "";
                        string SubBuscar = "";
                        if (carpetas.Length > 0)
                        {
                            for (int i = 0; i < carpetas.Length; i++)
                            {
                                SubSubAuxiliar += carpetas[i];
                                Search lfSearch2 = new Search(session);
                                if (i == 0)
                                {
                                    lfSearch2.Command = "{LF:Name=\"" + carpetas[i] + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\\" + SubCarpeta + "\"}";
                                }
                                else
                                {
                                    SubBuscar += carpetas[i - 1];
                                    lfSearch2.Command = "{LF:Name=\"" + carpetas[i] + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\\" + SubCarpeta + "\\" + SubBuscar + "\"}";
                                    SubBuscar += "\\";
                                }
                                lfSearch2.Run();
                                SearchListingSettings searchSettings2 = new SearchListingSettings();
                                searchSettings2.SortDirection = SortDirection.Ascending;
                                SearchResultListing results2 = lfSearch2.GetResultListing(searchSettings2);

                                if (results2.RowCount <= 0)
                                {
                                    //No existe, creamos ls SubSubCarpeta
                                    FolderInfo carpetaContent = Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta, session);
                                    FolderInfo carpetaExpediente = new FolderInfo(session);

                                    if (SubSubAuxiliar.Contains("\\"))
                                    {
                                        String[] carpetas2 = SubSubCarpeta.Split('\\');
                                        carpetaContent = Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta + '\\' + carpetas2[0], session);
                                    }

                                    //carpetaExpediente.Create(carpetaContent, SubSubAuxiliar, EntryNameOption.None);
                                    carpetaExpediente.Create(carpetaContent, carpetas[i], EntryNameOption.None);
                                    carpetaExpediente.Unlock();
                                    carpetaExpediente.Refresh(true);
                                }
                                SubSubAuxiliar += "\\";
                            }
                        }
                    }
                    else
                    {
                        Search lfSearch = new Search(session);
                        lfSearch.Command = "{LF:Name=\"" + SubSubCarpeta + "\", Type=\"F\"} & {LF:LOOKIN=\"" + Repositorio + "\\" + Carpeta + "\\" + SubCarpeta + "\"}";
                        lfSearch.Run();
                        SearchListingSettings searchSettings = new SearchListingSettings();
                        searchSettings.SortDirection = SortDirection.Ascending;
                        SearchResultListing results = lfSearch.GetResultListing(searchSettings);

                        if (results.RowCount <= 0)
                        {
                            //No existe, creamos la SubSubCarpeta
                            FolderInfo carpetaContent = Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta, session);
                            FolderInfo carpetaExpediente = new FolderInfo(session);
                            carpetaExpediente.Create(carpetaContent, SubSubCarpeta, EntryNameOption.None);
                            carpetaExpediente.Unlock();
                            carpetaExpediente.Refresh(true);
                        }
                    }

                    importer.Document.Create(Folder.GetFolderInfo("\\" + Carpeta + "\\" + SubCarpeta + "\\" + SubSubCarpeta, session), NombreArchivo, Volumen, EntryNameOption.AutoRename);
                    importer.Document.Extension = vFile.Substring(vFile.LastIndexOf(".") + 1, vFile.Length - (vFile.LastIndexOf(@".") + 1));
                    importer.GetType();

                    importer.ImportEdoc("application/vnd.ms-word", vFile);
                    importer.Document.Save();
                    session.LogOut();
                    IdLaserFiche = importer.Document.Id;
                    if (System.IO.File.Exists(vFile))
                    {
                        System.IO.File.Delete(vFile);
                    }

                }
                else
                {
                    IdLaserFiche = 0;
                }
            }
            catch (Exception EX)
            {
                AplicacionLog.Mensaje(EX.ToString());
                try
                {
                    session.LogOut();
                }
                catch (Exception ex)
                {
                    AplicacionLog.Mensaje(ex.ToString());
                }
            }
            return IdLaserFiche;
        }

    }
}