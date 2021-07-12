using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Activity
    {
        public string Name { get; set; }
        public int CalliriesPerMinut { get; set; }
         public Activity(string name, int calls)
        {
            Name = name;
            CalliriesPerMinut = calls;
        }
    }
}
