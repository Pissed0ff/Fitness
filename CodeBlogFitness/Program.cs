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
            Console.WriteLine("Введите имя пользователя");
            string name  = Console.ReadLine();

            Console.WriteLine("Выберети пол:");
            string[] genderArr = new UserController().GetGenders();
            for (int i = 0; i < genderArr.Length;i++)
            {
                Console.WriteLine($"{i+1}: {genderArr[i]}");
            }
            Console.ReadLine();
            


        }
    }
}
