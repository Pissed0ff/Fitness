using FitnessBL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness
{
    public delegate void ActionTrainingDelegate(TrainingController tc, UserController uc);
    public class ConsoleTrainingAction
    {
        public string Name { get; }
        public ActionTrainingDelegate Action;

        public ConsoleTrainingAction(string name, ActionTrainingDelegate deleg)
        {
            Name = name;
            Action += deleg;
        }
    }
}
