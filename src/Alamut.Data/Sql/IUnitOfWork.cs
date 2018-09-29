using System;
using Alamut.Data.Structure;

namespace Alamut.Data.Sql
{
    /// <summary>
    /// Represents a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ServiceResult Commit();
        void RollBack();
    }
}
