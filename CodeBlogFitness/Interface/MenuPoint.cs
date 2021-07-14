using FitnessBL;
using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Interface
{
    public delegate void BaseDelegate(BaseController baseController);
    public class MenuPoint:BaseActionsController<BaseElement,BaseActions>
    {
        public string Name { get; set; }
        public BaseDelegate action;

        public MenuPoint(string name, BaseDelegate baseDelegate)
        {
            Name = name;
            action = baseDelegate;
        }
    }


}
