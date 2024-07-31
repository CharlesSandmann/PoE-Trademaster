using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class AffixGroupRepository : RepositoryBase
    {
        public bool AddAffixGroup(AffixGroupModel affixGroupModel)
        {
            try
            {
                var sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@AffixId", affixGroupModel.AffixId));
                sqlParams.Add(new SqlParameter("@AffixGroupName", affixGroupModel.AffixGroupName));
                string sql = GeneratedProcString("dbo.AddAffixGroup", sqlParams);
                int rowsAffected = _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
