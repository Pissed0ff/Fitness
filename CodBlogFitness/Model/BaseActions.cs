using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    abstract public class BaseActions
    {
        public int Id { get; set; }
        public User User { get; set; }

        public BaseActions(User user)
        {
            User = user;
        }
        public BaseActions()
        { }
    }
}
