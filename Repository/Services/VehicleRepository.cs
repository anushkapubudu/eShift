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
    class VehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString = DbConst.ConnectionString;

        public void AddVehicle(Vehicle vehicle)
        {
            var query = @"
                INSERT INTO Vehicle (PlateNumber, VehicleType, CapacityKg)
                VALUES (@PlateNumber, @VehicleType, @CapacityKg)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
                cmd.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType);
                cmd.Parameters.AddWithValue("@CapacityKg", vehicle.CapacityKg);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            var query = "SELECT * FROM Vehicle WHERE VehicleId = @VehicleId AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapVehicle(reader);
                }
            }

            return null;
        }

        public List<Vehicle> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            var query = "SELECT * FROM Vehicle WHERE DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        vehicles.Add(MapVehicle(reader));
                }
            }

            return vehicles;
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            var query = @"
                UPDATE Vehicle SET 
                    PlateNumber = @PlateNumber,
                    VehicleType = @VehicleType,
                    CapacityKg = @CapacityKg
                WHERE VehicleId = @VehicleId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@PlateNumber", vehicle.PlateNumber);
                cmd.Parameters.AddWithValue("@VehicleType", vehicle.VehicleType);
                cmd.Parameters.AddWithValue("@CapacityKg", vehicle.CapacityKg);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void DeleteVehicle(int vehicleId)
        {
            var query = "UPDATE Vehicle SET DeletedAt = SYSDATETIME() WHERE VehicleId = @VehicleId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Vehicle MapVehicle(SqlDataReader reader)
        {
            return new Vehicle
            {
                VehicleId = Convert.ToInt32(reader["VehicleId"]),
                PlateNumber = reader["PlateNumber"].ToString(),
                VehicleType = reader["VehicleType"].ToString(),
                CapacityKg = Convert.ToDecimal(reader["CapacityKg"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                DeletedAt = reader["DeletedAt"] == DBNull.Value ? null : (DateTime?)reader["DeletedAt"]
            };

        }
    }
}
