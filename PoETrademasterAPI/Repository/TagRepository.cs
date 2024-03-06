using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Database;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class TagRepository : RepositoryBase
    {
        public bool AddAffixTag(int affixId, int tagId)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@AffixId", affixId));
            sqlParams.Add(new SqlParameter("@TagId", tagId));
            string sql = GeneratedProcString("dbo.AddAffixTag", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }

        public bool AddTag(TagModel tagModel)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@TagId", tagModel.TagId));
            sqlParams.Add(new SqlParameter("@TagName", tagModel.TagName));
            string sql = GeneratedProcString("dbo.AddTag", sqlParams);
            _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }

        public List<TagModel>? GetTags()
        {
            string sql = GeneratedProcString("dbo.GetTags", new List<SqlParameter>());
            var values = _dbContext.Tags.FromSqlRaw<Tag>(sql).ToList();
            return values.Select(bg => new TagModel
            {
                TagId = bg.TagId,
                TagName = bg.TagName,
            }).ToList();
        }
    }
}
