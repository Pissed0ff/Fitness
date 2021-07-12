using System;
using FitnessBL.Model;
using FitnessBL.Controller;
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
            var ResourceManager = new ResourceManager("Fitness.Languages.Messages",typeof(Program).Assembly);
            Console.WriteLine(ResourceManager.GetString("AppHello",Culture));
              //создали пользователя
            var userController = UserSelect();

            EatingController eatingController = new EatingController(userController.CurrentUser);
            while (true)
            {
                MainMenu(eatingController, userController);
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

        public static void MainMenu(EatingController eatingController, UserController userController)
        {
            //Создание словаря возможных функций
            var actions = new Dictionary<int, ConsoleAction>();
            actions.Add(1, new ConsoleAction("Добавить продукт", AddProductHandler));
            actions.Add(2, new ConsoleAction("Добавить прием пищи", AddEatingHandler));
            actions.Add(3, new ConsoleAction("Отобразить приемы пищи", ShowEatingsHandler));
            actions.Add(4, new ConsoleAction("Сменить пользователя", ChangeUserHandler));

            #region oldMenu
            //Console.WriteLine("Доступные функции:");
            //Console.WriteLine("1 - Добавить продукт:");
            //Console.WriteLine("2 - Добавить прием пищи");
            //Console.WriteLine("3 - Отобразить приемы пищи");
            //Console.WriteLine("4 - Сменить пользователья");
            //int action = GetInt("");
            //switch (action)
            //{
            //    case 1:
            //        {
            //            while(true)
            //                {
            //                Console.WriteLine("Введите название продукта или \"Выход\" ");
            //                string product = Console.ReadLine();
            //                if (product != "Выход")
            //                {
            //                    if (eatingController.isFoodExist(product))
            //                        Console.WriteLine("Такой продукт уже существует");
            //                    else AddNewFood(eatingController, product);
            //                }
            //                else
            //                    break;
            //            }
            //            break;
            //        }
            //    case 2:
            //        {
            //            string message = "Введите продукт";
            //            bool isFirst = true;
            //            while (AddProductToEating(eatingController, ref message, ref isFirst))
            //            { }
            //            break;
            //        }
            //    case 3:
            //        {
            //            ShowEatings(eatingController);
            //            break;
            //        }
            //    case 4:
            //        {
            //            userController = UserSelect();
            //            eatingController.ChangeUser(userController.CurrentUser);
            //            break;
            //        }
            //}
            #endregion

            Console.WriteLine("Доступные функции:");
            foreach(var el in actions)
            {
                Console.WriteLine(el.Key + " - " + el.Value.Name);
            }
            int action = GetInt("Выберите действие");
            actions[action].Action.Invoke(eatingController, userController);
        }


        public static bool AddProductToEating(EatingController ec, ref string message, ref bool isFirst)
        {
            Console.WriteLine(message);
            string product = Console.ReadLine();
            if (product == "Выход" && (!isFirst))
            {
                ec.SaveEating();
                return false;
            }
            int quantity = GetInt("Введите размер порции");
            if (!ec.AddEating(product, quantity))
            {
                AddNewFood(ec, product);
                ec.AddEating(product, quantity);
            }
            message = "Введите продукт или \"Выход\" ";
            isFirst = false;
            return true;
        }

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

        public static void AddNewFood(EatingController ec, string product)
        {
            int calls = GetInt("Введите каллорийность на 100г");
            int fats = GetInt("Введите кол-во жиров на 100г");
            int prots = GetInt("Введите кол-во белков на 100г");
            int carbs = GetInt("Введите кол-во углеводов на 100г");
            ec.AddFoodElem(product, calls, fats, prots, carbs);
        }
        public static void AddProductHandler(EatingController eatingController, UserController userController)
        {
            while (true)
            {
                Console.WriteLine("Введите название продукта или \"Выход\" ");
                string product = Console.ReadLine();
                if (product != "Выход")
                {
                    if (eatingController.isFoodExist(product))
                        Console.WriteLine("Такой продукт уже существует");
                    else AddNewFood(eatingController, product);
                }
                else
                    break;
            }
        }

        public static void AddEatingHandler(EatingController eatingController, UserController userController)
        {
            string message = "Введите продукт";
            bool isFirst = true;
            while (AddProductToEating(eatingController, ref message, ref isFirst))
            { }
        }
        public static void ShowEatingsHandler(EatingController eatingController, UserController userController)
        {
            ShowEatings(eatingController);
        }
        public static void ChangeUserHandler(EatingController eatingController, UserController userController)
        {
            userController = UserSelect();
            eatingController.ChangeUser(userController.CurrentUser);
        }
    }
}
