using SMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services.Contract
{
    public interface IStoreItemService
    {
        List<StoreItemDTO> GetAll();
        List<StoreItemDTO> GetStoreItems(int? page, int? limit, string sortBy, string direction, string searchString, out int total);
        StoreItemDTO FindById(int id);
        StoreItemDTO Save(StoreItemDTO entity);
        void Delete(StoreItemDTO entity);
    }
}
