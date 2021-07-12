using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Controller
{
    

    public class ExerciseController : BaseActionsController<Activity,Exercise>
    {
        public const string File_Activity_Name = "activity.xml";
        public const string File_Exercise_Name = "exercise.dat";
        private User User { get; }
        public List<Exercise> ListExercises{get; private set ;}
        public List<Activity> ListActivities { get; private set; }
        public ExerciseController(User user) : base(user, File_Activity_Name, File_Exercise_Name)
        {}
    }
}
