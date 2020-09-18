﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkerRepository
    {
        List<Walker> GetWalkersInNeighborhood(int neighborhoodId);
        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);
    }
}
