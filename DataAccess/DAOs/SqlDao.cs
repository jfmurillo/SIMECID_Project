using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    // Singleton data access object — executes stored procedures against the database.
    // Call SqlDao.Configure(connectionString) at application startup before first use.
    public class SqlDao
    {
        private static string _connectionString = string.Empty;
        private static SqlDao? _instance;

        private SqlDao()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException(
                    "SqlDao has not been configured. Call SqlDao.Configure(connectionString) at startup.");
        }

        public static void Configure(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be empty.", nameof(connectionString));
            _connectionString = connectionString;
            _instance = null; // Reset singleton so next GetInstance() uses the new string
        }

        public static SqlDao GetInstace()
        {
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            return _instance;
        }

        //Paso 4 Metodo que nos va a permitir conectarnos a la base de datos y ejecutar el StoreProcedure(SP)
        public void ExecuteProcedure(SqlOperation sqlOperation)

        {
            //Aqui estamos definiedo la instancia del objeto de la conexion
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure //hace el store procedure en vez de hacer un query en la bd
                    //Incluye los campos en la tabla
                }
                )
                {
                    //Agregar los parametros a la ejecucion 
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    //Ejecutar la sentencia "contra la base de datos"
                    conn.Open();
                    command.ExecuteNonQuery();//Por que no vamos a esperar una respuesta
                }
            }
        }
        //Metodo para recuper info desde la base de datos
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            var lstResults = new List<Dictionary<string, object>>();

            //Aqui estamos definiedo la instancia del objeto de la conexion
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure //hace el store procedure en vez de hacer un query en la bd
                    //Incluye los campos en la tabla
                }
                )
                {
                    //Agregar los parametros a la ejecucion 
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    //Ejecutar la sentencia "contra la base de datos"
                    conn.Open();

                    //Aqui varia el metodo respecto al anterior a partir de este punto 
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var rowDict = new Dictionary<string, object>(); //Por cada fila un diccionario
                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);
                                rowDict[key] = value;
                            }
                            lstResults.Add(rowDict);
                        }
                    }
                }
            }


            return lstResults;

        }
    }
}
