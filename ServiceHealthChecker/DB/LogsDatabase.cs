using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceHealthChecker.DB.Models;
using SQLite;

namespace ServiceHealthChecker.DB
{
    public class LogsDatabase
    {
        readonly SQLiteAsyncConnection database;

        public LogsDatabase()
        {
            database = new SQLiteAsyncConnection(Constants.LogsDatabasePath);
            database.CreateTableAsync<ProbingLog>().Wait();
        }

        public Task<List<ProbingLog>> GetProbingLogsAsync(int ServiceId = 0)
        {
            if (ServiceId == 0)
            {
                return database.Table<ProbingLog>().OrderByDescending(log => log.ID).ToListAsync();
            }
            return database.Table<ProbingLog>()
                .Where(log => log.ServiceID == ServiceId)
                .OrderByDescending(log => log.ID)
                .ToListAsync();
        }

        public Task<ProbingLog> GetProbingLogAsync(int id)
        {
            return database.Table<ProbingLog>()
                .Where(log => log.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveProbingLogAsync(ProbingLog log)
        {
            if (log.ID != 0)
            {
                return database.UpdateAsync(log);
            }
            return database.InsertAsync(log);
        }
        
        public Task<int> DeleteProbingLogAsync(ProbingLog log)
        {
            return database.DeleteAsync(log);
        }
        
        public Task DeleteProbingLogsAsync()
        {
            return database.DeleteAllAsync<ProbingLog>();
        }
    }
}