using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        void AddDog(Dog newDog);
        Dog GetDogById(int id);
        List<Dog> GetDogsByOwnerId(int ownerId);
        void UpdateDog(Dog dog);
        void DeleteDog(int dogid);

    }
}