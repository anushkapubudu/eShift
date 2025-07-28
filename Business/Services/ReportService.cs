using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository;
using eShift.Repository.Interface;
using eShift.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eShift.Business.Service
{
    public class ReportService : IReportService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJobRepository _jobRepo;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IPaymentRepository _paymentRepo;

        
        public ReportService()
        {
            _userRepo = new UserRepository();
            _jobRepo = new JobRepository();
            _invoiceRepo = new InvoiceRepository();
            _paymentRepo = new PaymentRepository();
        }

        public List<CustomerReport> GetCustomerReport(DateTime from, DateTime to)
        {
            var users = _userRepo.GetAllUsers().Where(u => u.Role == "Customer").ToList();
            var jobs = _jobRepo.GetAllJobs().Where(j => j.CreatedAt >= from && j.CreatedAt <= to).ToList();
            var invoices = _invoiceRepo.GetAllInvoices().Where(i => i.IssueDate >= from && i.IssueDate <= to).ToList();

            return users.Select(u => new CustomerReport
            {
                FullName = $"{u.FirstName} {u.LastName}",
                Email = u.Email,
                Telephone = u.Telephone,
                JobCount = jobs.Count(j => j.CustomerId == u.UserId),
                InvoiceCount = invoices.Count(i => jobs.Any(j => j.JobId == i.JobId && j.CustomerId == u.UserId)),
                TotalPaid = invoices.Where(i => jobs.Any(j => j.JobId == i.JobId && j.CustomerId == u.UserId)).Sum(i => i.PaidAmount)
            }).ToList();
        }

        public List<RevenueReport> GetRevenueReport(DateTime from, DateTime to)
        {
            var invoices = _invoiceRepo.GetAllInvoices().Where(i => i.IssueDate >= from && i.IssueDate <= to).ToList();

            return invoices.Select(i => new RevenueReport
            {
                InvoiceNumber = i.InvoiceNumber,
                IssueDate = i.IssueDate,
                TotalAmount = i.TotalAmount,
                PaidAmount = i.PaidAmount,
                Status = i.Status.ToString(),
                TaxAmount = i.SubTotal * i.TaxRate / 100,
                NetRevenue = i.PaidAmount
            }).ToList();
        }

        public List<JobReport> GetJobReport(DateTime from, DateTime to)
        {
            var jobs = _jobRepo.GetAllJobs().Where(j => j.StartDate >= from && j.StartDate <= to).ToList();

            return jobs.Select(j => new JobReport
            {
                JobNumber = j.JobNumber,
                CustomerId = j.CustomerId,
                Pickup = j.PickupLocation,
                Dropoff = j.DropoffLocation,
                Status = j.JobStatus.ToString(),
                StartDate = j.StartDate,
                EndDate = j.EndDate,
            }).ToList();
        }
    }
}
