using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Exercise:BaseElement
    {
         public int CalliriesPerMinut { get; set; }
         public Exercise(string name, int calls):base(name)
            {
                CalliriesPerMinut = calls;
            }
        public Exercise() 
        {
        }
    }
}
