using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;

namespace eShift.Business.Services
{
    class JobService : IJobService
    {
        private readonly IJobRepository _jobRepo;

        public JobService(IJobRepository jobRepo)
        {
            _jobRepo = jobRepo;
        }

        public void CreateJob(Job job)
        {
            if (string.IsNullOrWhiteSpace(job.PickupLocation) ||
                string.IsNullOrWhiteSpace(job.DropoffLocation) ||
                string.IsNullOrWhiteSpace(job.JobDescription) ||
                job.CustomerId <= 0)
                throw new Exception("Missing required job fields.");

            job.JobNumber = GenerateJobNumber();
            job.JobStatus = JobStatus.Pending.ToString();

            _jobRepo.CreateJob(job);
        }

        public Job GetJobById(int jobId)
        {
            var job = _jobRepo.GetJobById(jobId);
            if (job == null)
                throw new Exception($"Job with ID {jobId} not found.");

            return job;
        }

        public List<Job> GetJobsByCustomerId(int customerId)
        {
            return _jobRepo.GetJobsByCustomerId(customerId);
        }

        public List<Job> GetJobRequestsByCustomerId(int customerId)
        {
            return _jobRepo.GetJobRequestsByCustomerId(customerId);
        }

        public List<Job> GetAllJobs()
        {
            return _jobRepo.GetAllJobs();
        }

        public void UpdateJobDetails(Job updatedJob)
        {
            if (string.IsNullOrWhiteSpace(updatedJob.PickupLocation) ||
                string.IsNullOrWhiteSpace(updatedJob.DropoffLocation) ||
                string.IsNullOrWhiteSpace(updatedJob.JobDescription))
                throw new Exception("Missing required job fields.");

            bool success = _jobRepo.UpdateJob(updatedJob);
            if (!success)
                throw new Exception("Job update failed.");
        }

        public void ChangeJobStatus(int jobId, JobStatus newStatus)
        {
            bool success = _jobRepo.UpdateJobStatus(jobId, newStatus);
            if (!success)
                throw new Exception($"Unable to update status for Job ID {jobId}.");
        }

        private string GenerateJobNumber()
        {
            int latestId = _jobRepo.GetLastJobId();
            return $"JOB{latestId + 1:0000}";
        }
    }
}
