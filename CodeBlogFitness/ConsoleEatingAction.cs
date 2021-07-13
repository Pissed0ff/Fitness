using FitnessBL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness
{
    public delegate void ActionEatingDelegate(EatingController ec, UserController uc);
    public class ConsoleEatingAction
    {
        public string Name { get; }
        public ActionEatingDelegate Action;

        public ConsoleEatingAction(string name, ActionEatingDelegate deleg)
        {
            Name = name;
            Action += deleg;
        }
    }
}
