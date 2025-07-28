using eShift.Model;
using System;
using System.Collections.Generic;

namespace eShift.Business.Interface
{
    public interface IReportService
    {
        List<CustomerReport> GetCustomerReport(DateTime from, DateTime to);
        List<RevenueReport> GetRevenueReport(DateTime from, DateTime to);
        List<JobReport> GetJobReport(DateTime from, DateTime to);
    }
}
