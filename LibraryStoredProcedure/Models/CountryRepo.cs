using Dapper;
using LibraryStoredProcedure.Connections;
using LibraryStoredProcedure.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStoredProcedure.Models
{
    public class CountryRepo
    {
        public List<Country> GetCountries()
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var countries = connection.Query<Country>(StoredProcs.GetAllCountries.ToString(),
                    commandType: CommandType.StoredProcedure).ToList();
                return countries;
            }
        }

    }
}
