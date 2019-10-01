using System;
using Alamut.Abstractions.Structure;

namespace Alamut.Data.Sql
{
    /// <summary>
    /// Represents a unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        Result Commit();
        void RollBack();
    }
}
