using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPizzeria.Classes
{
    public class Order
    {
        public Order()
        {


        }

        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int Sale { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }


        public string GetPizzasNames
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                ApplicationContext db1 = new ApplicationContext();
                string pizzasNames = "";
                string pizzaName;
                var cartUnits = db.CartUnits.Where(i => i.Order == Id);
                foreach (CartUnit i in cartUnits)
                {
                    pizzaName = db1.Pizzas.Find(i.Pizza).Name;
                    pizzasNames += pizzaName + $"({i.Amount}) ";
                }
                db.Dispose();
                db1.Dispose();
                return pizzasNames;
            }
      
        }

        public double GetOrderCost
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                ApplicationContext db1 = new ApplicationContext();
                
                double orderCost = 0;
                int pizzaCost;
                Pizza pizza;
                var cartUnits = db.CartUnits.Where(i => i.Order == Id);
                foreach (CartUnit i in cartUnits)
                {
                    pizza = db1.Pizzas.Find(i.Pizza);
                    pizzaCost = pizza.Cost;
                    orderCost += pizzaCost;
                }
                if(Status == 2 && Sale != 0)
                {
;                    orderCost*=db.Sales.Find(Sale).Multiplier;
                }
                db.Dispose();
                db1.Dispose();
                
                return orderCost;

            }
        }

        public string GetUserName
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                return db.Users.Find(UserId).Login;
            }
        }

        public string GetOrderDate
        {
            get
            {
                return Convert.ToString(DateOfOrder);
            }
        }

        public string GetOrderStatus
        {
            get
            {
                string status = "";
                switch (Status)
                {
                    case 0:
                        {
                            status = "Ожидание обработки оператором";
                            break;
                        }
                    case 1:
                        {
                            ApplicationContext db = new ApplicationContext();
                            Sale sale = db.Sales.Find(Sale);
                            status = "Ожидание подтверждения пользователем скидки: "+sale.Summary+$"({100-sale.Multiplier*100}%)";
                            db.Dispose();
                            break;
                        }
                    case 2:
                        {
                            status = "Заказ принят";
                            break;
                        }
                }
                return status;
            }
        }

        public string GetSaleName
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                string saleName;
                if (Sale == 0)
                {
                    saleName = "Без скидки";
                }
                else
                {
                    saleName = db.Sales.Find(Sale).Summary;
                }
                return saleName;
            }
        }
    }
}
