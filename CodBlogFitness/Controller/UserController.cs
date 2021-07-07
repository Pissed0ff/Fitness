using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Controller
{
    public class UserController
    {
        public List<User> Users { get; }
        public User CurrentUser { get; private set; }
        public bool IsNewUser {get;} = false;

        /// <summary>
        /// Метод сохранения списка в файл
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (Users == null)
                throw new ArgumentNullException("Пользователь не должен быть равен нулю", nameof(Users));
            var formatter = new BinaryFormatter();
            using ( var fs = new FileStream("user.dat", FileMode.OpenOrCreate) )
            {
                formatter.Serialize(fs, Users);
            }
            return true;
        }

        /// <summary>
        /// Получить сохраненный список пользователей
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("user.dat", FileMode.Open))
            {
                if( formatter.Deserialize(fs) is List<User> users)
                    return users;
                else
                {
                    return new List<User>();
                }
            }
        }

        /// <summary>
        /// Проверка на наличие пользователя в списке сохраненных
        /// </summary>
        /// <param name="name"></param>
        public UserController(string name)
        {
            Users = GetUsers();
            CurrentUser = Users.FirstOrDefault<User>(u => u.Name == name);
            if (CurrentUser == null)
            {
                CurrentUser = new User(name);
                IsNewUser = true;
            }
            
        }

        public void AddUserInformation(Gender gender,
                                       DateTime birthDate,
                                       double weight,
                                       double height)
        {
            CurrentUser = new User(CurrentUser.Name, gender, birthDate, weight, height);
            Users.Add(CurrentUser);
            if (Save())
                Console.WriteLine("Новый пользователь сохранен");
        }
        public UserController(string name,
                    Gender gender,
                    DateTime birthDate,
                    double weight,
                    double height)
        {
            Users.Add(new User(name, gender, birthDate, weight, height));     
        }
        public UserController()
        { }
        public string[] GetGenders()
        {
            string[] gen = System.Enum.GetNames(typeof(FitnessBL.Model.Gender.value));
            return gen;
        }
    }


}
