using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace eShift.Repository.Services
{
    public class PaymentRepository : IPaymentRepository
    {
        void IPaymentRepository.AddPayment(Payment payment)
        {
            string query = @"INSERT INTO Payments 
                             (InvoiceId, PaymentDate, Amount, Method, ReferenceNo, CreatedAt)
                             VALUES 
                             (@InvoiceId, @PaymentDate, @Amount, @Method, @ReferenceNo, @CreatedAt)";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", payment.InvoiceId);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@Method", payment.Method.ToString());
                cmd.Parameters.AddWithValue("@ReferenceNo", payment.ReferenceNo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", payment.CreatedAt);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        bool IPaymentRepository.UpdatePayment(Payment payment)
        {
            string query = @"UPDATE Payments SET 
                             InvoiceId = @InvoiceId, 
                             PaymentDate = @PaymentDate, 
                             Amount = @Amount, 
                             Method = @Method, 
                             ReferenceNo = @ReferenceNo,
                             UpdatedAt = SYSDATETIME()
                             WHERE PaymentId = @PaymentId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", payment.InvoiceId);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@Method", payment.Method.ToString());
                cmd.Parameters.AddWithValue("@ReferenceNo", payment.ReferenceNo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        void IPaymentRepository.DeletePayment(int paymentId)
        {
            string query = "DELETE FROM Payments WHERE PaymentId = @PaymentId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        Payment IPaymentRepository.GetPaymentById(int paymentId)
        {
            string query = "SELECT * FROM Payments WHERE PaymentId = @PaymentId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Payment
                    {
                        PaymentId = Convert.ToInt32(reader["PaymentId"]),
                        InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        Method = Enum.TryParse(reader["Method"].ToString(), true, out PaymentMethod methodVal) ? methodVal : PaymentMethod.Other,
                        ReferenceNo = reader["ReferenceNo"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    };
                }

                return null;
            }
        }

        List<Payment> IPaymentRepository.GetPaymentsByInvoice(int invoiceId)
        {
            var result = new List<Payment>();
            string query = "SELECT * FROM Payments WHERE InvoiceId = @InvoiceId ORDER BY PaymentDate DESC";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Payment
                    {
                        PaymentId = Convert.ToInt32(reader["PaymentId"]),
                        InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        Method = Enum.TryParse(reader["Method"].ToString(), true, out PaymentMethod methodVal) ? methodVal : PaymentMethod.Other,
                        ReferenceNo = reader["ReferenceNo"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }

            return result;
        }

        List<Payment> IPaymentRepository.GetAllPayments()
        {
            var result = new List<Payment>();
            string query = "SELECT * FROM Payments ORDER BY PaymentDate DESC";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Payment
                    {
                        PaymentId = Convert.ToInt32(reader["PaymentId"]),
                        InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        Method = Enum.TryParse(reader["Method"].ToString(), true, out PaymentMethod methodVal) ? methodVal : PaymentMethod.Other,
                        ReferenceNo = reader["ReferenceNo"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                        UpdatedAt = reader["UpdatedAt"] != DBNull.Value ? Convert.ToDateTime(reader["UpdatedAt"]) : (DateTime?)null
                    });
                }
            }

            return result;
        }
    }
}
