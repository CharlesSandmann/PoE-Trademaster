using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class ItemRepository : RepositoryBase
    {
        public bool AddItem(ItemModel itemModel)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@ItemId", itemModel.ItemId));
            sqlParams.Add(new SqlParameter("@BaseId", itemModel.BaseId));
            sqlParams.Add(new SqlParameter("@ItemName", itemModel.ItemName));
            sqlParams.Add(new SqlParameter("@ImgLocation", itemModel.ImgLocation ?? ""));
            sqlParams.Add(new SqlParameter("@IsExperimentedBase", itemModel.IsExperimentedBase));
            string sql = GeneratedProcString("dbo.AddItem", sqlParams);
            int rowsAffected = _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }
    }
}
