using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// TODO: main index View have checkboxes for removing
// POST to /remove

// TODO: how about editing existing cheeses?

namespace CheeseMVC.Controllers {
    public class CheeseController : Controller {
        private readonly CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext) {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index() {
            List<Cheese> cheeses = context.Cheeses.Include(cheese => cheese.Category).ToList();
            return View(cheeses);
        }

        /**
         *
         * this is a shorthand equivalent to
         *
         * public IActionResult Add() = {
         *  AddCheeseViewModel addCheeseViewModel = new AddCheseViewModel(context.Categories.ToList());
         *  return View(addCheeseViewModel);
         * }
         * 
         */
        public IActionResult Add() => View(new AddCheeseViewModel(context.Categories.ToList()));

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel) {
            if (!ModelState.IsValid) return View(addCheeseViewModel);
           
            context.Cheeses.Add(new Cheese {
                Name = addCheeseViewModel.Name,
                Description = addCheeseViewModel.Description,
                CategoryID = addCheeseViewModel.CategoryID
            });
            context.SaveChanges();
            return Redirect("/Cheese");
        }

        public IActionResult Remove() {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = context.Cheeses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds) {
            foreach (int cheeseId in cheeseIds) {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }
            
            /*
             alternative approach
             RemoveRange takes an iterable (a List in this case) of Cheese objects to remove
             we use the Linq iterable Select() method to transform [mutate] the array into a new form
             see AddCheeseViewModel.cs for more notes on Select()
             
             context.Cheeses.RemoveRange(
                cheeseIds.Select(
                    id => context.Cheeses.Single(x => x.ID == id)
                ).ToList()        
             );
            */

            context.SaveChanges();
            return Redirect("/");
        }
    }
}
