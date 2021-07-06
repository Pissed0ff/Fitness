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
        public User _User { get; set; }
        public bool Save(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Пользователь не должен быть равен нулю", nameof(user));
            var formatter = new BinaryFormatter();
            using ( var fs = new FileStream("user.dat", FileMode.OpenOrCreate) )
            {
                formatter.Serialize(fs, user);
            }
            return true;
        }
        
        public UserController(string fail)
        {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream(fail, FileMode.Open))
            {
                User user = formatter.Deserialize(fs) as User;                    
            }

            //todo:  ЧТо делать есть пользователь не прочитан 
        }
        public UserController()
        {
            
        }
        public string[] GetGenders()
        {
            string[] gen = System.Enum.GetNames(typeof(FitnessBL.Model.Gender.value));
            return gen;
        }
    }


}
