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
        protected List<EL> ListElements { get; set; }
        protected List<AC> ListActions { get; set; }
        protected AC CurrentAction { get; set; }

        public BaseActionsController(User user, string elementFileName, string actionsFileName)
        {
            CurrentUser = user ?? throw new ArgumentNullException("Пользователь не должен быть равен null", nameof(user));
            ListElements = LoadXml<EL>(elementFileName);
            ListActions = Load<AC>(actionsFileName);
        }

        public EL GetElemByName(string elemName)
        {
            return ListElements.FirstOrDefault(f => f.Name == elemName);
        }

        public string GetUserName()
        {
            return CurrentUser.ToString();
        }

        public void ChangeUser(User user)
        {
            CurrentUser = user;
        }

        public IEnumerable<AC> GetUserActions()
        {
            var list = from e in ListActions
                       where (e.User.Name == CurrentUser.Name)
                       select e;
            return list;
        }
    }
}
