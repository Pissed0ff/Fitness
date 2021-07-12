using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    abstract public class BaseElement
    {
        public string Name { get; set; }

        public BaseElement(string name)
        {
            Name = name;
        }
        public BaseElement()
        { }
    }
}
