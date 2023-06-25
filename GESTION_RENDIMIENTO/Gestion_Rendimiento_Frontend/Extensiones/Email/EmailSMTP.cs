
using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using static Gestion_Rendimiento_Entity.Model.ConfiguracionSistemaModel;

namespace Gestion_Rendimiento_Frontend.Extensiones.Email
{
    public static class   EmailSMTP
    {
        private static void AdjuntarFile(ref MailMessage message, List<EmailFile> files)
        {
            if (files != null)
            {
                foreach (EmailFile file in files)
                {
                    if (!string.IsNullOrEmpty(file.url))
                    {
                        //Attachment attach = new Attachment(file.url);
                        // message.Attachments.Add(attach);
                        byte[] archivo = File.ReadAllBytes(file.url);
                        MemoryStream ms = new MemoryStream(archivo);
                        Attachment Adjunto = new Attachment(ms, "");
                        message.Attachments.Add(Adjunto);
                        ms.Close();
                        ms.Dispose();
                    }
                }
            }
        }

        public static BaseResponse EnviarCorreoElectronico(ConfiguracionCorreo _configuracion, string correo_to, string nombre_remitente, string asunto, string cuerpo, List<EmailFile> fileAttach = null)
        {
            BaseResponse respuesta = new BaseResponse();
            try
            {
                try
                {
                    Mensaje email = new Mensaje();
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    string copiaOculta = _configuracion.CopiaOculta;
                    string copia = _configuracion.Copia;
                    string correo_remitente = _configuracion.Remitente;

                    bool enableSsl = _configuracion.EnableSsl;
                    message.From = new MailAddress(correo_remitente, nombre_remitente);
                    if (!string.IsNullOrEmpty(correo_to))
                    {
                        string[] correos = correo_to.Split(',');
                        if (correos != null)
                        {
                            foreach (var item in correos)
                            {
                                var correo = item;
                                if (!string.IsNullOrEmpty(correo))
                                {
                                    message.To.Add(new MailAddress(correo));
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(copia))
                    {
                        string[] correos = copia.Split(',');
                        if (correos != null)
                        {
                            foreach (var item in correos)
                            {
                                var correo = item;
                                if (!string.IsNullOrEmpty(correo))
                                {
                                    message.CC.Add(new MailAddress(correo));
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(copiaOculta))
                    {
                        string[] correos = copiaOculta.Split(',');
                        if (correos != null)
                        {
                            foreach (var item in correos)
                            {
                                var correo = item;
                                if (!string.IsNullOrEmpty(correo))
                                {
                                    message.Bcc.Add(new MailAddress(correo));
                                }
                            }
                        }
                    }

                    MemoryStream ms = null;
                    if (fileAttach != null)
                    {
                        foreach (EmailFile file in fileAttach)
                        {
                            if ((file.Archivo != null))
                            {
                                ms = new MemoryStream(file.Archivo);
                                Attachment Adjunto = new Attachment(ms, file.nombre);
                                message.Attachments.Add(Adjunto);
                            }
                        }
                    }

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s
                   , X509Certificate certificate
                   , X509Chain chain
                   , SslPolicyErrors sslPolicyErrors)

                    { return true; };

                    message.Subject = asunto;

                    string clave = _configuracion.Clave;
                    string host = _configuracion.Servidor;
                    smtp.Host = host;
                    smtp.Credentials = new NetworkCredential(correo_remitente, clave);
                    smtp.Port = _configuracion.Puerto;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Body = cuerpo;
                    message.BodyEncoding = Encoding.UTF8;
                    smtp.EnableSsl = enableSsl;
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.Normal;
                    smtp.Send(message);
                    respuesta.Success = true;
                    respuesta.Code = 200;
                    if (ms != null)
                    {
                        ms.Close();
                        ms.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    string inner = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                    respuesta.Message = inner;
                    respuesta.Success = false;
                    Log.CreateLogger("EnviarCorreoElectronico= " + ex.Message);
                    respuesta.Code = (int)HttpStatusCode.InternalServerError;
                }

            }
            catch (Exception ex)
            {
                Log.CreateLogger("EnviarCorreoElectronico2= " + ex.Message);
                respuesta.Message = ex.Message;
            }
            return respuesta;
        }
    }
}
