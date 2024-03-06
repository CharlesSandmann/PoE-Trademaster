using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PoETrademasterAPI.Models;

namespace PoETrademasterAPI.Repository
{
    public class ItemPropertyRepository : RepositoryBase
    {
        public bool AddItemProperty(ItemPropertyModel itemPropertyModel)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@Property", itemPropertyModel.Property));
            sqlParams.Add(new SqlParameter("@ItemId", itemPropertyModel.ItemId));
            sqlParams.Add(new SqlParameter("@ItemPropertyTypeId", itemPropertyModel.ItemPropertyTypeId));
            string sql = GeneratedProcString("dbo.AddItemProperty", sqlParams);
            int rowsAffected = _dbContext.Database.ExecuteSqlRaw(sql, sqlParams.ToArray());
            return true;
        }
    }
}
