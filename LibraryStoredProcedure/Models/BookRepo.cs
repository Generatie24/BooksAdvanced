using Dapper;
using LibraryStoredProcedure.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryStoredProcedure.Enums;

namespace LibraryStoredProcedure.Models
{
    public class BookRepo
    {
        private GenericRepo repo = new GenericRepo();
        public void AddBook(Book book)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Title", book.Title);
            param.Add("@Author", book.Author);
            param.Add("@Price", book.Price);
            param.Add("@Description", book.Description);
            param.Add("@CountryId", book.CountryId);

            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute(StoredProcs.AddBook.ToString(), param, commandType: CommandType.StoredProcedure);
            }


           

        }

        public List<Book> GetBooks()
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                var books = connection.Query<Book>(StoredProcs.GetAllBooks.ToString(),
                    commandType: CommandType.StoredProcedure).ToList();
                return books;
            }
        }

        public void DeleteBook(Book book)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", book.Id);
            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute(StoredProcs.DeleteBook.ToString(), param, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateBook(Book book)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", book.Id);
            param.Add("@Title", book.Title);
            param.Add("@Author", book.Author);
            param.Add("@Price", book.Price);
            param.Add("@Description", book.Description);
            param.Add("@CountryId", book.CountryId);

            using (IDbConnection connection = new SqlConnection(Helper.ConStr("Books")))
            {
                connection.Execute(StoredProcs.UpdateBook.ToString(), param, commandType: CommandType.StoredProcedure);
            }

        }

    }
}
