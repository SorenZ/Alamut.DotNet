using System;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Abstractions.Structure;
using Microsoft.EntityFrameworkCore;

namespace Alamut.Data.Sql.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EfUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public Result Commit()
        {
            var updateCount = _context.SaveChanges();

            return Result.Okay($"{updateCount} item(s) have been updated.");
        }

        public void RollBack() => _context.Database.CurrentTransaction.Rollback();
        

        public void Dispose() => _context.Dispose();
    }
}
