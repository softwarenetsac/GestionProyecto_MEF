
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Rendimiento_Frontend.Extensiones
{
    public static class ExcelUtil
    {
        #region "Exportar a excel-Configuracion Celda"
        public static void CeldaFormatoEtiqueta_Numero(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", string formatoDecimal = "D")
        {
            try
            {
                string formato = "#,##0.00;[red]-#,##0.00;";
                if (formatoDecimal == "E") // entero
                {
                    // sin decimal
                    formato = "#,##0.00;[red]-#,##0;";

                }

                Object obj = new Object();
                obj = valor;
                string valueComuna = obj.ToString();
                //   formato += "-";
                if (string.IsNullOrEmpty(valueComuna))
                {
                    valueComuna = "0";
                }
                else {
                    valueComuna = valor.ToString();
                }

           

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                  //  rng.Style.Font.Bold = true;
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    rng.Style.Font.Size = 11;

                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


                    //rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                    //rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    //rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    //rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    //rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    //rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    //rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    // rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
                    //  rng.Style.Font.Color.SetColor(Color.FromArgb(0, 0, 0));
                    if (fila2 - fila1 > 0 || columna2 - columna1 > 0)
                    {
                        rng.Merge = true;
                    }
                    if (formatoDecimal == "D")
                    {
                        rng.Value = decimal.Parse(valueComuna);
                        rng.Style.Numberformat.Format = formato;
                    }
                    else
                    {
                        rng.Value = Double.Parse(valueComuna);
                        // rng.Style.Numberformat.Format = formato;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }



        public  static void CeldaFormatoEtiqueta_V2(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", bool negrita = false)
        {
            try
            {

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    if (negrita)
                    {
                        rng.Style.Font.Bold = true;
                    }
                   // rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    rng.Style.Font.Size = 10;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
                    rng.Style.Font.Color.SetColor(Color.FromArgb(0, 0, 0));
                    if (fila2 - fila1 > 0 || columna2 - columna1 > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }




        public static void CeldaFormatoEtiqueta(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C")
        {
            try
            {

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    //rng.Style.Font.Bold = true;
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    rng.Style.Font.Size = 11;
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    /*
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(255, 255, 255));
                    rng.Style.Font.Color.SetColor(Color.FromArgb(0, 0, 0));

                    */
                    if (fila2 - fila1 > 0 || columna2 - columna1 > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static void CeldaFormatoFondoVerdeColorBlanco(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(84, 130, 53);
            Color texto = Color.FromArgb(255, 255, 255);

            ExcelUtil.CeldaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }


        public static void CeldaFormatoFondoAzulColorBlanco(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C")
        {
            Color fondo = Color.FromArgb(68, 114, 196);
            Color texto = Color.FromArgb(255, 255, 255);

            ExcelUtil.CeldaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto, alineacion);
        }

        public static void CeldaFormatoFondoGrisColorNegro(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(201, 201, 201);
            Color texto = Color.FromArgb(0, 0, 0);

            ExcelUtil.CeldaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }

        public static void CeldaFormatoFondoAzulColorNegro(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(47, 117, 181);
            Color texto = Color.FromArgb(0, 0, 0);

            ExcelUtil.CeldaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }

        public static void CeldaFormatoFondoGrisColorRojo(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0)
        {
            Color fondo = Color.FromArgb(201, 201, 201);
            Color texto = Color.FromArgb(255, 0, 0);

            ExcelUtil.CeldaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto);
        }

        public static void CeldaFormatoFondoCelesteColorNegro(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C")
        {
            Color fondo = Color.FromArgb(163, 226, 232);
            Color texto = Color.FromArgb(0, 0, 0);

            ExcelUtil.CeldaFormato(ws1, valor, fila1, columna1, fila2, columna2, fondo, texto, alineacion);
        }

        public static void CeldaFormato(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2, int columna2, Color colorFondo, Color colorTexto, string alineacion = "C")
        {
            try
            {
                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    else
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }


                    rng.Style.Font.Size = 11;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));

                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;


                    rng.Style.Fill.BackgroundColor.SetColor(colorFondo);
                    rng.Style.Font.Color.SetColor(colorTexto);
                    if (fila2 - fila1 > 0 || columna2 - columna1 > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }





        public static void CeldaFormatoEtiqueta_CabeceraColor(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", bool negrita = false)
        {
            try
            {

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    if (negrita)
                    {
                        rng.Style.Font.Bold = true;
                    }
                   // rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }


                   
                  //  rng.Style.WrapText = true;

                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(141, 180, 226));
                    rng.Style.Font.Size = 11;

                    // rng.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255));

                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));


                    if ((fila2 - fila1) > 0 || (columna2 - columna1) > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }



        public static void CeldaFormatoEtiqueta_Cabecera(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", bool negrita = false)
        {
            try
            {

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    if (negrita)
                    {
                        rng.Style.Font.Bold = true;
                    }
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }


                    // rng.Style.Font.Bold = true;
                    rng.Style.WrapText = true;

                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;

                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 114, 196));
                    rng.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255));


                    rng.Style.Font.Size = 11;

                    // rng.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255));

                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                    rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));


                    if ((fila2 - fila1) > 0 || (columna2 - columna1) > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static void CeldaFormatoEtiqueta_Cabecera_V2(ExcelWorksheet ws1, object valor, int fila1, int columna1, int fila2 = 0, int columna2 = 0, string alineacion = "C", bool negrita = false)
        {
            try
            {

                fila2 = (fila2 == 0 ? fila1 : fila2);
                columna2 = (columna2 == 0 ? columna1 : columna2);
                using (var rng = ws1.Cells[fila1, columna1, fila2, columna2])
                {
                    if (negrita)
                    {
                        rng.Style.Font.Bold = true;
                    }
                    rng.Style.WrapText = true;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (alineacion == "L")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (alineacion == "D")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }
                    if (alineacion == "J")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;
                    }
                    if (alineacion == "C")
                    {
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }


                    // rng.Style.Font.Bold = true;
                   // rng.Style.WrapText = true;

                  //  rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                  //  rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                   // rng.Style.Fill.PatternType = ExcelFillStyle.Solid;

                   // rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 114, 196));
                   // rng.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255));


                    rng.Style.Font.Size = 11;

                    // rng.Style.Font.Color.SetColor(Color.FromArgb(255, 255, 255));

                    rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                   // rng.Style.Border.Top.Color.SetColor(Color.FromArgb(0, 0, 0));
                   // rng.Style.Border.Left.Color.SetColor(Color.FromArgb(0, 0, 0));
                   // rng.Style.Border.Right.Color.SetColor(Color.FromArgb(0, 0, 0));
                  //  rng.Style.Border.Bottom.Color.SetColor(Color.FromArgb(0, 0, 0));


                    if ((fila2 - fila1) > 0 || (columna2 - columna1) > 0)
                    {
                        rng.Merge = true;
                    }
                    rng.Value = valor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }




        #endregion "Configuracion Celda"
    }
}
