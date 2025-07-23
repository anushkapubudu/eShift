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
    class ContainerRepository : IContainerRepository
    {
        private readonly string _connectionString = DbConst.ConnectionString;

        public void AddContainer(Container container)
        {
            var query = @"
                INSERT INTO Container (ContainerNo, ContainerType, CapacityKg)
                VALUES (@ContainerNo, @ContainerType, @CapacityKg)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ContainerNo", container.ContainerNo);
                cmd.Parameters.AddWithValue("@ContainerType", (object)container.ContainerType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CapacityKg", container.CapacityKg);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Container GetContainerById(int containerId)
        {
            var query = "SELECT * FROM Container WHERE ContainerId = @ContainerId AND DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ContainerId", containerId);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapContainer(reader);
                }
            }

            return null;
        }

        public List<Container> GetAllContainers()
        {
            var containers = new List<Container>();
            var query = "SELECT * FROM Container WHERE DeletedAt IS NULL";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        containers.Add(MapContainer(reader));
                }
            }

            return containers;
        }

        public bool UpdateContainer(Container container)
        {
            var query = @"
                UPDATE Container SET 
                    ContainerNo = @ContainerNo,
                    ContainerType = @ContainerType,
                    CapacityKg = @CapacityKg
                WHERE ContainerId = @ContainerId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ContainerId", container.ContainerId);
                cmd.Parameters.AddWithValue("@ContainerNo", container.ContainerNo);
                cmd.Parameters.AddWithValue("@ContainerType", (object)container.ContainerType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CapacityKg", container.CapacityKg);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void DeleteContainer(int containerId)
        {
            var query = "UPDATE Container SET DeletedAt = SYSDATETIME() WHERE ContainerId = @ContainerId";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ContainerId", containerId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Container MapContainer(SqlDataReader reader)
        {
            return new Container
            {
                ContainerId = Convert.ToInt32(reader["ContainerId"]),
                ContainerNo = reader["ContainerNo"].ToString(),
                ContainerType = reader["ContainerType"] == DBNull.Value ? null : reader["ContainerType"].ToString(),
                CapacityKg = Convert.ToDecimal(reader["CapacityKg"]),
                DeletedAt = reader["DeletedAt"] == DBNull.Value ? null : (DateTime?)reader["DeletedAt"]
            };
        }
    }
}
