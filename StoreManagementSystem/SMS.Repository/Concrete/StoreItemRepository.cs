using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Repository.Contract;
using SMS.Data.Entities;
using System.Linq.Expressions;
using System.Data.Entity;
using SMS.Data;

namespace SMS.Repository.Concrete
{
    public class StoreItemRepository : Repository, IStoreItemRepository
    {
        public StoreItemRepository(StoreDbContext context)
            : base(context)
        {
        }

        #region Public Methods
        public IQueryable<StoreItem> GetQueryable()
        {
            return storeDbContext.StoreItems.AsQueryable();
        }

        public IEnumerable<StoreItem> GetAll()
        {
            return storeDbContext.StoreItems.AsEnumerable();
        }

        public List<StoreItem> GetStoreItems(int? page, int? limit, string sortBy, string direction, string searchString, out int total)
        {
            var records = storeDbContext.StoreItems.Where(s => !s.IsDeleted).AsQueryable();
            if (records != null && records.Any())
            {
                total = records.Count();

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    records = records.Where(p => p.ItemName.Contains(searchString));
                }

                if (!string.IsNullOrEmpty(sortBy) && !string.IsNullOrEmpty(direction))
                {
                    if (direction.Trim().ToLower() == "asc")
                    {
                        records = SortHelper.OrderBy(records, sortBy);
                    }
                    else
                    {
                        records = SortHelper.OrderByDescending(records, sortBy);
                    }
                }
                else
                    records = records.OrderByDescending(or => or.CreatedDate);

                if (page.HasValue && limit.HasValue)
                {
                    int start = (page.Value - 1) * limit.Value;
                    records = records.Skip(start).Take(limit.Value);
                }
            }
            else
                total = 0;

            return records.ToList();
        }

        public StoreItem FindById(int id)
        {
            return storeDbContext.StoreItems.Where(i => i.StoreItemID == id).SingleOrDefault();
        }

        public IEnumerable<StoreItem> Find(Expression<Func<StoreItem, Boolean>> where)
        {
            return storeDbContext.StoreItems.Where(where).AsEnumerable();
        }

        public virtual StoreItem Add(StoreItem entity)
        {
            storeDbContext.StoreItems.Add(entity);
            storeDbContext.SaveChanges();
            return entity;
        }

        public virtual StoreItem Update(StoreItem entity)
        {
            storeDbContext.StoreItems.Attach(entity);
            storeDbContext.Entry<StoreItem>(entity).State = EntityState.Modified;
            storeDbContext.SaveChanges();
            return entity;
        }

        public virtual void Delete(StoreItem entity)
        {
            storeDbContext.StoreItems.Attach(entity);
            storeDbContext.Entry<StoreItem>(entity).State = EntityState.Modified;
            storeDbContext.SaveChanges();
        }
        #endregion Public Methods
    }
}
