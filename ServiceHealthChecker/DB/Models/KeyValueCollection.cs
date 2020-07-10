using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ServiceHealthChecker.DB.Models
{
    abstract public class ComplexServiceColumn
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(Service))]
        public int ServiceId { get; set; }
        [ManyToOne]
        public Service Service { get; set; }
    }

    abstract public class KeyValueColumn: ComplexServiceColumn
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public abstract class StringColumn:  ComplexServiceColumn
    {
        public string Value { get; set; }
    }

    //although unintuitive, every column must be of different type, bc sqllite-extensions uses only types to distinguish them
    public class BodyMustContain : StringColumn
    {
    }

    public class BodyMustNotContain : StringColumn
    {
    }

    //unrelated todo inspect malformed header parameters
    public class Header : KeyValueColumn
    {
    }

    public class QueryParam : KeyValueColumn
    {
    }


}