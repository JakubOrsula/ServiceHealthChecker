using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ServiceHealthChecker.DB.Models
{
    abstract public class KeyValueCollection
    {
        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        [ForeignKey(typeof(Service))]
        public int ServiceId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        [ManyToOne]
        public Service Service { get; set; }
    }
    //unrelated todo inspect malformed header parameters
    public class Header : KeyValueCollection
    {
    }

    public class QueryParam : KeyValueCollection
    {
    }
}