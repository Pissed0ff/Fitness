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
        
        public List<Food> ListFood { get; set; }
        public List<Eating> ListEatings { get; set; }
        private Eating CurrentEating { get; set; }

        /// <summary>
        /// Создание контроллера с пользователем, список продуктов загружается из XML.
        /// </summary>
        /// <param name="user"></param>
        public EatingController(User user): base(user)
        {
            //User = user ?? throw new ArgumentNullException("Пользователь не должен быть равен null", nameof(user));
            ListFood = LoadXml();
            ListEatings = LoadEatings();
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
        /// Добавление приема пищи. Возвращает frue если продукт есть в базе
        /// </summary>
        /// <param name="foodName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool AddEating(string foodName, int quantity)
        {
            var food = GetFoodByName(foodName);
            if (food != null)
            {
                if(CurrentEating == null)
                    CurrentEating = new Eating(User);
                CurrentEating.AddFood(food, quantity);
                return true; 
            }     
            else return false;
        }
        public Food GetFoodByName(string foodName)
        {
            return ListFood.FirstOrDefault(f => f.Name == foodName);
        }
        public bool isFoodExist(string foodName)
        {
            if (GetFoodByName(foodName) != null)
                return true;
            else return false;
        }
        
        /// <summary>
        /// Сохранение приемов пищи
        /// </summary>
        /// <param name="eating"></param>
        /// <returns></returns>
        public bool SaveEating()
        {
            ListEatings.Add(CurrentEating);
            CurrentEating = null;
            Save<Eating>("eating.dat", ListEatings);
            return true;
        }

        /// <summary>
        /// Загрузка Приемов пищи из файла
        /// </summary>
        /// <returns></returns>
        public List<Eating> LoadEatings()
        {
            return Load<Eating>("eating.dat");
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

        public IEnumerable<Eating> GetUserEating()
        {
            var list = from e in ListEatings
                       where (e.User.Name == User.Name)
                       select e;
            return list;
        }
        public string GetUserName()
        {
            return User.ToString();
        }

       public void ChangeUser(User user)
        {
            User = user;
        }

    }
}
