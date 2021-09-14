using System;
using Npgsql;

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
        public void Insert(string task, bool isComplete, string operation)
        {
            string sql = "INSERT INTO consumerr(task, isComplete, operation) VALUES(@task, @isComplete, @operation)";
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("task", task);
            cmd.Parameters.AddWithValue("isComplete", isComplete);
            cmd.Parameters.AddWithValue("operation", operation);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
        public void Delete(string task)
        {
            string sql = "DELETE FROM consumerr WHERE task = @task";
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("task", task);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }
    }
}

