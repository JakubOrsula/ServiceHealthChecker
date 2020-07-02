using System;
using System.IO;
using XF = Xamarin.Forms;

namespace ServiceHealthChecker
{
    //bootstrap colors
    public static class Palette
    {
        public static XF.Color Primary = XF.Color.FromHex("#007bff");
        public static XF.Color Secondary = XF.Color.FromHex("#6c757d");
        public static XF.Color Success = XF.Color.FromHex("#28a745");
        public static XF.Color Danger = XF.Color.FromHex("#dc3545");
        public static XF.Color Warning = XF.Color.FromHex("#ffc107");
        public static XF.Color Info = XF.Color.FromHex("#17a2b8");
        public static XF.Color Light = XF.Color.FromHex("#f8f9fa");
        public static XF.Color Dark = XF.Color.FromHex("#343a40");
    }
    public static class Constants
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