using System.Collections.Generic;
using Alamut.Data.Entity;

namespace Alamut.Data.Sql.EF.Test.Database
{
    public class Blog : IEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }
}