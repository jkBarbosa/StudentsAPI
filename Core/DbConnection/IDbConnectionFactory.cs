using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Core.DbConnection
{
    public interface IDbConnectionFactory : IDisposable
    {

        void Commit();

        void Rollback();

        void Start();

    }
}
