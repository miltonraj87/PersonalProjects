using CrossCutting.Attributes;
using SMS.Services.Contract;
using SMS.Services.Concrete;
using StoreManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.DTO;

namespace StoreManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        [Autowire]
        private IStoreItemService storeItemService;

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetStoreItems(int? page, int? limit, string sortBy, string direction, string searchString = null)
        {
            int total = 0;
            List<StoreItemViewModel> records = new List<StoreItemViewModel>();
            var storeItems = storeItemService.GetStoreItems(page, limit, sortBy, direction, searchString, out total);
            if (storeItems.Any())
                records = storeItems.Select(e => new StoreItemViewModel { StoreItemID = e.StoreItemID, ItemName = e.ItemName, ItemCost = e.ItemCost, CreatedDate = e.CreatedDate.ToString("dd/MMM/yyyy hh:mm:ss:fff tt") }).ToList();
            return Json(new
            {
                records,
                total
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveStoreItem(StoreItemViewModel storeItem)
        {
            storeItemService.Save(new StoreItemDTO { ItemName = storeItem.ItemName, ItemCost = storeItem.ItemCost, StoreItemID = storeItem.StoreItemID });

            return Json(new { Status = true, Message = string.Empty });
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            storeItemService.Delete(new StoreItemDTO { StoreItemID = id });
            return Json(true);
        }
    }
}