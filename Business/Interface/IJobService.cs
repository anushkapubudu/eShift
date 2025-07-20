using eShift.Model;
using eShift.Utilities;
using System.Collections.Generic;

namespace eShift.Business.Interface
{
    interface IJobService
    {
        void CreateJob(Job job);                                      
        Job GetJobById(int jobId);                                     
        List<Job> GetJobsByCustomerId(int customerId);                 
        List<Job> GetJobRequestsByCustomerId(int customerId);         
        List<Job> GetAllJobs();                                        
        void UpdateJobDetails(Job updatedJob);                         
        void ChangeJobStatus(int jobId, JobStatus newStatus);         
    }
}
