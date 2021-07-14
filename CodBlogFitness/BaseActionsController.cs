using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL
{
    public abstract class BaseActionsController<EL, AC> : BaseController where EL:BaseElement where AC:BaseActions 
    {
        protected List<EL> ListElements { get; set; } // список продуктов или упражнений
        protected List<AC> ListActions { get; set; } // список приемов пищи или тренировок
        protected AC CurrentAction { get; set; } // текущий прием пищи или тренировка

        public BaseActionsController() { }
        /// <summary>
        /// Контроллер
        /// </summary>
        /// <param name="user"> Пользователь</param>
        /// <param name="elementFileName"> Имя файла продуктов или упражнение</param>
        /// <param name="actionsFileName"> Имя файла приеов пищи или тренировок</param>
        public BaseActionsController(User user, string elementFileName, string actionsFileName)
        {
            CurrentUser = user ?? throw new ArgumentNullException("Пользователь не должен быть равен null", nameof(user));
            ListElements = LoadXml<EL>(elementFileName);
            ListActions = Load<AC>(actionsFileName);
        }

        /// <summary>
        /// Получение продукта или упражнения по имени
        /// </summary>
        /// <param name="elemName"></param>
        /// <returns></returns>
        public EL GetElemByName(string elemName)
        {
            return ListElements.FirstOrDefault(f => f.Name == elemName);
        }

        /// <summary>
        /// Получение имени текущего пользователя
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            return CurrentUser.ToString();
        }

        /// <summary>
        /// Метод замены текущего пользователя
        /// </summary>
        /// <param name="user"></param>
        public void ChangeUser(User user)
        {
            CurrentUser = user;
        }

        /// <summary>
        /// Получение списка приемов пищи или тренировок
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AC> GetUserActions()
        {
            var list = from e in ListActions
                       where (e.User.Name == CurrentUser.Name)
                       select e;
            return list;
        }
    }
}
