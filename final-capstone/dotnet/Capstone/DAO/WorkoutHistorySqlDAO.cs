﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class WorkoutHistorySqlDAO : IWorkoutHistoryDAO
    {
        private readonly string _connectionString;
        public WorkoutHistorySqlDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        public WorkoutHistory GetWorkoutHistory(int id, DateTime dateTime)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM workoutHistory WHERE user_id = @user_id AND date = @date", conn);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    cmd.Parameters.AddWithValue("@date", dateTime);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        return ReaderToWorkoutHistory(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
        public List<WorkoutHistory> GetWorkoutHistory(int id)
        {
            List<WorkoutHistory> workoutHistories = new List<WorkoutHistory>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM workoutHistory WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", id);                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        workoutHistories.Add(ReaderToWorkoutHistory(reader));
                    }                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            return workoutHistories;
        }

        public WorkoutHistory AddWorkoutHistory(WorkoutHistory workoutHistory)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand insertCmd = new SqlCommand(@"INSERT INTO workoutHistory VALUES ( @user_id, @time, @date )", conn);
                    insertCmd.Parameters.AddWithValue("@user_id", workoutHistory.UserId);
                    insertCmd.Parameters.AddWithValue("@time", workoutHistory.Time);
                    insertCmd.Parameters.AddWithValue("@date", workoutHistory.Date);

                    int numRowsAffected = insertCmd.ExecuteNonQuery();
                    if (numRowsAffected != 1)
                    {
                        throw new Exception("Rows affected was not 1.");
                    }
                    return workoutHistory;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public WorkoutHistory EditWorkoutHistory(WorkoutHistory workoutHistory)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand insertCmd = new SqlCommand(@"UPDATE workoutHistory SET time = @time
                                                            WHERE user_id = @user_id AND date = @date", conn);
                    insertCmd.Parameters.AddWithValue("@user_id", workoutHistory.UserId);
                    insertCmd.Parameters.AddWithValue("@time", workoutHistory.Time);
                    insertCmd.Parameters.AddWithValue("@date", workoutHistory.Date);

                    int numRowsAffected = insertCmd.ExecuteNonQuery();
                    if (numRowsAffected != 1)
                    {
                        throw new Exception("Rows affected was not 1.");
                    }
                    return workoutHistory;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
        public static WorkoutHistory ReaderToWorkoutHistory(SqlDataReader reader)
        {
            return new WorkoutHistory
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Time = Convert.ToInt32(reader["time"]),
                Date = Convert.ToDateTime(reader["date"]),
            };
        }
    }
}
