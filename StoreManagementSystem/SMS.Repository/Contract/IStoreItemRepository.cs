using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Data.Entities;

namespace SMS.Repository.Contract
{
    public interface IStoreItemRepository : IRepository<StoreItem>
    {
        List<StoreItem> GetStoreItems(int? page, int? limit, string sortBy, string direction, string searchString, out int total);
    }
}
