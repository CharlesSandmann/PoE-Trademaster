using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class BaseAffixTierRepository : RepositoryBase
    {
        public bool AddBaseAffixTier(BaseAffixTierModel baseAffixTierModel)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@BaseAffixId", baseAffixTierModel.BaseAffixId));
            sqlParams.Add(new SqlParameter("@Stat1StartValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat1StartValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@Stat1EndValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat1EndValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@Stat2StartValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat2StartValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@Stat2EndValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat2EndValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@Stat3StartValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat3StartValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@Stat3EndValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat3EndValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@Stat4StartValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat4StartValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@Stat4EndValue", System.Data.SqlDbType.Decimal)
            {
                Value = baseAffixTierModel.Stat4EndValue as object ?? DBNull.Value,
            });

            sqlParams.Add(new SqlParameter("@IsElevated", baseAffixTierModel.IsElevated));
            sqlParams.Add(new SqlParameter("@ILvlRequirement", baseAffixTierModel.ILvlRequirement));
            sqlParams.Add(new SqlParameter("@Weight", baseAffixTierModel.Weight));
            string sql = GeneratedProcString("dbo.AddBaseAffixTier", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }
    }
}
