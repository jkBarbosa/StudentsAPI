using System;
using Core.DbConnection;

namespace Core.CQRS
{
    /// <summary>
    /// Unit Of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
        IUnitOfWork Start();

    }


    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UnitOfWork(
            IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;

            // NOTE: Not disposing of the factory at this moment since we need it for query afterwards
            // _dbConnectionFactory?.Connections.ForEach(c =>
            // {
            //     c.Connection?.Close();
            //     c.Connection?.Dispose();
            // });
            //
            // _dbConnectionFactory?.Dispose();
        }


        #region Transaction Related

        public IUnitOfWork Start()
        {
            _dbConnectionFactory.Start();
            return this;
        }
        public void Commit()
        {
            _dbConnectionFactory.Commit();
        }
        public void Rollback()
        {
            _dbConnectionFactory.Rollback();
        }

        #endregion
    }
}
