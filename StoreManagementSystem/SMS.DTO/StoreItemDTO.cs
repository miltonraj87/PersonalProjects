using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DTO
{
    public class StoreItemDTO
    {
        public StoreItemDTO()
        {
            IsDeleted = false;
        }

        public int StoreItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemCost { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
