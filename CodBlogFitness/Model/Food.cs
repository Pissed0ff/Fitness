using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Food: BaseElement
    {
        // PHG - Per Hundred Gramms
        public double CalloriesPHG { get; set; }
        public double Callories { get; set; }
        public double FatsPHG { get; set; }
        public double ProteinsPHG { get; set; }
        public double CarbohdratesPHG { get; set; }

        //
        public Food() {}
        public Food(string name)
        {
            Name = name;
        }

        public Food (string name, double calls, double fats, double prots, double carb)
        {
            Name = name;
            CalloriesPHG = calls;
            ProteinsPHG = prots;
            FatsPHG = fats;
            CarbohdratesPHG = carb;
        }
    }
}
