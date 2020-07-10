using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceHealthChecker.DB.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace ServiceHealthChecker.DB
{
    public class ServicesDatabase
    {
        private readonly SQLiteAsyncConnection database;

        // I used callbacks to rerender parent components, but in this case I wanted to try doing this via event
        public event EventHandler ServiceDeletedEventHandler;

        public ServicesDatabase()
        {
            database = new SQLiteAsyncConnection(Constants.ServicesDatabasePath);
            database.CreateTableAsync<Service>().Wait();
            database.CreateTableAsync<Header>().Wait();
            database.CreateTableAsync<QueryParam>().Wait(); 
            database.CreateTableAsync<BodyMustContain>().Wait();
            database.CreateTableAsync<BodyMustNotContain>().Wait();
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
                ServiceDeletedEventHandler?.Invoke(null, EventArgs.Empty);
                return task.Result;
            });
        }
        
        public Task DeleteServicesAsync()
        {
            return database.DeleteAllAsync<Service>();
        }
    }
}