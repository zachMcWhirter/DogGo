using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Walk> GetAllWalks()
        {
            using (SqlConnection conn = Connection)

            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                        SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, d.OwnerId, d.Name as DogName, dw.Name as WalkerName, o.Name as OwnerName
                        FROM Walks w
                        JOIN Dog d ON w.DogId = d.Id
                        JOIN Walker dw ON w.WalkerId = dw.Id
                        JOIN Owner o ON d.OwnerId = o.Id
                        ";

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Walk> walks = new List<Walk>();
                    while (reader.Read())
                    {
                        Walk walk = new Walk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            DogId = reader.GetInt32(reader.GetOrdinal("Id")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            Owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                            }
                        };

                        walks.Add(walk);
                    }

                    reader.Close();

                    return walks;
                }
            }
        }

        public List<Walk> GetWalksByWalkerId(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT w.Id, w.Date, w.Duration, w.WalkerId, w.DogId, dw.[Name], dw.NeighborhoodId, dw.ImageUrl, o.Id AS OwnerId, 
                    o.[Name] AS OwnerName,dw.Name as WalkerName, n.[Name]
                    FROM Walks w 
                    JOIN Walker dw ON w.WalkerId = dw.Id
                    JOIN Dog d ON w.DogId = d.Id
                    JOIN Owner o ON d.OwnerId = o.Id
                    JOIN Neighborhood n ON dw.NeighborhoodId = n.Id
                    WHERE dw.Id = @walkerId
                    ";

                    cmd.Parameters.AddWithValue("@walkerId", walkerId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walk> walks = new List<Walk>();

                    while (reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                            Owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                            }


                        };
                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }
    }
}