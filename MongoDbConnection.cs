using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using BCrypt.Net;

namespace Human_Resources_Management_System
{
    public class MongoDbConnection
    {
        private readonly IMongoDatabase _database;
        public MongoDbConnection() {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("HumanResourcesManagementDb");

        }

        public IMongoCollection<UsersModel> GetUsersCollection()
        {
            return _database.GetCollection<UsersModel>("Users");
        }

        public IMongoCollection<PeoplesModel> GetPeoplesCollection()
        {
            return _database.GetCollection<PeoplesModel>("Peoples");
        }



    }
}
