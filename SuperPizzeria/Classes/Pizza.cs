using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPizzeria.Classes
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IngridientsList { get; set; }
        public bool IsOnMenu { get; set; }


        public int Cost
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                int[] ingridientList = Functions.StringToIntArray(IngridientsList);
                var ingridients = db.Ingridients.Where(i => ingridientList.Contains(i.Id));
                int sum = 0;
                foreach (var ingridient in ingridients)
                {
                    sum += ingridient.Cost;
                }
                db.Dispose();
                return sum;
            }
        }

        public string GetIngridientsNames
        {
            get
            {
                ApplicationContext db = new ApplicationContext();
                int[] ingridientList = Functions.StringToIntArray(IngridientsList);
                var ingridients = db.Ingridients.Where(i => ingridientList.Contains(i.Id));
                string IngridientsNames = "";
                foreach (var ingridient in ingridients)
                {
                    IngridientsNames += ingridient.Name + " ";
                }
                db.Dispose();
                return IngridientsNames;

            }

        }
    }
}
