using Alamut.Data.MongoDb.Repositories;
using Alamut.Sample.DataDriven.Contracts;
using Alamut.Sample.DataDriven.Models;
using MongoDB.Driver;

namespace Alamut.Sample.DataDriven.Repositories
{
    public class ArticleRepo : Repository<Article>,
        IArticleRepo
    {
        public ArticleRepo(IMongoDatabase database) : base(database)
        {
        }

        
    }
}