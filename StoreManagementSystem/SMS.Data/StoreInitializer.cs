using SMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data
{
    public class StoreInitializer : DropCreateDatabaseIfModelChanges<StoreDbContext>
    {
        protected override void Seed(StoreDbContext context)
        {
            var storeItems = new List<StoreItem>
            {
                new StoreItem{ ItemName="Colgate paste", ItemCost= 45, IsDeleted = false, CreatedDate = DateTime.Now},
                new StoreItem{ ItemName="Colgate brush", ItemCost= 25, IsDeleted = false, CreatedDate = DateTime.Now},
                new StoreItem{ ItemName="Dove soap", ItemCost= 55, IsDeleted = false, CreatedDate = DateTime.Now},                  
                new StoreItem{ ItemName="Brill cream", ItemCost= 100, IsDeleted = false, CreatedDate = DateTime.Now},
                new StoreItem{ ItemName="Ponds powder", ItemCost= 120, IsDeleted = false, CreatedDate = DateTime.Now}
            };

            storeItems.ForEach(storeItem => context.StoreItems.Add(storeItem));
            context.SaveChanges();
        }
    }
}
