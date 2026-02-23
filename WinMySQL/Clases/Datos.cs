using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace WinMySQL.Clases
{
    internal class Datos
    {
        string cadenaConexion = "server=localhost;user=ale;pwd=ale123";
        MySqlConnection conexion;
        private void Conectar() {
            try
            {
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
        }
        private void Desconectar()
        {
            try
            {
                if (conexion != null) { 
                    conexion.Close(); 
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                
            }
        }
    }
}
