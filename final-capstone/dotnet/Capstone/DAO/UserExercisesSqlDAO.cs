using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class UserExercisesSqlDAO : IUserExerciseDAO
    {
        private readonly string _connectionString;
        public UserExercisesSqlDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Exercise AddUserExercise(Exercise exercise, string username)
       {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand insertCmd = new SqlCommand(@"INSERT INTO userExercises VALUES ((SELECT user_id FROM users WHERE username = @username), @exercise_id);", conn);
                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@exercise_id", exercise.ExerciseId);

                    int numRowsAffected = insertCmd.ExecuteNonQuery();
                    if (numRowsAffected != 1)
                    {
                        throw new Exception("Rows affected was not 1.");
                    }                 
                    
                    return exercise;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool DeleteAllUserExercises(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand insertCmd = new SqlCommand("DELETE FROM userExercises WHERE user_id = (SELECT user_id FROM users WHERE username = @username)", conn);
                    insertCmd.Parameters.AddWithValue("@username", username);

                    int numRowsAffected = insertCmd.ExecuteNonQuery();
                    return (numRowsAffected >= 1);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }
    }
}
