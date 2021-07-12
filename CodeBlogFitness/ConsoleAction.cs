using FitnessBL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness
{
    public delegate void ActionDelegate(EatingController ec, UserController uc);
    public class ConsoleAction
    {
        public string Name { get; }
        public ActionDelegate Action;

        public ConsoleAction(string name, ActionDelegate deleg)
        {
            Name = name;
            Action += deleg;
        }
    }
}
