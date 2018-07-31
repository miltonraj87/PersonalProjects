using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Repository.Contract;
using SMS.Services.Contract;
using SMS.DTO;
using System.Linq.Expressions;
using SMS.Data.Entities;
using CrossCutting.Attributes;
using CrossCutting.Utilities;
using SMS.Repository.Concrete;

namespace SMS.Services.Concrete
{
    public class StoreItemService : IStoreItemService
    {
        [Autowire]
        private IStoreItemRepository storeItemRepository;

        public StoreItemService()
        {
        }

        #region Public Methods
        public List<StoreItemDTO> GetAll()
        {
            List<StoreItemDTO> storeItems = new List<StoreItemDTO>();
            var items = storeItemRepository.GetAll();
            if (items.Any())
                storeItems = items.Where(s => !s.IsDeleted).Select(entity => new StoreItemDTO
                {
                    StoreItemID = entity.StoreItemID,
                    ItemName = entity.ItemName,
                    ItemCost = entity.ItemCost,
                    IsDeleted = entity.IsDeleted,
                    CreatedDate = entity.CreatedDate
                }).ToList();
            return storeItems;
        }

        public List<StoreItemDTO> GetStoreItems(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            List<StoreItem> records = storeItemRepository.GetStoreItems(page, limit, sortBy, direction, searchString, out total);
            return records.Select(entity => new StoreItemDTO
            {
                StoreItemID = entity.StoreItemID,
                ItemName = entity.ItemName,
                ItemCost = entity.ItemCost,
                IsDeleted = entity.IsDeleted,
                CreatedDate = entity.CreatedDate
            }).ToList();
        }

        public StoreItemDTO FindById(int id)
        {
            var storeItem = storeItemRepository.FindById(id);
            StoreItemDTO storeItemDTO = new StoreItemDTO();

            return storeItemDTO;
        }

        public StoreItemDTO Save(StoreItemDTO entity)
        {
            if (entity != null)
            {
                if (entity.StoreItemID <= 0)
                {
                    storeItemRepository.Add(new StoreItem
                    {
                        StoreItemID = entity.StoreItemID,
                        ItemName = entity.ItemName,
                        ItemCost = entity.ItemCost,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now
                    });
                }
                else
                {
                    var storeItem = storeItemRepository.FindById(entity.StoreItemID);
                    storeItem.ItemName = entity.ItemName;
                    storeItem.ItemCost = entity.ItemCost;
                    storeItem.IsDeleted = entity.IsDeleted;

                    storeItemRepository.Update(storeItem);
                }
            }
            return entity;
        }

        public void Delete(StoreItemDTO entity)
        {
            var storeItem = storeItemRepository.FindById(entity.StoreItemID);
            storeItem.IsDeleted = true;

            storeItemRepository.Delete(storeItem);
        }
        #endregion Public Methods
    }
}
