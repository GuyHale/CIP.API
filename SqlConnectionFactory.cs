using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace CIP.API
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string connectionString) => new SqlConnection(connectionString);
    }
}
