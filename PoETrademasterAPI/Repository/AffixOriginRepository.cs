using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Database;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class AffixOriginRepository : RepositoryBase
    {
        public bool AddAffixOrigin(AffixOriginModel affixOriginModel)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@AffixOriginId", affixOriginModel.AffixOriginId));
            sqlParams.Add(new SqlParameter("@AffixOriginName", affixOriginModel.AffixOriginName));
            sqlParams.Add(new SqlParameter("@AffixLimit", System.Data.SqlDbType.Int)
            {
                Value = affixOriginModel.AffixLimit as object ?? DBNull.Value
            });

            sqlParams.Add(new SqlParameter("@IsInfluence", affixOriginModel.IsInfluence));
            sqlParams.Add(new SqlParameter("@IsEldritch", affixOriginModel.IsEldritch));
            string sql = GeneratedProcString("dbo.AddAffixOrigin", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }

        public List<AffixOriginModel>? GetAffixOrigins()
        {
            string sql = GeneratedProcString("dbo.GetAffixOrigins", new List<SqlParameter>());
            var values = _dbContext.AffixOrigins.FromSqlRaw<AffixOrigin>(sql).ToList();
            return values.Select(bg => new AffixOriginModel
            {
                AffixOriginId = bg.AffixOriginId,
                AffixOriginName = bg.AffixOriginName,
                AffixLimit = bg.AffixLimit ?? 0,
                IsInfluence = bg.IsInfluence,
                IsEldritch = bg.IsEldritch,
            }).ToList();
        }
    }
}
