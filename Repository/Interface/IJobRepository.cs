using eShift.Model;
using eShift.Utilities;
using System.Collections.Generic;

namespace eShift.Repository.Interface
{
    interface IJobRepository
    {
        void CreateJob(Job job);                     // Insert new job
        Job GetJobById(int jobId);                   // Retrieve job by primary key
        List<Job> GetJobsByCustomerId(int customerId);       // All jobs for a customer
        List<Job> GetJobRequestsByCustomerId(int customerId); // Jobs with status = Pending, etc.
        List<Job> GetAllJobs();                      // Admin-level view
        bool UpdateJob(Job updatedJob);              // Modify pickup/dropoff/description
        bool UpdateJobStatus(int jobId, JobStatus newStatus); // Update only status
        int GetLastJobId();                          // For auto-generating JobNumber
    }
}
