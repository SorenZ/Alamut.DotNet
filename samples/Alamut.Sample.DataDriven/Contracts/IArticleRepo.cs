using Alamut.Data.Repository;
using Alamut.Sample.DataDriven.Models;

namespace Alamut.Sample.DataDriven.Contracts
{
    public interface IArticleRepo : IRepository<Article>
    {
    }
}