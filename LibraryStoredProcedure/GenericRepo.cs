using LibraryStoredProcedure.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStoredProcedure
{
    public class GenericRepo
    {
        public List<T> LoadInfo<T, U>(string sqlstatement, U parameters)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                List<T> result = connection.Query<T>(sqlstatement, parameters).ToList();
                return result;
            }
        }

        public void SaveInfo<T>(string sqlstatement, T parameters)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute(sqlstatement, parameters);
            }
        }
    }
}
