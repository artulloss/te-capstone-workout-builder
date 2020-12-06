using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Capstone.DAO
{
    public class FocusSqlDAO : IFocusDAO
    {
        private readonly string _connectionString;
        public FocusSqlDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Focus GetFocus(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM focuses WHERE focus_id = @focus_id", conn);
                    cmd.Parameters.AddWithValue("@focus_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    return ReaderToFocus(reader);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<Focus> GetFocuses()
        {
            List<Focus> focuses = new List<Focus>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM focuses", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //if (!reader.HasRows) continue;
                        focuses.Add(ReaderToFocus(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return focuses;
        }

        private Focus ReaderToFocus(SqlDataReader reader)
        {
            return new Focus
            {
                FocusId = Convert.ToInt32(reader["focus_id"]),
                FocusName = Convert.ToString(reader["focus_name"])
            };
        }
    }
}
