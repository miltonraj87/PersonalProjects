using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace StoreManagementSystem.Models
{
    public class StoreItemViewModel
    {
        public StoreItemViewModel()
        {
            IsDeleted = false;
        }
                     
        public int StoreItemID { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Required]
        [Display(Name = "Item Cost")]
        [DataType(DataType.Currency)]
        public decimal ItemCost { get; set; }
        public string CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}