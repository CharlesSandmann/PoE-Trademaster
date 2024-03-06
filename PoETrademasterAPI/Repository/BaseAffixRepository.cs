using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Database;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class BaseAffixRepository : RepositoryBase
    {
        public bool AddBaseAffix(BaseAffixModel baseAffixModel)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@AffixId", baseAffixModel.AffixId));
            sqlParams.Add(new SqlParameter("@BaseId", baseAffixModel.BaseId));
            string sql = GeneratedProcString("dbo.AddBaseAffix", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }

        public List<BaseAffixModel>? GetBaseAffixes()
        {
            string sql = GeneratedProcString("dbo.GetBaseAffixes", new List<SqlParameter>());
            var values = _dbContext.BaseAffixes.FromSqlRaw(sql).ToList();
            return values.Select(ba => new BaseAffixModel
            {
                BaseAffixId = ba.BaseAffixId,
                AffixId = ba.AffixId,
                BaseId = ba.BaseId,
            }).ToList();
        }
    }
}
