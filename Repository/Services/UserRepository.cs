using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Data.SqlClient;


namespace eShift.Repository.Services
{
    public class UserRepository : IUserRepository
    {
        void IUserRepository.CreateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            {
                string query = @"INSERT INTO Users 
                                 (FirstName, LastName, Address, Telephone, Email, PasswordHash, Role)
                                 VALUES 
                                 (@FirstName, @LastName, @Address, @Telephone, @Email, @PasswordHash, @Role)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@Telephone", user.Telephone);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        User IUserRepository.GetUserByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            {
                string query = "SELECT * FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new User
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Telephone = reader["Telephone"].ToString(),
                        Email = reader["Email"].ToString(),
                        PasswordHash = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }

                return null;
            }
        }

        bool IUserRepository.IsEmailTaken(string email)
        {
            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
