using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Controller
{
    public class ExerciseController : BaseController
    {
        private User User { get; }
        public List<Exercise> ListExercises{get; private set ;}
        public List<Activity> ListActivities { get; private set; }



        public ExerciseController(User user)
        {
            User = User ?? throw new ArgumentNullException(nameof(user));

        }
    }
}
