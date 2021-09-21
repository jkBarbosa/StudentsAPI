using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Core.DbConnection;

namespace EntityFramework.Factory
{
    public class EfConnectionFactory : IDbConnectionFactory
    {
        private readonly StudentsAppDbContext _dbContext;
        public EfConnectionFactory(StudentsAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Commit()
        {
            if (InTransaction) _dbContext.Database.CommitTransaction();
            InTransaction = false;
        }

        public void Rollback()
        {
            if (InTransaction) _dbContext.Database.RollbackTransaction();
            InTransaction = false;
        }

        public void Start()
        {
            _dbContext.Database.BeginTransaction();
            InTransaction = true;
        }

        public bool InTransaction { get; private set; }
    }
}
