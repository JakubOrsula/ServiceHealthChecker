using System;
using System.IO;

namespace ServiceHealthChecker
{
    public class Constants
    {
        public const int DefaultTimeout = 15;
        
        public const string ServicesDatabaseFilename = "ServicesDB.db3";
        public const string LogsDatabaseFilename = "LogsDB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string ServicesDatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, ServicesDatabaseFilename);
            }
        }
        
        public static string LogsDatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, LogsDatabaseFilename);
            }
        }
    }
}