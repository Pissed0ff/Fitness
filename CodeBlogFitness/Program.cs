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

            //ввод имени
            Console.WriteLine("Введите имя пользователя");
            string name  = Console.ReadLine();

            UserController controller = new UserController(name);

            if (controller.IsNewUser)
            {
                //ввод гендера
                Console.WriteLine("Выберети пол:");
                string[] genderArr = new UserController().GetGenders();
                for (int i = 0; i < genderArr.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {genderArr[i]}");
                }
                string strGender = Console.ReadLine();
                int intGender = 0;
                int.TryParse(strGender, out intGender);
                Gender.value genderVal = (Gender.value)Enum.GetValues(typeof(Gender.value)).GetValue(intGender - 1);
                Gender gender = new Gender(genderVal);

                //ввод даты рождения
                Console.WriteLine("Введите дату рождения");
                string strBirthDate = Console.ReadLine();
                DateTime birthDate = DateTime.MinValue;
                DateTime.TryParse(strBirthDate, out birthDate);

                //ввод роста
                Console.WriteLine("Введите свой рост");
                string strHeight = Console.ReadLine();
                int height = 0;
                int.TryParse(strHeight, out height);

                //ввод веса
                Console.WriteLine("Введите свой вес");
                string strWeight = Console.ReadLine();
                int weight = 0;
                int.TryParse(strHeight, out weight);

                controller.AddUserInformation(gender, birthDate, weight, height);
            }
            else
            {
                Console.WriteLine("О! я вас помню");
            }


        }
    }
}
