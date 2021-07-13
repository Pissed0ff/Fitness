using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Eating : BaseActions
    {
        public DateTime Moment { get; }
        public Dictionary<Food, int> Foods { get; }

        public Eating(User user) : base(user)
        {
            Moment = DateTime.Now;
            Foods = new Dictionary<Food, int>();
        }
        public void AddFood(Food food, int quantity)
        {
            var value = Foods.Keys.FirstOrDefault<Food>(f => f.Name.Equals(food.Name));
            if (value == null)
                Foods.Add(food, quantity);
            else Foods[food] += quantity;
        }

    }
}