using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceHealthChecker.DB.Models;
using SQLite;

namespace ServiceHealthChecker.DB
{
    //todo rename to represent everything
    public class ServicesDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ServicesDatabase()
        {
            database = new SQLiteAsyncConnection(Constants.ServicesDatabasePath);
            database.CreateTableAsync<Service>().Wait();
        }

        public Task<List<Service>> GetServicesAsync()
        {
            return database.Table<Service>().ToListAsync();
        }

        public Task<Service> GetServiceAsync(int id)
        {
            return database.Table<Service>()
                .Where(service => service.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveServiceAsync(Service service)
        {
            if (service.ID != 0)
            {
                return database.UpdateAsync(service);
            }
            return database.InsertAsync(service);
        }
        
        public Task<int> DeleteServiceAsync(Service service)
        {
            return database.DeleteAsync(service);
        }
        
        public Task DeleteServicesAsync()
        {
            return database.DeleteAllAsync<Service>();
        }
    }
}