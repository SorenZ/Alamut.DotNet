using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Alamut.Data.Structure
{
    public class IdValue 
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class IdValue<TValue>
    {
        public string Id { get; set; }
        public TValue Value { get; set; }
    }

    public class IdValues
    {
        public IdValues()
        {
            this.Values = new Collection<string>();
        }

        public string Id { get; set; }
        public ICollection<string> Values { get; set; }
    }
    public class IdValues<TValue>
    {
        public IdValues()
        {
            this.Values = new Collection<TValue>();
        }

        public string Id { get; set; }
        public ICollection<TValue> Values { get; set; }
    }
}