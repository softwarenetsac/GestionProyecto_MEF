
using Gestion_Rendimiento_Common;
using Gestion_Rendimiento_Data;
using Gestion_Rendimiento_Entity;
using Gestion_Rendimiento_IService;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
namespace Gestion_Rendimiento_Service
{
    public class UsuarioService : IUsuarioService
    {

        private IConfiguration _configuration;
        protected readonly DatabaseContext _context;
        protected BaseResponse baseResponse;
        protected  OracleTransaction _transaccion = null;
        public UsuarioService(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }


        public List<Modulo> Modulo(int id_usuario, int id_sistema)
        {
            List<Modulo> lista = new List<Modulo>();
            OracleConnection connection = null;
            try
            {
                string connString = _configuration.GetConnectionString("DefaultConnection");
                connection = new OracleConnection(connString);
                using (OracleCommand command = new OracleCommand())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        OracleParameter[] parametros = null;
                        parametros = new OracleParameter[3];
                        parametros[0] = new OracleParameter("PI_ID_USUARIO", id_usuario);
                        parametros[1] = new OracleParameter("PI_ID_SISTEMA", id_sistema);
                        parametros[2] = new OracleParameter("PI_Results", OracleDbType.RefCursor);
                        parametros[2].Direction = ParameterDirection.Output;
                        command.Parameters.AddRange(parametros);
                        command.CommandText = "SEGURIDAD.PCK_SEEGURIDAD_LDAP.USP_MENU_USUARIO";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var result = new Modulo();
                            result.ID_SISTEMA_MODULO = reader["ID_SISTEMA_MODULO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_SISTEMA_MODULO"].ToString());
                            result.ID_SISTEMA_MODULO_PADRE = reader["ID_SISTEMA_MODULO_PADRE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_SISTEMA_MODULO_PADRE"].ToString());
                            result.ID_TIPO_MODULO = reader["ID_TIPO_MODULO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_TIPO_MODULO"].ToString());
                            result.DESC_MODULO = reader["DESC_MODULO"] == DBNull.Value ? "" : reader["DESC_MODULO"].ToString();
                            result.IMAGEN = reader["IMAGEN"] == DBNull.Value ? "" : reader["IMAGEN"].ToString();
                            result.URL_MODULO = reader["URL_MODULO"] == DBNull.Value ? "" : reader["URL_MODULO"].ToString();
                            result.ORDEN = reader["ORDEN"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ORDEN"].ToString());
                            lista.Add(result);
                        }
                        reader.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                Log.CreateLogger(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lista;
        }

        public UsuarioSesion Usuario_Detalle(string usuario, int id_sistema)
        {
            UsuarioSesion result = null;
            OracleConnection connection = null;
            try
            {
                string connString = _configuration.GetConnectionString("DefaultConnection");
                connection = new OracleConnection(connString);
                using (OracleCommand command = new OracleCommand())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        OracleParameter[] parametros = null;
                        parametros = new OracleParameter[3];
                        parametros[0] = new OracleParameter("PI_ID_SISTEMA", id_sistema);
                        parametros[1] = new OracleParameter("PI_USUARIO", usuario);
                        parametros[2] = new OracleParameter("PI_Results", OracleDbType.RefCursor);
                        parametros[2].Direction = ParameterDirection.Output;
                        command.Parameters.AddRange(parametros);
                        command.CommandText = "SEGURIDAD.PCK_SEEGURIDAD_LDAP.USP_USUARIO_DETALLE";

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new UsuarioSesion();
                            result.ID_USUARIO = reader["ID_USUARIO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_USUARIO"].ToString());
                            result.UsuarioLogin = reader["LOGIN_USUARIO"] == DBNull.Value ? "" : reader["LOGIN_USUARIO"].ToString();
                            result.ID_PERSONAL = reader["ID_PERSONA"] == DBNull.Value ? "" : reader["ID_PERSONA"].ToString();
                            result.ID_OFICINA = reader["ID_OFICINA"] == DBNull.Value ? "" : reader["ID_OFICINA"].ToString();
                            //result.ID_AREA = reader["ID_AREA"] == DBNull.Value ? "" : reader["ID_AREA"].ToString();
                            result.Nombre = reader["NOMBRE_PERSONA"] == DBNull.Value ? "" : reader["NOMBRE_PERSONA"].ToString();
                            result.NOMBRE_OFICINA = reader["NOMBRE_OFICINA"] == DBNull.Value ? "" : reader["NOMBRE_OFICINA"].ToString();
                            result.SIGLA = reader["SIGLA"] == DBNull.Value ? "" : reader["SIGLA"].ToString();
                        }
                        reader.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                Log.CreateLogger(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return result;
        }



