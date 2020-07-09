using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceHealthChecker.DB.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace ServiceHealthChecker.DB
{
    //todo rename to represent everything
    public class ServicesDatabase
    {
        readonly SQLiteAsyncConnection database;

        public event EventHandler ServiceDeleted; //todo add handler suffix and consider not using it

        public ServicesDatabase()
        {
            database = new SQLiteAsyncConnection(Constants.ServicesDatabasePath);
            database.CreateTableAsync<Service>().Wait();
            database.CreateTableAsync<ServiceHeaders>().Wait();
        }

        public Task<List<Service>> GetServicesAsync()
        {
            return database.GetAllWithChildrenAsync<Service>();
        }

        public Task<Service> GetServiceAsync(int id)
        {
            return database.GetWithChildrenAsync<Service>(id);
        }

        public Task SaveServiceAsync(Service service)
        {
            return database.InsertOrReplaceWithChildrenAsync(service);
        }
        
        public Task<int> DeleteServiceAsync(Service service)
        {
            return database.DeleteAsync(service).ContinueWith(task => {
                ServiceDeleted?.Invoke(null, EventArgs.Empty);
                return task.Result;
            });
        }
        
        public Task DeleteServicesAsync()
        {
            return database.DeleteAllAsync<Service>();
        }
    }
}