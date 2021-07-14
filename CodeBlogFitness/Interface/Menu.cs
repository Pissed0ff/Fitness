using FitnessBL;
using FitnessBL.Controller;
using System;
using System.Collections.Generic;
using static Fitness.Program;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Interface
{
    public class Menu
    {
        public Dictionary<int, MenuPoint> actions;
        public BaseController Controller { get; set; }
        public Menu(EatingController ec)
        {
            Controller = ec;
            //Создание словаря возможных функций
            actions = new Dictionary<int, MenuPoint>();
            actions.Add(1, new MenuPoint("Добавить продукт", AddProductHandler));
            actions.Add(2, new MenuPoint("Добавить прием пищи", AddEatingHandler));
            actions.Add(3, new MenuPoint("Отобразить приемы пищи", ShowEatingsHandler));
        }
        public Menu(TrainingController tc)
        {
            Controller = tc;
            //Создание словаря возможных функций
            actions = new Dictionary<int, MenuPoint>();
            actions.Add(1, new MenuPoint("Добавить упражнение", AddExerciseHandler));
            actions.Add(2, new MenuPoint("Добавить тренировку", AddTrainingHandler));
            actions.Add(3, new MenuPoint("Отобразить тренировки", ShowTrainingHandler));
        }

        public bool Actions()
        {
            Console.WriteLine("Доступные функции:");
            Console.WriteLine("0 - Выход");
            foreach (var el in actions)
            {
                Console.WriteLine(el.Key + " - " + el.Value.Name);
            }
            int actionNomber = Program.GetInt("Выберите действие");
            if (actionNomber == 0)
                return false;
            else if (actionNomber > 0 && actionNomber <= actions.Count())
            {
                actions[actionNomber].action.Invoke(Controller);
            }
            else 
                Console.WriteLine("Невенрное значение");
            return true;
        }


        #region EatingMenuHandlers
        public static void AddProductHandler(BaseController controller)
        {
            while (true)
            {
                Console.WriteLine("Введите название продукта или \"Выход\" ");
                string product = Console.ReadLine();
                if (product != "Выход")
                {
                    if (((EatingController)controller).isFoodExist(product))
                        Console.WriteLine("Такой продукт уже существует");
                    else AddNewFood((EatingController)controller, product);
                }
                else
                    break;
            }
        }
        public static void AddEatingHandler(BaseController controller)
        {
            string message = "Введите продукт";
            bool isFirst = true;
            while (AddProductToEating((EatingController)controller, ref message, ref isFirst))
            { }
        }
        public static void ShowEatingsHandler(BaseController controller)
        {
            ShowEatings((EatingController)controller);
        }

        #endregion

        #region EatingMenuActions

        /// <summary>
        /// Добавляет новый продукт в прием пищи
        /// </summary>
        /// <param name="ec"></param>
        /// <param name="message"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        public static bool AddProductToEating(EatingController ec, ref string message, ref bool isFirst)
        {
            Console.WriteLine(message);
            string product = Console.ReadLine();
            if (product == "Выход" && (!isFirst))
            {
                ec.SaveEating();
                return false;
            }
            int quantity = Program.GetInt("Введите размер порции");
            if (!ec.AddEating(product, quantity))
            {
                AddNewFood(ec, product);
                ec.AddEating(product, quantity);
            }
            message = "Введите продукт или \"Выход\" ";
            isFirst = false;
            return true;
        }

        /// <summary>
        /// Отображает прием пищи пользователя
        /// </summary>
        /// <param name="ec"></param>
        public static void ShowEatings(EatingController ec)
        {
            var arr = ec.GetUserEating();
            Console.Clear();
            Console.WriteLine("Приемы пищи пользователя " + ec.GetUserName());
            foreach (var eatingEl in arr)
            {
                Console.WriteLine($"-------{eatingEl.Moment}-------");
                foreach (var foodEl in eatingEl.Foods)
                {
                    Console.WriteLine(foodEl.Key.Name + " : " + foodEl.Value);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Добавляет новый продукт
        /// </summary>
        /// <param name="ec"></param>
        /// <param name="product"></param>
        public static void AddNewFood(EatingController ec, string product)
        {
            int calls = GetInt("Введите каллорийность на 100г");
            int fats = GetInt("Введите кол-во жиров на 100г");
            int prots = GetInt("Введите кол-во белков на 100г");
            int carbs = GetInt("Введите кол-во углеводов на 100г");
            ec.AddFoodElem(product, calls, fats, prots, carbs);
        }

        #endregion

        #region TrainingMenuActions
        /// <summary>
        /// Добавляет новое упражнение
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="exercise"></param>
        public static void AddNewExercise(TrainingController tc, string exercise)
        {
            int calls = GetInt("Введите кол-во затраченных каллорийи за единицу");
            tc.AddExerciseElem(exercise, calls);
        }

        /// <summary>
        /// Добавляет новое упражнение в тренировку
        /// </summary>
        /// <param name="ec"></param>
        /// <param name="message"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        public static bool AddExerciseToTraining(TrainingController tc, ref string message, ref bool isFirst)
        {
            Console.WriteLine(message);
            string exercise = Console.ReadLine();
            if (exercise == "Выход" && (!isFirst))
            {
                tc.SaveTraining();
                return false;
            }
            int quantity = GetInt("Введите кол-во повторений");
            if (!tc.AddTraining(exercise, quantity))
            {
                AddNewExercise(tc, exercise);
                tc.AddTraining(exercise, quantity);
            }
            message = "Введите упражнение или \"Выход\" ";
            isFirst = false;
            return true;
        }

        /// <summary>
        /// Отображает тренировки пользователя
        /// </summary>
        /// <param name="ec"></param>
        public static void ShowTrainings(TrainingController tc)
        {
            var arr = tc.GetUserTrainings();
            Console.Clear();
            Console.WriteLine("Тренировки пользователя " + tc.GetUserName());
            foreach (var trainingEl in arr)
            {
                Console.WriteLine($"-------{trainingEl.Moment}-------");
                foreach (var exerciseEl in trainingEl.Exercises)
                {
                    Console.WriteLine(exerciseEl.Key.Name + " : " + exerciseEl.Value);
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region TrainingControllerHandlers
        public static void AddExerciseHandler(BaseController trainingController)
        {
            while (true)
            {
                Console.WriteLine("Введите название упражнения или \"Выход\" ");
                string exercise = Console.ReadLine();
                if (exercise != "Выход")
                {
                    if (((TrainingController)trainingController).isExerciseExist(exercise))
                        Console.WriteLine("Такое упражнение уже существует");
                    else AddNewExercise((TrainingController)trainingController, exercise);
                }
                else
                    break;
            }
        }
        public static void AddTrainingHandler(BaseController trainingController)
        {
            string message = "Введите упражнение";
            bool isFirst = true;
            while (AddExerciseToTraining((TrainingController)trainingController, ref message, ref isFirst))
            { }
        }
        public static void ShowTrainingHandler(BaseController trainingController)
        {
            ShowTrainings((TrainingController)trainingController);
        }
        #endregion
    }
}
