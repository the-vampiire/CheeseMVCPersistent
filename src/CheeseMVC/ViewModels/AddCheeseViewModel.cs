using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public List<SelectListItem> Categories { get; set; }

        // constructor that accepts a categories argument from the DbContext in the controller
        public AddCheeseViewModel(IEnumerable<CheeseCategory> categories) {
            
            /**
             * for those familiar with the Array.map() method in javascript look here:
             * https://stackoverflow.com/questions/32959468/example-of-array-map-in-c
             *
             * this is known as a Select method in C# (from the Linq List methods) 
             * what the Select method does is loop over the contents of a List (or other iterable, also referre)
             * and produce a new List.
             * each element in the original list has some function or mutation applied to it
             * the new list that is produced now contains elements that have been "mapped" or mutated
             *
             * this is a convenient way of applying some transformation of data on all elements in an iterable
             */
            Categories = categories.Select(category =>
                new SelectListItem {
                    Value = category.ID.ToString(),
                    Text = category.Name
                }
            ).ToList(); 

        }
        // default constructor for model binding
        // we pass in an emtpy "shell" AddCheeseViewModel to store the form contents
        // then we use this shell (temporary storage) to set the properties of a new model
        // in the POST request handler for that route
        public AddCheeseViewModel() {}
    }
}