        public List<Modulo> Modulo_x_Rol(string nombre_rol, int id_sistema)
        {
            List<Modulo> lista = new List<Modulo>();
            OracleConnection connection = null;
            try
            {
                string connString = _configuration.GetConnectionString("DefaultConnection");
                connection = new OracleConnection(connString);
                using (OracleCommand command = new OracleCommand())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        OracleParameter[] parametros = null;
                        parametros = new OracleParameter[3];
                        parametros[0] = new OracleParameter("PI_NOMBRE_ROL", nombre_rol);
                        parametros[1] = new OracleParameter("PI_ID_SISTEMA", id_sistema);
                        parametros[2] = new OracleParameter("PI_Results", OracleDbType.RefCursor);
                        parametros[2].Direction = ParameterDirection.Output;
                        command.Parameters.AddRange(parametros);
                        command.CommandText = "SEGURIDAD.PCK_SEEGURIDAD_LDAP.USP_MENU_X_ROL_DEFECTO";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var result = new Modulo();
                            result.ID_SISTEMA_MODULO = reader["ID_SISTEMA_MODULO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_SISTEMA_MODULO"].ToString());
                            result.ID_SISTEMA_MODULO_PADRE = reader["ID_SISTEMA_MODULO_PADRE"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_SISTEMA_MODULO_PADRE"].ToString());
                            result.ID_TIPO_MODULO = reader["ID_TIPO_MODULO"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ID_TIPO_MODULO"].ToString());
                            result.DESC_MODULO = reader["DESC_MODULO"] == DBNull.Value ? "" : reader["DESC_MODULO"].ToString();
                            result.IMAGEN = reader["IMAGEN"] == DBNull.Value ? "" : reader["IMAGEN"].ToString();
                            result.URL_MODULO = reader["URL_MODULO"] == DBNull.Value ? "" : reader["URL_MODULO"].ToString();
                            result.ORDEN = reader["ORDEN"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ORDEN"].ToString());
                            lista.Add(result);
                        }
                        reader.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

                Log.CreateLogger(ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }

            return lista;
        }


        public BaseResponse GenerarUsuario(UsuarioSeguridad item)
        {
            baseResponse = new BaseResponse();
            baseResponse.Success = false;
            OracleConnection connection = null;
            try
            {
                string connString = _configuration.GetConnectionString("DefaultConnection");
                connection = new OracleConnection(connString);
                using (OracleCommand command = new OracleCommand())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    _transaccion = connection.BeginTransaction();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    OracleParameter[] parametros = new OracleParameter[5];
                    parametros[0] = new OracleParameter("PI_ID_OFICINA", item.ID_OFICINA);
                    parametros[1] = new OracleParameter("PI_ID_PERSONA", item.ID_PERSONA);
                    parametros[2] = new OracleParameter("PI_NOMBRE_ROL", item.NOMBRE_ROL);
                    parametros[3] = new OracleParameter("PI_ID_SISTEMA", item.ID_SISTEMA);
                    parametros[4] = new OracleParameter("PI_USU_CREACION", item.USU_CREACION);
                    command.Parameters.AddRange(parametros);
                    command.CommandText = "SEGURIDAD.PCK_SEEGURIDAD_LDAP.USP_USUARIO_INSERTAR";
                    var code = command.ExecuteNonQuery();
                    _transaccion.Commit();
                    baseResponse.Success = true;
                    baseResponse.Message = "se ha Procesado Correctamente";
                }
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = ex.Message;
                Log.CreateLogger(ex.Message);
            }
            finally
            {

                if (connection != null)
                {
                    connection.Close();
                }
            }
            return baseResponse;
        }



        public BaseResponse AnularUsuario(UsuarioSeguridad item)
        {
            baseResponse = new BaseResponse();
            baseResponse.Success = false;
            OracleConnection connection = null;
            try
            {
                string connString = _configuration.GetConnectionString("DefaultConnection");
                connection = new OracleConnection(connString);
                using (OracleCommand command = new OracleCommand())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    _transaccion = connection.BeginTransaction();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    OracleParameter[] parametros = new OracleParameter[3];
                    parametros[0] = new OracleParameter("PI_ID_PERSONA", item.ID_PERSONA);
                    parametros[1] = new OracleParameter("PI_NOMBRE_ROL", item.NOMBRE_ROL);
                    parametros[2] = new OracleParameter("PI_ID_SISTEMA", item.ID_SISTEMA);
                    command.Parameters.AddRange(parametros);
                    command.CommandText = "SEGURIDAD.PCK_SEEGURIDAD_LDAP.USP_ANULAR_ROL";
                    var code = command.ExecuteNonQuery();
                    _transaccion.Commit();
                    baseResponse.Success = true;
                    baseResponse.Message = "se ha Procesado Correctamente";
                }
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = ex.Message;
                Log.CreateLogger(ex.Message);
            }
            finally
            {

                if (connection != null)
                {
                    connection.Close();
                }
            }
            return baseResponse;
        }

    }
}
