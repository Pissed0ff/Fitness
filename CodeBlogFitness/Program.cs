using System;
using FitnessBL.Model;
using FitnessBL.Controller ;

namespace Fitness
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует приложение");

            var userController = UserSelect();
            EatingController eatingController = new EatingController(userController.CurrentUser);
            eatingController.AddFoodElem("кабачок", 101, 22, 63, 24);
        }

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
                    if (int.TryParse(strGender, out intGender) && intGender <= genderArr.Length && intGender>0)
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
            else
            {
                Console.WriteLine("О! я вас помню");
            }
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
            while(true)
            {
                Console.Write(mes+": ");
                str = Console.ReadLine();
                if (int.TryParse(str, out x))
                {
                    return x;
                }
                else Console.WriteLine("Неверный формат");  
            }
            


        }


    }
}
