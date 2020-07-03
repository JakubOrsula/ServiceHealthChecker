using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceHealthChecker.DB.Models;
using SQLite;

namespace ServiceHealthChecker.DB
{
    public class ServiceHeadersDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ServiceHeadersDatabase()
        {
            database = new SQLiteAsyncConnection(Constants.ServiceHeadersDatabasePath);
            database.CreateTableAsync<ServiceHeaders>().Wait();
        }

        public Task<int> DeleteHeader(ServiceHeaders header)
        {
            return database.DeleteAsync(header);
        }

        public async Task UpdateHeaders(int serviceID, List<ServiceHeaders> headers)
        {
            List<Task> tasks = new List<Task>();
            foreach (var header in headers)
            {
                tasks.Add(database.InsertAsync(header));
            }
            await Task.WhenAll(tasks);
        }
    }
}