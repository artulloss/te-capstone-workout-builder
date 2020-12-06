using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Capstone.Models;

namespace Capstone.DAO
{
    public class ExerciseSqlDAO : IExerciseDAO
    {
        private readonly string _connectionString;
        public ExerciseSqlDAO(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public List<Exercise> GetExercises()
        {
            List<Exercise> exercises = new List<Exercise>();
            try {
                using (SqlConnection conn = new SqlConnection(_connectionString)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM exercises", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        //if (!reader.HasRows) continue;
                        exercises.Add(ReaderToExercise(reader));
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

        public Exercise GetExercise(int id) {
            try {
                using (SqlConnection conn = new SqlConnection(_connectionString)) {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM exercises WHERE exercise_id = @exercise_id", conn);
                    cmd.Parameters.AddWithValue("@exercise_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    return ReaderToExercise(reader);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                throw;
            } 
        }

        public Exercise AddExercise(Exercise exercise)
        {
            Exercise newExercise = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand insertCmd = new SqlCommand(@"INSERT INTO exercises (exercise_name, focus_id, description, weight, time, repetitions, sets, user_id) 
                                                            VALUES (@exercise_name, @focus_id, @description, @weight, @time, @repetitions, @sets, @user_id);", conn);
                    insertCmd.Parameters.AddWithValue("@exercise_name", exercise.ExerciseName);
                    insertCmd.Parameters.AddWithValue("@focus_id", exercise.FocusId);
                    insertCmd.Parameters.AddWithValue("@description", exercise.Description);
                    if(exercise.Weight != null)
                    {
                        insertCmd.Parameters.AddWithValue("@weight", exercise.Weight);
                    }
                    else
                    {
                        insertCmd.Parameters.AddWithValue("@weight", DBNull.Value);
                    }
                    insertCmd.Parameters.AddWithValue("@time", exercise.Time);
                    if (exercise.Repetitions != null)
                    {
                        insertCmd.Parameters.AddWithValue("@repetitions", exercise.Repetitions);
                    }
                    else
                    {
                        insertCmd.Parameters.AddWithValue("@repetitions", DBNull.Value);
                    }
                    if (exercise.Sets != null)
                    {
                        insertCmd.Parameters.AddWithValue("@sets", exercise.Sets);
                    }
                    else
                    {
                        insertCmd.Parameters.AddWithValue("@sets", DBNull.Value);
                    }                    
                    insertCmd.Parameters.AddWithValue("@user_id", exercise.UserId);

                    int numRowsAffected = insertCmd.ExecuteNonQuery();
                    if(numRowsAffected != 1) 
                    {
                        throw new Exception("Rows affected was not 1.");
                    }
                    newExercise = exercise;

                    SqlCommand selectCmd = new SqlCommand("SELECT TOP 1 @@IDENTITY FROM users", conn);
                    newExercise.ExerciseId = Convert.ToInt32(selectCmd.ExecuteScalar());
                    return newExercise;                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return newExercise;
            }
        }

        public  static Exercise ReaderToExercise(SqlDataReader reader) 
        {
            return new Exercise 
            {
                ExerciseId = Convert.ToInt32(reader["exercise_id"]),
                ExerciseName = Convert.ToString(reader["exercise_name"]),
                FocusId = Convert.ToInt32(reader["focus_id"]),
                Description = Convert.ToString(reader["description"]), 
                Weight = (int?) (reader["weight"] != DBNull.Value ? reader["weight"] : null),
                Time = Convert.ToInt32(reader["time"]), 
                Repetitions = (int?) (reader["repetitions"] != DBNull.Value ? reader["repetitions"] : null),
                Sets = (int?) (reader["sets"] != DBNull.Value ? reader["sets"] : null),
                UserId = Convert.ToInt32(reader["user_id"])
            };
        }
    }
}