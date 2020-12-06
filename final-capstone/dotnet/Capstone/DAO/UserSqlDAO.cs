using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly string _connectionString;

        public UserSqlDAO(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public List<User> GetUsers() {
            List<User> users = new List<User>();
            try {
                using (SqlConnection conn = new SqlConnection(_connectionString)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM users", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        //if (!reader.HasRows) continue;
                        users.Add(GetUserFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }
            return users;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return returnUser;
        }


        public User AddUser(string username, string password, string role)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (username, password_hash, salt, user_role) VALUES (@username, @password_hash, @salt, @user_role)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", role);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return GetUser(username);
        }

        private List<Exercise> GetExercises(string username, string query) {
            List<Exercise> exercises = new List<Exercise>();
            try {
                using (SqlConnection conn = new SqlConnection(_connectionString)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        query, conn
                    );
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        exercises.Add(ExerciseSqlDAO.ReaderToExercise(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return exercises;
        }

        public List<Exercise> GetUserExercises(string username) {
            return GetExercises(username, 
            @"SELECT e.* FROM exercises e
                    INNER JOIN userExercises ue ON e.exercise_id = ue.exercise_id
                    INNER JOIN users u ON u.user_id = ue.user_id
                    WHERE u.username = @username");
        }

        public List<Exercise> GetTrainerExercises(string username) {
            return GetExercises(username,
                @"SELECT * FROM exercises WHERE user_id = (SELECT user_id FROM users WHERE username = @username)");
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
                Role = Convert.ToString(reader["user_role"]),
            };

            return u;
        }
    }
}
