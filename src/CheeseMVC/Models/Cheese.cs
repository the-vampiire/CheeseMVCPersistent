using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        
        // this is a FOREIGN KEY [FK]
            // the ID of the category is stored in rows of Cheese table because a Cheese HAS ONE Category 
            // meaning a Category HAS MANY Cheeses
                // a ONE to MANY relationship constrained by a foreign key
        public int CategoryID { get; set; }
        
        // the CheeseType enum is replaced by data from the Category table identified by the CategoryID FK
        public CheeseCategory Category { get; set; }
    }
}
