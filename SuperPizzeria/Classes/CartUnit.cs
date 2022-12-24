using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPizzeria.Classes
{
    public class CartUnit
    {
        public int Id { get; set; }
        public int Pizza { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public int Order { get; set; }

        public int GetPizzaCost
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                int pizzaCost = db.Pizzas.Find(Pizza).Cost;
                db.Dispose();
                return pizzaCost * Amount; 
            }
        }

        public string GetPizzaName
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                string pizzaName = db.Pizzas.Find(Pizza).Name;
                db.Dispose();
                return pizzaName;
            }
        }

    }
}
