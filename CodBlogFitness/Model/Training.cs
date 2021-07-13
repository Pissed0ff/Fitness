using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Training: BaseActions
    {
        public DateTime Moment { get; }
        public Dictionary<Exercise, int> Exercises { get; }

        public Training() 
        {
        }
        public Training(User user) : base(user)
        {
            Moment = DateTime.Now;
            Exercises = new Dictionary<Exercise, int>();
        }

        /// <summary>
        /// Добавление упражнения в тренировку
        /// </summary>
        /// <param name="exercise"></param>
        /// <param name="quantity"></param>
        public void AddExercise(Exercise exercise, int quantity)
        {
            var value = Exercises.Keys.FirstOrDefault<Exercise>(f => f.Name.Equals(exercise.Name));
            if (value == null)
                Exercises.Add(exercise, quantity);
            else Exercises[exercise] += quantity;
        }

    }
}
