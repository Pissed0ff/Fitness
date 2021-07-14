using Fitness.Interface;
using FitnessBL;
using FitnessBL.Controller;
using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace Fitness
{
    class Program
    {
        static void Main(string[] args)
        {
            var Culture = new CultureInfo("en-us");
            var ResourceManager = new ResourceManager("Fitness.Languages.Messages", typeof(Program).Assembly);
            Console.WriteLine(ResourceManager.GetString("AppHello", Culture));

            //Создали пользователя
            var userController = UserSelect();

            //Выбрали модуль
            while (true)
            {
                var mod = GetInt("Выберите модуль: 1 - Треннировка 2 - Прием пищи 3 - Сменить пользователя");
                switch (mod)
                {
                    case 1:
                        {
                            //Создали контроллер тренировки
                            var trainingController = new TrainingController(userController.CurrentUser);
                            var menu = new Menu(trainingController);
                            while (menu.Actions())
                            { }
                            break;
                        }

                    case 2:
                        {
                            //Создали контроллер приемов пищи
                            var eatingController = new EatingController(userController.CurrentUser);
                            var menu = new Menu(eatingController);
                            while (menu.Actions())
                            { }
                            break;
                        }
                    case 3:
                        {
                            userController = UserSelect();
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Интерфейс выбора пользователя
        /// </summary>
        /// <returns></returns>
        public static UserController UserSelect()
        {
            //ввод имени
            Console.WriteLine("Введите имя пользователя");
            string name = Console.ReadLine();

            UserController controller = new UserController(name);

            if (controller.IsNewUser)
            {
                //ввод гендера
                bool condition = true;
                int intGender = default;
                while (condition)
                {
                    Console.WriteLine("Выберети пол:");
                    string[] genderArr = controller.GetGenders();
                    for (int i = 0; i < genderArr.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}: {genderArr[i]}");
                    }
                    string strGender = Console.ReadLine();
                    if (int.TryParse(strGender, out intGender) && intGender <= genderArr.Length && intGender > 0)
                        condition = false;
                    else Console.WriteLine("неверный формат номера");
                }
                Gender.value genderVal = (Gender.value)Enum.GetValues(typeof(Gender.value)).GetValue(intGender - 1);
                Gender gender = new Gender(genderVal);

                //ввод даты рождения
                condition = true;
                string strBirthDate;
                DateTime birthDate = DateTime.MinValue;
                while (condition)
                {
                    Console.WriteLine("Введите дату рождения");
                    strBirthDate = Console.ReadLine();
                    if (DateTime.TryParse(strBirthDate, out birthDate))
                        condition = false;
                    else
                        Console.WriteLine("Неверный формат даты");
                }


                //ввод роста
                int height = GetInt("Введите рост");
                //ввод веса
                int weight = GetInt("Введите вес");

                controller.AddUserInformation(gender, birthDate, weight, height);
            }
            Console.Clear();
            Console.WriteLine($"Приветствую, {controller.CurrentUser.ToString()}");
            return controller;
        }

        /// <summary>
        /// Перевод из буквенного значения в числовой
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static int GetInt(string mes)
        {
            int x;
            string str;
            while (true)
            {
                Console.Write(mes + ": ");
                str = Console.ReadLine();
                if (int.TryParse(str, out x))
                {
                    return x;
                }
                else Console.WriteLine("Неверный формат");
            }



        }

        ///// <summary>
        ///// Отображение меню питания
        ///// </summary>
        ///// <param name="eatingController"></param>
        ///// <param name="userController"></param>
        //public static void EatingMenu(EatingController eatingController,
        //                            UserController userController)
        //{
        //    //Создание словаря возможных функций
        //    var actions = new Dictionary<int, ConsoleEatingAction>();
        //    actions.Add(1, new ConsoleEatingAction("Добавить продукт", AddProductHandler));
        //    actions.Add(2, new ConsoleEatingAction("Добавить прием пищи", AddEatingHandler));
        //    actions.Add(3, new ConsoleEatingAction("Отобразить приемы пищи", ShowEatingsHandler));
        //    actions.Add(4, new ConsoleEatingAction("Сменить пользователя", ChangeUserHandler));

        //    Console.WriteLine("Доступные функции:");
        //    foreach (var el in actions)
        //    {
        //        Console.WriteLine(el.Key + " - " + el.Value.Name);
        //    }
        //    int action = GetInt("Выберите действие");
        //    actions[action].Action.Invoke(eatingController, userController);
        //}

        //#region EatingMenuActions

        ///// <summary>
        ///// Добавляет новый продукт в прием пищи
        ///// </summary>
        ///// <param name="ec"></param>
        ///// <param name="message"></param>
        ///// <param name="isFirst"></param>
        ///// <returns></returns>
        //public static bool AddProductToEating(EatingController ec, ref string message, ref bool isFirst)
        //{
        //    Console.WriteLine(message);
        //    string product = Console.ReadLine();
        //    if (product == "Выход" && (!isFirst))
        //    {
        //        ec.SaveEating();
        //        return false;
        //    }
        //    int quantity = GetInt("Введите размер порции");
        //    if (!ec.AddEating(product, quantity))
        //    {
        //        AddNewFood(ec, product);
        //        ec.AddEating(product, quantity);
        //    }
        //    message = "Введите продукт или \"Выход\" ";
        //    isFirst = false;
        //    return true;
        //}

        ///// <summary>
        ///// Отображает прием пищи пользователя
        ///// </summary>
        ///// <param name="ec"></param>
        //public static void ShowEatings(EatingController ec)
        //{
        //    var arr = ec.GetUserEating();
        //    Console.Clear();
        //    Console.WriteLine("Приемы пищи пользователя " + ec.GetUserName());
        //    foreach (var eatingEl in arr)
        //    {
        //        Console.WriteLine($"-------{eatingEl.Moment}-------");
        //        foreach (var foodEl in eatingEl.Foods)
        //        {
        //            Console.WriteLine(foodEl.Key.Name + " : " + foodEl.Value);
        //        }
        //        Console.WriteLine();
        //    }
        //}

        ///// <summary>
        ///// Добавляет новый продукт
        ///// </summary>
        ///// <param name="ec"></param>
        ///// <param name="product"></param>
        //public static void AddNewFood(EatingController ec, string product)
        //{
        //    int calls = GetInt("Введите каллорийность на 100г");
        //    int fats = GetInt("Введите кол-во жиров на 100г");
        //    int prots = GetInt("Введите кол-во белков на 100г");
        //    int carbs = GetInt("Введите кол-во углеводов на 100г");
        //    ec.AddFoodElem(product, calls, fats, prots, carbs);
        //}

        //#endregion

        //#region EatingMenuHandlers
        //public static void AddProductHandler(BaseController eatingController)
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("Введите название продукта или \"Выход\" ");
        //        string product = Console.ReadLine();
        //        if (product != "Выход")
        //        {
        //            if (((EatingController)eatingController).isFoodExist(product))
        //                Console.WriteLine("Такой продукт уже существует");
        //            else AddNewFood((EatingController)eatingController, product);
        //        }
        //        else
        //            break;
        //    }
        //}
        //public static void AddEatingHandler(EatingController eatingController, UserController userController)
        //{
        //    string message = "Введите продукт";
        //    bool isFirst = true;
        //    while (AddProductToEating(eatingController, ref message, ref isFirst))
        //    { }
        //}
        //public static void ShowEatingsHandler(EatingController eatingController, UserController userController)
        //{
        //    ShowEatings(eatingController);
        //}
        //public static void ChangeUserHandler(EatingController eatingController, UserController userController)
        //{
        //    userController = UserSelect();
        //    eatingController.ChangeUser(userController.CurrentUser);
        //}

        //#endregion


        //public static void TrainingMenu(TrainingController trainingController,
        //                            UserController userController)
        //{
        //    //Создание словаря возможных функций
        //    var actions = new Dictionary<int, ConsoleTrainingAction>();
        //    actions.Add(1, new ConsoleTrainingAction("Добавить упражнение", AddExerciseHandler));
        //    actions.Add(2, new ConsoleTrainingAction("Добавить тренировку", AddTrainingHandler));
        //    actions.Add(3, new ConsoleTrainingAction("Отобразить тренировки", ShowTrainingHandler));
        //    //actions.Add(4, new ConsoleTrainingAction("Сменить пользователя", ChangeUserHandler));
           

        //    Console.WriteLine("Доступные функции:");
        //    foreach (var el in actions)
        //    {
        //        Console.WriteLine(el.Key + " - " + el.Value.Name);
        //    }
        //    int action = GetInt("Выберите действие");
        //    actions[action].Action.Invoke(trainingController, userController);
        //}

        //#region TrainingMenuActions
        ///// <summary>
        ///// Добавляет новое упражнение
        ///// </summary>
        ///// <param name="tc"></param>
        ///// <param name="exercise"></param>
        //public static void AddNewExercise(TrainingController tc, string exercise)
        //{
        //    int calls = GetInt("Введите кол-во затраченных каллорийи за единицу");
        //    tc.AddExerciseElem(exercise, calls);
        //}
       
        ///// <summary>
        ///// Добавляет новое упражнение в тренировку
        ///// </summary>
        ///// <param name="ec"></param>
        ///// <param name="message"></param>
        ///// <param name="isFirst"></param>
        ///// <returns></returns>
        //public static bool AddExerciseToTraining(TrainingController tc, ref string message, ref bool isFirst)
        //{
        //    Console.WriteLine(message);
        //    string exercise = Console.ReadLine();
        //    if (exercise == "Выход" && (!isFirst))
        //    {
        //        tc.SaveTraining();
        //        return false;
        //    }
        //    int quantity = GetInt("Введите кол-во повторений");
        //    if (!tc.AddTraining(exercise, quantity))
        //    {
        //        AddNewExercise(tc, exercise);
        //        tc.AddTraining(exercise, quantity);
        //    }
        //    message = "Введите упражнение или \"Выход\" ";
        //    isFirst = false;
        //    return true;
        //}

        ///// <summary>
        ///// Отображает тренировки пользователя
        ///// </summary>
        ///// <param name="ec"></param>
        //public static void ShowTrainings(TrainingController tc)
        //{
        //    var arr = tc.GetUserTrainings();
        //    Console.Clear();
        //    Console.WriteLine("Тренировки пользователя " + tc.GetUserName());
        //    foreach (var trainingEl in arr)
        //    {
        //        Console.WriteLine($"-------{trainingEl.Moment}-------");
        //        foreach (var exerciseEl in trainingEl.Exercises)
        //        {
        //            Console.WriteLine(exerciseEl.Key.Name + " : " + exerciseEl.Value);
        //        }
        //        Console.WriteLine();
        //    }
        //}
        //#endregion

        //#region TrainingControllerHandlers
        //public static void AddExerciseHandler(TrainingController trainingController, UserController userController)
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("Введите название упражнения или \"Выход\" ");
        //        string exercise = Console.ReadLine();
        //        if (exercise != "Выход")
        //        {
        //            if (trainingController.isExerciseExist(exercise))
        //                Console.WriteLine("Такое упражнение уже существует");
        //            else AddNewExercise(trainingController, exercise);
        //        }
        //        else
        //            break;
        //    }
        //}
        //public static void AddTrainingHandler(TrainingController trainingController, UserController userController)
        //{
        //    string message = "Введите упражнение";
        //    bool isFirst = true;
        //    while (AddExerciseToTraining(trainingController, ref message, ref isFirst))
        //    { }
        //}
        //public static void ShowTrainingHandler(TrainingController trainingController, UserController userController)
        //{
        //    ShowTrainings(trainingController);
        //}
        //#endregion
    }
}
