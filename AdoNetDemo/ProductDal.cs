using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb;initial catalog=ETrade; integrated security=true");
        #region DataTable
        public DataTable GetAll2()
        {
            //Sql database'imiz ile bağlantı kuruyoruz.
            //we connect with our sql database.

            ConnectionControl();
            //Sql Komutu oluşturduk.
            SqlCommand command = new SqlCommand("Select * from Products", _connection);
            //Sql reader ile komutu okuttuk.
            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            _connection.Close();
            return dataTable;
        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        #endregion

        #region ListVersion
        public List<Product> GetAll()
        {
            //Sql database'imiz ile bağlantı kuruyoruz.
            //we connect with our sql database.

            ConnectionControl();
            //Sql Komutu oluşturduk.
            SqlCommand command = new SqlCommand("Select * from Products", _connection);
            //Sql reader ile komutu okuttuk.
            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                };
                products.Add(product);
            }
            reader.Close();
            _connection.Close();
            return products;
        }
        #endregion

        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("Insert into Products (Id,Name,UnitePrice,StockAmount) Values('" + product.Id + "','" + product.Name + "-" + product.UnitPrice + "-" + product.StockAmount + "')", _connection);

            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }


}
