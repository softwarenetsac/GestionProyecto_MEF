using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Gestion_Rendimiento_Common
{
    public  static   class Seguridad
    {

        public static bool ValidarEmail(string mail)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(mail, sFormato))
            {
                if (Regex.Replace(mail, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static string Encriptar(string _cadenaAencriptar)
        {
            //  string key = "";
            string result = string.Empty;
            try
            {
                if (!String.IsNullOrWhiteSpace(_cadenaAencriptar))
                {

                    byte[] encryted = Encoding.Unicode.GetBytes(_cadenaAencriptar);
                    result = Convert.ToBase64String(encryted);

                }

            }
            catch (Exception)
            {

                result = ""; ;
            }

            return result;

        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            try
            {

                if (!String.IsNullOrWhiteSpace(_cadenaAdesencriptar))
                {

                    byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
                    result = System.Text.Encoding.Unicode.GetString(decryted);
                }

            }
            catch (Exception)
            {

                result = "";
            }

            return result;
        }


    }
}
