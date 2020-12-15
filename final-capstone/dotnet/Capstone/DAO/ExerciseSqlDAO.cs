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
                    while (reader.Read())
                    {
                        return ReaderToExercise(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
               
            }
            return null;
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
                    insertCmd.Parameters.AddWithValue("@weight", exercise.Weight ?? (object) DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@time", exercise.Time);
                    insertCmd.Parameters.AddWithValue("@repetitions", exercise.Repetitions ?? (object)DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@sets", exercise.Sets ?? (object)DBNull.Value);
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
        public Exercise EditExercise(Exercise exercise)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand insertCmd = new SqlCommand(@"UPDATE exercises SET exercise_name = @exercise_name, focus_id=@focus_id, description=@description, weight=@weight, time=@time, repetitions=@repetitions,
                                                            sets=@sets, user_id=@user_id WHERE exercise_id = @exercise_id", conn);
                    insertCmd.Parameters.AddWithValue("@exercise_name", exercise.ExerciseName);
                    insertCmd.Parameters.AddWithValue("@focus_id", exercise.FocusId);
                    insertCmd.Parameters.AddWithValue("@description", exercise.Description);
                    insertCmd.Parameters.AddWithValue("@weight", exercise.Weight ?? (object)DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@time", exercise.Time);
                    insertCmd.Parameters.AddWithValue("@repetitions", exercise.Repetitions ?? (object)DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@sets", exercise.Sets ?? (object)DBNull.Value);
                    insertCmd.Parameters.AddWithValue("@user_id", exercise.UserId);
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
            }
            return null;
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

        public bool DeleteExercise(int exerciseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand deleteUserExerciseCmd = new SqlCommand(@"DELETE FROM userExercises WHERE exercise_id = @exercise_id", conn);
                    SqlCommand deleteCmd = new SqlCommand(@"DELETE FROM exercises WHERE exercise_id = @exercise_id", conn);
                    deleteCmd.Parameters.AddWithValue("@exercise_id", exerciseId);
                    deleteUserExerciseCmd.Parameters.AddWithValue("@exercise_id", exerciseId);

                    deleteUserExerciseCmd.ExecuteNonQuery();
                    int numRowsAffectedExercise = deleteCmd.ExecuteNonQuery();
                    return (numRowsAffectedExercise == 1);

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