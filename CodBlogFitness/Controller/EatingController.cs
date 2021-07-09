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
    public class EatingController:BaseController
    {
        private User User { get; }
        public List<Food> ListFood { get; set; }
        public List<Eating> ListEatings { get; set; }

        /// <summary>
        /// Создание контроллера с пользователем, список продуктов загружается из XML.
        /// </summary>
        /// <param name="user"></param>
        public EatingController(User user)
        {
            User = user ?? throw new ArgumentNullException("Пользователь не должен быть равен null", nameof(user));
            ListFood = LoadXml();
        }

        /// <summary>
        /// Добавление продуктов в список с указанием БЖУ.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="calls"></param>
        /// <param name="fats"></param>
        /// <param name="prots"></param>
        /// <param name="carb"></param>
        public void AddFoodElem(string name, double calls, double fats, double prots, double carb)
        {
            ListFood.Add(new Food(name, calls, fats, prots, carb));
            SaveListFoodXml();
        }

        /// <summary>
        /// Добавление приема пищи.
        /// </summary>
        /// <param name="foodName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool AddEating(string foodName, int quantity)
        {
            var food = ListFood.FirstOrDefault(f => f.Name == foodName);
            if (food != null)
            {
                Eating newEating = new Eating(User);
                newEating.AddFood(food, quantity);
                return true; 
            }     
            else return false;
        }
        
        /// <summary>
        /// Сохранение приемов пищи
        /// </summary>
        /// <param name="eating"></param>
        /// <returns></returns>
        public bool SaveEating(Eating eating)
        {
            ListEatings.Add(eating);
            Save<Eating>("eating.dat", ListEatings);
            return true;
        }

        /// <summary>
        /// Загрузка Приемов пищи из файла
        /// </summary>
        /// <returns></returns>
        public bool LoadEatings()
        {
            ListEatings = Load<Eating>("eating.dat");
            return true;
        }

        /// <summary>
        /// Сохранение списка продуктов
        /// </summary>
        public void SaveListFood()
        {
            this.Save<Food>("eating.dat", ListFood);
        }
        public void SaveListFoodXml()
        {
            SaveXml<List<Food>>("eating.xml", ListFood);
        }
        public List<Food> LoadXml()
        {
            return LoadXml<List<Food>>("eating.xml");
        }
    }
}
