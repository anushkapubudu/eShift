using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace eShift.Repository.Services
{
    public class InvoiceRepository : IInvoiceRepository
    {
        void IInvoiceRepository.AddInvoice(Invoice invoice)
        {
            string query = @"INSERT INTO Invoice 
                             (InvoiceNumber, JobId, IssueDate, DueDate, SubTotal, TaxRate, TotalAmount, PaidAmount, Status, CreatedAt)
                             VALUES 
                             (@InvoiceNumber, @JobId, @IssueDate, @DueDate, @SubTotal, @TaxRate, @TotalAmount, @PaidAmount, @Status, @CreatedAt)";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                cmd.Parameters.AddWithValue("@JobId", invoice.JobId);
                cmd.Parameters.AddWithValue("@IssueDate", invoice.IssueDate);
                cmd.Parameters.AddWithValue("@DueDate", invoice.DueDate);
                cmd.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
                cmd.Parameters.AddWithValue("@TaxRate", invoice.TaxRate);
                cmd.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                cmd.Parameters.AddWithValue("@PaidAmount", invoice.PaidAmount);
                cmd.Parameters.AddWithValue("@Status", invoice.Status.ToString());
                cmd.Parameters.AddWithValue("@CreatedAt", invoice.CreatedAt);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        bool IInvoiceRepository.UpdateInvoice(Invoice invoice)
        {
            var query = new StringBuilder();
            query.Append("UPDATE Invoice SET ");
            query.Append("InvoiceNumber = @InvoiceNumber, ");
            query.Append("JobId = @JobId, ");
            query.Append("IssueDate = @IssueDate, ");
            query.Append("DueDate = @DueDate, ");
            query.Append("SubTotal = @SubTotal, ");
            query.Append("TaxRate = @TaxRate, ");
            query.Append("TotalAmount = @TotalAmount, ");
            query.Append("Status = @Status, ");

            query.Append("UpdatedAt = SYSDATETIME() ");
            query.Append("WHERE InvoiceId = @InvoiceId");

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                cmd.Parameters.AddWithValue("@JobId", invoice.JobId);
                cmd.Parameters.AddWithValue("@IssueDate", invoice.IssueDate);
                cmd.Parameters.AddWithValue("@DueDate", invoice.DueDate);
                cmd.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
                cmd.Parameters.AddWithValue("@TaxRate", invoice.TaxRate);
                cmd.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                cmd.Parameters.AddWithValue("@Status", invoice.Status.ToString());
                cmd.Parameters.AddWithValue("@InvoiceId", invoice.InvoiceId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        void IInvoiceRepository.DeleteInvoice(int invoiceId)
        {
            string query = "DELETE FROM Invoice WHERE InvoiceId = @InvoiceId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        Invoice IInvoiceRepository.GetInvoiceById(int invoiceId)
        {
            string query = "SELECT * FROM Invoice WHERE InvoiceId = @InvoiceId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Invoice
                    {
                        InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                        InvoiceNumber = reader["InvoiceNumber"].ToString(),
                        JobId = Convert.ToInt32(reader["JobId"]),
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                        DueDate = Convert.ToDateTime(reader["DueDate"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                        TaxRate = Convert.ToDecimal(reader["TaxRate"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        PaidAmount = Convert.ToDecimal(reader["PaidAmount"]),
                        Status = Enum.TryParse(reader["Status"].ToString(), true, out InvoiceStatus statusVal) ? statusVal : InvoiceStatus.Draft,
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }

                return null;
            }
        }

        List<Invoice> IInvoiceRepository.GetAllInvoices()
        {
            var invoices = new List<Invoice>();
            string query = "SELECT * FROM Invoice ORDER BY IssueDate DESC";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    invoices.Add(new Invoice
                    {
                        InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                        InvoiceNumber = reader["InvoiceNumber"].ToString(),
                        JobId = Convert.ToInt32(reader["JobId"]),
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                        DueDate = Convert.ToDateTime(reader["DueDate"]),
                        SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                        TaxRate = Convert.ToDecimal(reader["TaxRate"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        PaidAmount = Convert.ToDecimal(reader["PaidAmount"]),
                        Status = Enum.TryParse(reader["Status"].ToString(), true, out InvoiceStatus statusVal) ? statusVal : InvoiceStatus.Draft,
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }

            return invoices;
        }

        void IInvoiceRepository.UpdateInvoiceStatus(int invoiceId, InvoiceStatus status)
        {
            string query = "UPDATE Invoice SET Status = @Status, UpdatedAt = SYSDATETIME() WHERE InvoiceId = @InvoiceId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Status", status.ToString());
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        void IInvoiceRepository.UpdatePaidAmount(int invoiceId, decimal paidAmount)
        {
            string query = "UPDATE Invoice SET PaidAmount = @PaidAmount, UpdatedAt = SYSDATETIME() WHERE InvoiceId = @InvoiceId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PaidAmount", paidAmount);
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
