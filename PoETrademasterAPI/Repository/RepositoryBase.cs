using Microsoft.Data.SqlClient;
using PoETrademasterAPI.Database;

namespace PoETrademasterAPI.Repository
{
    public class RepositoryBase : IDisposable
    {
        protected PoEtrademasterContext _dbContext { get; set; }

        public RepositoryBase()
        {
            _dbContext = new PoEtrademasterContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        protected string GeneratedProcString(string procName, IEnumerable<SqlParameter> sqlParams)
        {
            string sql = "EXEC " + procName;
            foreach (var sqlParam in sqlParams)
            {
                sql += $" {sqlParam.ParameterName},";
            }

            return sql.TrimEnd(',');
        }
    }
}
