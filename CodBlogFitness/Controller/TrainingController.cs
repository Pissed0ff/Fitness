using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Controller
{
    

    public class TrainingController : BaseActionsController<Exercise,Training>
    {
        private const string File_Exercise_Name = "exercise.xml";
        private const string File_Training_Name = "training.dat";

        /// <summary>
        /// Создание контроллера с пользователем, список упражнений загружается из XML.
        /// </summary>
        /// <param name="user"></param>
        public TrainingController(User user) : base(user, File_Exercise_Name, File_Training_Name)
        { }

        /// <summary>
        /// Добавление упражнений в список с указанием затраченных КЛ.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="calls"></param>
        /// <param name="fats"></param>
        /// <param name="prots"></param>
        /// <param name="carb"></param>
        public void AddExerciseElem(string name, int calls)
        {
            ListElements.Add(new Exercise(name, calls));
            SaveListExerciseXml();
        }

        /// <summary>
        /// Добавление тренировки. Возвращает frue если упражнение есть в базе
        /// </summary>
        /// <param name="foodName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool AddTraining(string ExerciseName, int quantity)
        {
            var exercise = GetExerciseByName(ExerciseName);
            if (exercise != null)
            {
                if (CurrentAction == null)
                    CurrentAction = new Training(CurrentUser);
                CurrentAction.AddExercise(exercise, quantity);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Возвращает упражнение по запращиваемому имени
        /// </summary>
        /// <param name="foodName"></param>
        /// <returns></returns>
        public Exercise GetExerciseByName(string exerciseName)
        {
            return GetElemByName(exerciseName);
        }

        /// <summary>
        /// Проверяет существует ли продукт с данным именем в базе
        /// </summary>
        /// <param name="foodName"></param>
        /// <returns></returns>
        public bool isExerciseExist(string exerciseName)
        {
            if (GetExerciseByName(exerciseName) != null)
                return true;
            else return false;
        }

        /// <summary>
        /// Сохранение тренировки
        /// </summary>
        /// <param name="eating"></param>
        /// <returns></returns>
        public bool SaveTraining()
        {
            ListActions.Add(CurrentAction);
            CurrentAction = null;
            Save(File_Training_Name, ListActions);
            return true;
        }

        /// <summary>
        /// Загрузка тренировок из файла
        /// </summary>
        /// <returns></returns>
        public List<Training> LoadTraining()
        {
            return Load<Training>(File_Training_Name);
        }

        /// <summary>
        /// Сохранение списка упражнений
        /// </summary>
        public void SaveListExercise()
        {
            Save(File_Exercise_Name, ListElements);
        }
        /// <summary>
        /// Сохранение списка упражнений в формате xml
        /// </summary>
        public void SaveListExerciseXml()
        {
            SaveXml(File_Exercise_Name, ListElements);
        }
        /// <summary>
        /// Загрузка упражнений из файла формата xml
        /// </summary>
        /// <returns></returns>
        public List<Exercise> LoadXml()
        {
            return LoadXml<Exercise>(File_Exercise_Name);
        }

        /// <summary>
        /// Получение списка тренировок для текущего пользователя
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Training> GetUserTrainings()
        {
            return GetUserActions();
        }
    }
}
