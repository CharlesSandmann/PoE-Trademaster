using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class AffixRepository : RepositoryBase
    {
        public bool AddAffix(AffixModel affixModel)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@AffixId", affixModel.AffixId));
            sqlParams.Add(new SqlParameter("@Affix", affixModel.Affix));
            sqlParams.Add(new SqlParameter("@ElevatedAffix", System.Data.SqlDbType.VarChar)
            {
                Value = affixModel.ElevatedAffix as object ?? DBNull.Value
            });

            sqlParams.Add(new SqlParameter("@IsPrefix", affixModel.IsPrefix));
            sqlParams.Add(new SqlParameter("@IsImplicit", affixModel.IsImplicit));
            sqlParams.Add(new SqlParameter("@IsNotablePassive", affixModel.IsNotablePassive));
            sqlParams.Add(new SqlParameter("@HasResistance", affixModel.HasResistance));
            sqlParams.Add(new SqlParameter("@HasAttribute", affixModel.HasAttribute));
            sqlParams.Add(new SqlParameter("@AffixOriginId", affixModel.AffixOriginId));
            string sql = GeneratedProcString("dbo.AddAffix", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }

        public List<AffixModel>? GetAffixes()
        {
            string sql = GeneratedProcString("dbo.GetAffixes", new List<SqlParameter>());
            var values = _dbContext.Affixes.FromSqlRaw(sql).ToList();
            return values.Select(bg => new AffixModel
            {
                AffixId = bg.AffixId,
                Affix = bg.AffixName,
                ElevatedAffix = bg.ElevatedAffix,
                IsPrefix = bg.IsPrefix,
                IsImplicit = bg.IsImplicit,
                IsNotablePassive = bg.IsNotablePassive,
                HasAttribute = bg.HasAttribute,
                HasResistance = bg.HasResistance,
                Tags = bg.Tags,
                AffixGroups = bg.AffixGroups,
                AffixOriginId = bg.AffixOriginId,
            }).ToList();
        }
    }
}
