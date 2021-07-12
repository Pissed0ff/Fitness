using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Exercise
    {
        DateTime Start { get;  }
        DateTime End { get;  }
        User User { get;  }
        Activity Activity { get; }

        public Exercise(DateTime start, DateTime end, User user, Activity activity)
        {
            Start = start;
            End = end;
            User = user;
            Activity = activity;
        }

    }
}
