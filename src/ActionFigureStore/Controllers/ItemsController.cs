using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActionFigureStore.Models;
using Microsoft.EntityFrameworkCore;



namespace ActionFigureStore.Controllers
{
    public class ItemsController : Controller
    {

        private IItemRepository itemRepo;

        public ItemsController(IItemRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.itemRepo = new EFItemRepository();
            }
            else
            {
                this.itemRepo = thisRepo;
            }
        }

        public IActionResult Index()
        {
            return View(itemRepo.Items.ToList());
        }

        public IActionResult ItemInfo(int ItemInfo)
        {
            Item item = itemRepo.Items.FirstOrDefault(x => x.ItemId == ItemInfo);
            return Json(item);
        }

        public IActionResult Details(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            itemRepo.Save(item);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            itemRepo.Edit(item);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            itemRepo.Remove(thisItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult NewActionFigure(string description, float price, int inventory)
        {
            Item newActionFigure = new Item(description, price, inventory);
            itemRepo.Save(newActionFigure);
            return Json(newActionFigure);
        }

        [HttpPost]
        public IActionResult BuyFigure(int Figure)
        { 
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == Figure);
            thisItem.Inventory -= 1;
            itemRepo.Edit(thisItem);
            return Json(thisItem);
        }
    }
}