using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Repository.Services
{
    class StaffRepository : IStaffRepository
    {
        private readonly string _connectionString = DbConst.ConnectionString;

        public void CreateStaff(Staff staff)
        {
            var query = @"
                INSERT INTO Staff (StaffNumber, FirstName, LastName, ContactNumber, Email, Designation, LicenceNumber)
                VALUES (@StaffNumber, @FirstName, @LastName, @ContactNumber, @Email, @Designation, @LicenceNumber)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StaffNumber", staff.StaffNumber);
                cmd.Parameters.AddWithValue("@FirstName", staff.FirstName);
                cmd.Parameters.AddWithValue("@LastName", staff.LastName);
                cmd.Parameters.AddWithValue("@ContactNumber", (object)staff.ContactNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)staff.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Designation", staff.Designation);
                cmd.Parameters.AddWithValue("@LicenceNumber", (object)staff.LicenceNumber ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Staff GetStaffById(int staffId)
        {
            var query = "SELECT * FROM Staff WHERE StaffId = @StaffId AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapStaff(reader);
                }
            }

            return null;
        }

        public List<Staff> GetAllStaff()
        {
            var staffList = new List<Staff>();
            var query = "SELECT * FROM Staff WHERE DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        staffList.Add(MapStaff(reader));
                }
            }

            return staffList;
        }

        public List<Staff> GetStaffByDesignation(string designation)
        {
            var staffList = new List<Staff>();
            var query = "SELECT * FROM Staff WHERE Designation = @Designation AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Designation", designation);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        staffList.Add(MapStaff(reader));
                }
            }

            return staffList;
        }

        public bool UpdateStaff(Staff staff)
        {
            var query = @"
                UPDATE Staff SET 
                    FirstName = @FirstName,
                    LastName = @LastName,
                    ContactNumber = @ContactNumber,
                    Email = @Email,
                    LicenceNumber = @LicenceNumber,
                    Designation = @Designation,
                    UpdatedAt = SYSDATETIME()
                WHERE StaffId = @StaffId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StaffId", staff.StaffId);
                cmd.Parameters.AddWithValue("@FirstName", staff.FirstName);
                cmd.Parameters.AddWithValue("@LastName", staff.LastName);
                cmd.Parameters.AddWithValue("@Designation", staff.Designation);
                cmd.Parameters.AddWithValue("@ContactNumber", (object)staff.ContactNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)staff.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LicenceNumber", (object)staff.LicenceNumber ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void DeleteStaff(int staffId)
        {
            var query = "UPDATE Staff SET DeletedAt = SYSDATETIME() WHERE StaffId = @StaffId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetLastStaffId()
        {
            var query = "SELECT ISNULL(MAX(StaffId), 0) FROM Staff";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }


        private Staff MapStaff(SqlDataReader reader)
        {
            return new Staff
            {
                StaffId = Convert.ToInt32(reader["StaffId"]),
                StaffNumber = reader["StaffNumber"].ToString(),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                ContactNumber = reader["ContactNumber"] == DBNull.Value ? null : reader["ContactNumber"].ToString(),
                Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                Designation = reader["Designation"].ToString(),
                LicenceNumber = reader["LicenceNumber"] == DBNull.Value ? null : reader["LicenceNumber"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"],
                DeletedAt = reader["DeletedAt"] == DBNull.Value ? null : (DateTime?)reader["DeletedAt"]
            };
        }

    }
}
