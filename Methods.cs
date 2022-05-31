using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class_Management_System
{
    public static class Methods
    {
        public static SqlConnection MyMethod()
        {
            var connection = new SqlConnection(Dapper.Connection);
            connection.Open();
            return connection;

        }
        public static IEnumerable<T> RunQuery<T>(string sql, object parameter = null)
        {
            using (var con = MyMethod())
            {
                return con.Query<T>(sql, parameter);
            }

        }

        

        public static string GetID(string sql, object parameter = null)
        {
            var con = MyMethod();
            var rowsAffected = con.ExecuteReader(sql, parameter);
            rowsAffected.Read();
            var idas = rowsAffected["count"].ToString();
            rowsAffected.Close();
            return idas;


        }



    }
}
