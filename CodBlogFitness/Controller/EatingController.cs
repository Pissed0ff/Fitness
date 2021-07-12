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
    public class EatingController:BaseActionsController<Food,Eating>
    {

        //public List<Food> ListFood { get; set; }
        //public List<Eating> ListEatings { get; set; }
        //private Eating CurrentEating { get; set; }
        private const string File_Food_Name = "food.xml";
        private const string File_Eating_Name = "eating.dat";

        /// <summary>
        /// Создание контроллера с пользователем, список продуктов загружается из XML.
        /// </summary>
        /// <param name="user"></param>
        public EatingController(User user): base(user, File_Food_Name, File_Eating_Name)
        {}

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
            ListElements.Add(new Food(name, calls, fats, prots, carb));
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
                if(CurrentAction == null)
                    CurrentAction = new Eating(CurrentUser);
                CurrentAction.AddFood(food, quantity);
                return true; 
            }     
            else return false;
        }
        public Food GetFoodByName(string foodName)
        {
            return GetElemByName(foodName);
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
            ListActions.Add(CurrentAction);
            CurrentAction= null;
            Save<Eating>(File_Eating_Name, ListActions);
            return true;
        }

        /// <summary>
        /// Загрузка Приемов пищи из файла
        /// </summary>
        /// <returns></returns>
        public List<Eating> LoadEatings()
        {
            return Load<Eating>(File_Eating_Name);
        }

        /// <summary>
        /// Сохранение списка продуктов
        /// </summary>
        public void SaveListFood()
        {
            Save<Food>( File_Food_Name, ListElements);
        }
        public void SaveListFoodXml()
        {
            SaveXml<Food>(File_Food_Name, ListElements);
        }
        public List<Food> LoadXml()
        {
            return LoadXml<Food>(File_Food_Name);
        }
        public IEnumerable<Eating> GetUserEating()
        {
            return GetUserActions();
        }
        

    }
}
