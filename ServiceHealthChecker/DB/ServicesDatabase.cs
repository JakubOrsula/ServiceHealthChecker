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
            database = new SQLiteAsyncConnection(Constants.DatabasePath);
            database.CreateTableAsync<Service>().Wait();
        }

        public Task<List<Service>> GetServicesAsync()
        {
            return database.Table<Service>().ToListAsync();
        }

        public Task<Service> GetServiceAsync(int id)
        {
            return database.Table<Service>()
                .Where(target => target.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveServiceAsync(Service target)
        {
            if (target.ID != 0)
            {
                return database.UpdateAsync(target);
            }
            return database.InsertAsync(target);
        }
        
        public Task<int> DeleteServiceAsync(Service target)
        {
            return database.DeleteAsync(target);
        }
        
        public Task DeleteServicesAsync()
        {
            return database.DeleteAllAsync<Service>();
        }
    }
}