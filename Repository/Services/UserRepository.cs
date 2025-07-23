using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


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

        List<User> IUserRepository.GetAllUsers()
        {
            var users = new List<User>();
            var query = "SELECT UserId, FirstName, LastName, Role FROM Users";

            using (var conn = new SqlConnection(DbConst.ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Role = reader["Role"].ToString()
                        });
                    }
                }
            }

            return users;
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

        bool IUserRepository.IsEmailTakenByAnother(string email, int userId)
        {
            var query = @"SELECT COUNT(*) FROM Users 
                  WHERE Email = @Email AND UserId <> @UserId";

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        bool IUserRepository.UpdateUser(User user)
        {
            var query = new StringBuilder();
            query.Append("UPDATE Users SET ");
            query.Append("FirstName = @FirstName, ");
            query.Append("LastName = @LastName, ");
            query.Append("Email = @Email, ");
            query.Append("Telephone = @Telephone, ");
            query.Append("Address = @Address, ");

            if (user.PasswordHash != null)
                query.Append("PasswordHash = @PasswordHash, ");

            query.Append("UpdatedAt = SYSDATETIME() ");
            query.Append("WHERE UserId = @UserId");

            using (SqlConnection conn = new SqlConnection(DbConst.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query.ToString(), conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Telephone", user.Telephone);
                cmd.Parameters.AddWithValue("@Address", user.Address);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);

                if (user.PasswordHash != null)
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
