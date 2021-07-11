﻿using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Controller
{
    public class UserController: BaseController
    {
        private List<User> Users { get; }
        public User CurrentUser { get; private set; }
        public bool IsNewUser {get;} = false;

        /// <summary>
        /// Проверка на наличие пользователя в списке сохраненных
        /// </summary>
        /// <param name="name"></param>
        public UserController(string name)
        {
            Users = Load<User>("user.dat");
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
            if (Save<User>("user.dat",Users))
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
