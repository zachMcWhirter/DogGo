using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        SqlConnection Connection { get; }

        List<Walk> GetAllWalks();

        List<Walk> GetWalksByWalkerId(int id);
    }
}