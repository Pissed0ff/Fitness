using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Exercise : BaseActions
    {
        DateTime Start { get;  }
        DateTime End { get;  }
        Activity Activity { get; }

        public Exercise(DateTime start, DateTime end, User user, Activity activity):base(user)
        {
            Start = start;
            End = end;
            Activity = activity;
        }

    }
}
