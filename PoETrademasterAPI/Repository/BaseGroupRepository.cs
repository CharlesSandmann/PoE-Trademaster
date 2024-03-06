using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Database;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class BaseGroupRepository : RepositoryBase
    {
        public bool AddBaseGroup(BaseGroupModel baseGroup)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@BaseGroupId", baseGroup.BaseGroupId));
            sqlParams.Add(new SqlParameter("@BaseGroupName", baseGroup.BaseGroupName));
            sqlParams.Add(new SqlParameter("@MaxAffixes", baseGroup.MaxAffixes));
            sqlParams.Add(new SqlParameter("@CanBeRare", baseGroup.CanBeRare));
            sqlParams.Add(new SqlParameter("@CanBeInfluenced", baseGroup.CanBeInfluenced));
            sqlParams.Add(new SqlParameter("@CanUseFossil", baseGroup.CanUseFossil));
            sqlParams.Add(new SqlParameter("@CanUseEssence", baseGroup.CanUseEssence));
            sqlParams.Add(new SqlParameter("@AllowsCraftedAffix", baseGroup.AllowsCraftedAffix));
            sqlParams.Add(new SqlParameter("@IsNotable", baseGroup.IsNotable)); // currently unused
            sqlParams.Add(new SqlParameter("@IsCatalyst", baseGroup.CanUseCatalyst));
            string sql = GeneratedProcString("dbo.AddBaseGroup", sqlParams);
            int rowsAffected = _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }

        public List<BaseGroupModel>? GetBaseGroups()
        {
            string sql = GeneratedProcString("dbo.GetBaseGroups", new List<SqlParameter>());
            var values = _dbContext.BaseGroups.FromSqlRaw<BaseGroup>(sql).ToList();
            return values.Select(bg => new BaseGroupModel
            {
                BaseGroupId = bg.BaseGroupId,
                BaseGroupName = bg.BaseGroupName,
                MaxAffixes = bg.MaxAffixes,
                CanBeRare = bg.CanBeRare,
                CanBeInfluenced = bg.CanBeInfluenced,
                CanUseFossil = bg.CanUseFossil,
                CanUseEssence = bg.CanUseEssence,
                AllowsCraftedAffix = bg.AllowsCraftedAffix,
                CanUseCatalyst = bg.CanUseCatalyst,
            }).ToList();
        }
    }
}
