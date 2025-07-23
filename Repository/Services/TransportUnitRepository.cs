using eShift.Model;
using eShift.Repository.Interface;
using eShift.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace eShift.Repository
{
    class TransportUnitRepository : ITransportUnitRepository
    {
        private readonly string _connectionString = DbConst.ConnectionString;

        public void CreateTransportUnit(TransportUnit unit)
        {
            var query = @"
                INSERT INTO TransportUnit 
                (JobId, VehicleId, ContainerId, DriverId, AssistantId, AssignedStart, AssignedEnd, IsFragile, ItemDescription, ItemCount, Weight)
                VALUES 
                (@JobId, @VehicleId, @ContainerId, @DriverId, @AssistantId, @AssignedStart, @AssignedEnd, @IsFragile, @ItemDescription, @ItemCount, @Weight)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobId", unit.JobId);
                cmd.Parameters.AddWithValue("@VehicleId", unit.VehicleId);
                cmd.Parameters.AddWithValue("@ContainerId", unit.ContainerId);
                cmd.Parameters.AddWithValue("@DriverId", unit.DriverId);
                cmd.Parameters.AddWithValue("@AssistantId", (object)unit.AssistantId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AssignedStart", unit.AssignedStart);
                cmd.Parameters.AddWithValue("@AssignedEnd", (object)unit.AssignedEnd ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsFragile", unit.IsFragile);
                cmd.Parameters.AddWithValue("@ItemDescription", (object)unit.ItemDescription ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ItemCount", unit.ItemCount);
                cmd.Parameters.AddWithValue("@Weight", (object)unit.Weight ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public TransportUnit GetTransportUnitById(int unitId)
        {
            var query = "SELECT * FROM TransportUnit WHERE TransportUnitId = @UnitId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UnitId", unitId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapTransportUnit(reader);
                }
            }

            return null;
        }

        public List<TransportUnit> GetAllTransportUnits()
        {
            var units = new List<TransportUnit>();
            var query = "SELECT * FROM TransportUnit";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        units.Add(MapTransportUnit(reader));
                }
            }

            return units;
        }

        public List<TransportUnit> GetTransportUnitsByJobId(int jobId)
        {
            var units = new List<TransportUnit>();
            var query = "SELECT * FROM TransportUnit WHERE JobId = @JobId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@JobId", jobId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        units.Add(MapTransportUnit(reader));
                }
            }

            return units;
        }

        public bool UpdateTransportUnit(TransportUnit unit)
        {
            var query = @"
                UPDATE TransportUnit SET
                    JobId = @JobId,
                    VehicleId = @VehicleId,
                    ContainerId = @ContainerId,
                    DriverId = @DriverId,
                    AssistantId = @AssistantId,
                    AssignedStart = @AssignedStart,
                    AssignedEnd = @AssignedEnd,
                    IsFragile = @IsFragile,
                    ItemDescription = @ItemDescription,
                    ItemCount = @ItemCount,
                    Weight = @Weight
                WHERE TransportUnitId = @UnitId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UnitId", unit.TransportUnitId);
                cmd.Parameters.AddWithValue("@JobId", unit.JobId);
                cmd.Parameters.AddWithValue("@VehicleId", unit.VehicleId);
                cmd.Parameters.AddWithValue("@ContainerId", unit.ContainerId);
                cmd.Parameters.AddWithValue("@DriverId", unit.DriverId);
                cmd.Parameters.AddWithValue("@AssistantId", (object)unit.AssistantId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AssignedStart", unit.AssignedStart);
                cmd.Parameters.AddWithValue("@AssignedEnd", (object)unit.AssignedEnd ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@IsFragile", unit.IsFragile);
                cmd.Parameters.AddWithValue("@ItemDescription", (object)unit.ItemDescription ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ItemCount", unit.ItemCount);
                cmd.Parameters.AddWithValue("@Weight", (object)unit.Weight ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void DeleteTransportUnit(int unitId)
        {
            var query = "DELETE FROM TransportUnit WHERE TransportUnitId = @UnitId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UnitId", unitId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private TransportUnit MapTransportUnit(SqlDataReader reader)
        {
            return new TransportUnit
            {
                TransportUnitId = Convert.ToInt32(reader["TransportUnitId"]),
                JobId = Convert.ToInt32(reader["JobId"]),
                VehicleId = Convert.ToInt32(reader["VehicleId"]),
                ContainerId = Convert.ToInt32(reader["ContainerId"]),
                DriverId = Convert.ToInt32(reader["DriverId"]),
                AssistantId = reader["AssistantId"] == DBNull.Value ? null : (int?)reader["AssistantId"],
                AssignedStart = Convert.ToDateTime(reader["AssignedStart"]),
                AssignedEnd = reader["AssignedEnd"] == DBNull.Value ? null : (DateTime?)reader["AssignedEnd"],
                IsFragile = Convert.ToBoolean(reader["IsFragile"]),
                ItemDescription = reader["ItemDescription"] == DBNull.Value ? null : reader["ItemDescription"].ToString(),
                ItemCount = Convert.ToInt32(reader["ItemCount"]),
                Weight = reader["Weight"] == DBNull.Value ? null : (int?)reader["Weight"]
            };
        }
    }
}
