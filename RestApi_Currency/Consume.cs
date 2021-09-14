using System;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestApi
{
    public class Consume
    {
        static NpgsqlConnection con;
        static string cs;

        static Consume()
        {
            Consume.cs = "Host=localhost;Username=selen;Password=admin;Database=fistik";
            con = new NpgsqlConnection(cs);
            con.Open();
        } 
        public void Insert(string currency, int amount, string operation)
        {
            string sql = "INSERT INTO transactions_table(currency, amount, operation) VALUES(@currency, @amount, @operation)";
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("currency", currency);
            cmd.Parameters.AddWithValue("amount", amount);
            cmd.Parameters.AddWithValue("operation", operation);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public void Delete(string currency)
        {
            string sql = "DELETE FROM transactions_table WHERE currency = @currency";
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("currency", currency);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}
