using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace proyecto_final_iujo.Logica
{
    public class GestorBaseDatos
    {
        private string _cadenaConexion;
        private string _proveedor;

        // Por defecto apunta a MySQL local (puedes cambiar a System.Data.SqlClient si usas SQL Server)
        public GestorBaseDatos(
            string cadenaConexion = "Server=localhost;Database=bd_juego_iujo;Uid=root;Pwd=;",
            string proveedor = "MySql.Data.MySqlClient")
        {
            _cadenaConexion = cadenaConexion;
            _proveedor = proveedor;
        }

        private DbConnection CrearConexion()
        {
            try
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(_proveedor);
                DbConnection conexion = factory.CreateConnection();
                conexion.ConnectionString = _cadenaConexion;
                return conexion;
            }
            catch
            {
                // Fallback directo a SqlConnection si no está instalado el conector de MySQL en la máquina
                return new SqlConnection(_cadenaConexion);
            }
        }

        public int RegistrarUsuario(string nombreUsuario)
        {
            using (DbConnection con = CrearConexion())
            {
                con.Open();
                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Usuarios (NombreUsuario) VALUES (@nombre);";
                    AgregarParametro(cmd, "@nombre", nombreUsuario);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "SELECT MAX(IdUsuario) FROM Usuarios;";
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public void GuardarEquipo(int idUsuario, string nombreEquipo, int idCarta1, int idCarta2, int idCarta3)
        {
            using (DbConnection con = CrearConexion())
            {
                con.Open();
                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Equipos (IdUsuario, NombreEquipo, IdCarta1, IdCarta2, IdCarta3) 
                                        VALUES (@usr, @nom, @c1, @c2, @c3);";
                    AgregarParametro(cmd, "@usr", idUsuario);
                    AgregarParametro(cmd, "@nom", nombreEquipo);
                    AgregarParametro(cmd, "@c1", idCarta1);
                    AgregarParametro(cmd, "@c2", idCarta2);
                    AgregarParametro(cmd, "@c3", idCarta3);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarEstadistica(int idUsuario, bool esVictoria)
        {
            using (DbConnection con = CrearConexion())
            {
                con.Open();
                using (DbCommand cmd = con.CreateCommand())
                {
                    string campoSuma = esVictoria ? "Victorias = Victorias + 1" : "Derrotas = Derrotas + 1";
                    cmd.CommandText = string.Format(
                        "UPDATE Estadisticas SET {0}, PartidasJugadas = PartidasJugadas + 1 WHERE IdUsuario = @usr;",
                        campoSuma);
                    AgregarParametro(cmd, "@usr", idUsuario);
                    int filas = cmd.ExecuteNonQuery();

                    if (filas == 0)
                    {
                        cmd.CommandText = @"INSERT INTO Estadisticas (IdUsuario, Victorias, Derrotas, PartidasJugadas) 
                                            VALUES (@usr, @vic, @der, 1);";
                        AgregarParametro(cmd, "@vic", esVictoria ? 1 : 0);
                        AgregarParametro(cmd, "@der", esVictoria ? 0 : 1);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public DataTable ObtenerCartasRegistradas()
        {
            DataTable tabla = new DataTable();
            using (DbConnection con = CrearConexion())
            {
                con.Open();
                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT IdCarta, Nombre, Elemento, VidaBase, AtaqueBase, DefensaBase, Mitologia FROM Cartas;";
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            return tabla;
        }

        private void AgregarParametro(DbCommand cmd, string nombre, object valor)
        {
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = nombre;
            param.Value = valor ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }
    }
}