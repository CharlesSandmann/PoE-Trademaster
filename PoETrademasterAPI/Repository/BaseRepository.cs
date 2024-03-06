using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Database;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class BaseRepository : RepositoryBase
    {
        public bool AddBase(BaseModel baseGroup)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@BaseId", baseGroup.BaseId));
            sqlParams.Add(new SqlParameter("@BaseName", baseGroup.BaseName));
            sqlParams.Add(new SqlParameter("@BaseGroupId", baseGroup.BaseGroupId));
            sqlParams.Add(new SqlParameter("@ItemRequired", baseGroup.ItemRequired));
            sqlParams.Add(new SqlParameter("@ParentBaseId", System.Data.SqlDbType.Int)
            {
                Value = baseGroup.ParentBaseId as object ?? DBNull.Value
            });

            string sql = GeneratedProcString("dbo.AddBase", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }

        public List<BaseModel>? GetBases()
        {
            string sql = GeneratedProcString("dbo.GetBases", new List<SqlParameter>());
            var values = _dbContext.Bases.FromSqlRaw<Base>(sql).ToList();
            return values.Select(bg => new BaseModel
            {
                BaseId = bg.BaseId,
                BaseName = bg.BaseName,
                BaseGroupId = bg.BaseGroupId,
                ItemRequired = bg.ItemRequired,
                ParentBaseId = bg.ParentBaseId
            }).ToList();
        }
    }
}
