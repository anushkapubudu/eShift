using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace eShift.Repository
{
    class JobRepository : IJobRepository
    {
        private readonly string _connectionString;

        public JobRepository()
        {
            _connectionString = DbConst.ConnectionString;
        }

        public void CreateJob(Job job)
        {
            var query = @"INSERT INTO Job 
                        (JobNumber, CustomerId, PickupLocation, DropoffLocation, JobDescription, StartDate, EndDate, JobStatus, CreatedAt)
                        VALUES
                        (@JobNumber, @CustomerId, @PickupLocation, @DropoffLocation, @JobDescription, @StartDate, @EndDate, @JobStatus, SYSDATETIME())";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobNumber", job.JobNumber);
                cmd.Parameters.AddWithValue("@CustomerId", job.CustomerId);
                cmd.Parameters.AddWithValue("@PickupLocation", job.PickupLocation);
                cmd.Parameters.AddWithValue("@DropoffLocation", job.DropoffLocation);
                cmd.Parameters.AddWithValue("@JobDescription", job.JobDescription);
                cmd.Parameters.AddWithValue("@StartDate", job.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", job.EndDate);
                cmd.Parameters.AddWithValue("@JobStatus", job.JobStatus.ToString());

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Job GetJobById(int jobId)
        {
            var query = @"SELECT * FROM Job WHERE JobId = @JobId AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobId", jobId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return MapJob(reader);
                }
            }
        }

        public List<Job> GetJobsByCustomerId(int customerId)
        {
            var jobs = new List<Job>();
            var query = @"SELECT * FROM Job WHERE CustomerId = @CustomerId AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        jobs.Add(MapJob(reader));
                }
            }

            return jobs;
        }

        public List<Job> GetJobRequestsByCustomerId(int customerId)
        {
            var jobs = new List<Job>();

            var query = @"
                        SELECT j.*, (u.FirstName + ' ' + u.LastName) AS CustomerName
                        FROM Job j
                        INNER JOIN Users u ON j.CustomerId = u.UserId
                        WHERE j.CustomerId = @CustomerId
                        AND j.JobStatus IN ('Pending', 'Draft')
                        AND j.DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CustomerId", customerId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        jobs.Add(MapJob(reader)); 
                }
            }

            return jobs;
        }


        public List<Job> GetAllJobs()
        {
            var jobs = new List<Job>();
            var query = @"
                        SELECT j.*, (u.FirstName + ' ' + u.LastName) AS CustomerName
                        FROM Job j
                        INNER JOIN Users u ON j.CustomerId = u.UserId
                        WHERE j.DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        jobs.Add(MapJob(reader));
                }
            }

            return jobs;
        }


        public bool UpdateJob(Job updatedJob)
        {
            var query = @"UPDATE Job SET 
                            PickupLocation = @PickupLocation,
                            DropoffLocation = @DropoffLocation,
                            JobDescription = @JobDescription,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            UpdatedAt = SYSDATETIME()
                          WHERE JobId = @JobId AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PickupLocation", updatedJob.PickupLocation);
                cmd.Parameters.AddWithValue("@DropoffLocation", updatedJob.DropoffLocation);
                cmd.Parameters.AddWithValue("@JobDescription", updatedJob.JobDescription);
                cmd.Parameters.AddWithValue("@StartDate", updatedJob.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", updatedJob.EndDate);
                cmd.Parameters.AddWithValue("@JobId", updatedJob.JobId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateJobStatus(int jobId, JobStatus newStatus)
        {
            var query = @"UPDATE Job SET 
                            JobStatus = @JobStatus,
                            UpdatedAt = SYSDATETIME()
                          WHERE JobId = @JobId AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobStatus", newStatus.ToString());
                cmd.Parameters.AddWithValue("@JobId", jobId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int GetLastJobId()
        {
            var query = @"SELECT ISNULL(MAX(JobId), 0) FROM Job";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void DeleteJob(int jobId)
        {
            var query = "UPDATE Job SET DeletedAt = GETDATE() WHERE JobId = @JobId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobId", jobId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        private Job MapJob(SqlDataReader reader)
        {
            return new Job
            {
                JobId = (int)reader["JobId"],
                JobNumber = reader["JobNumber"].ToString(),
                CustomerId = (int)reader["CustomerId"],
                PickupLocation = reader["PickupLocation"].ToString(),
                DropoffLocation = reader["DropoffLocation"].ToString(),
                JobDescription = reader["JobDescription"].ToString(),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"]),
                JobStatus = reader["JobStatus"].ToString(),
                CustomerName = reader["CustomerName"].ToString(),
            };
        }
    }
}
