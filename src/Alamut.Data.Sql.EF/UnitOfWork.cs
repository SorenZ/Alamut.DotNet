using System;
using Alamut.Data.Entity;
using Alamut.Data.Repository;
using Alamut.Data.Structure;
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

        public ServiceResult Commit()
        {
            var updateCount = _context.SaveChanges();

            return ServiceResult.Okay($"{updateCount} item(s) have been updated.");
        }

        public void RollBack() => _context.Database.CurrentTransaction.Rollback();
        

        public void Dispose() => _context.Dispose();
    }
}
