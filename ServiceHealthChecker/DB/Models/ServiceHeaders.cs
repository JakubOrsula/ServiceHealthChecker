using SQLite;

namespace ServiceHealthChecker.DB.Models
{
    public class ServiceHeaders: IMyDictionary
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}