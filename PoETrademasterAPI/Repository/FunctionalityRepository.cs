using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Database;

namespace PoETrademasterAPI.Repository
{
    public class FunctionalityRepository : RepositoryBase
    {
        public bool ResetAllTables()
        {
            var sqlParams = new List<SqlParameter>();
            string sql = GeneratedProcString("dbo.ResetAllTables", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql);
            return true;
        }
    }
}
