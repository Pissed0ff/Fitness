using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    [Serializable]
    public class Gender
    {
        public int Id { get; set; }
        public value gender { get; private set; }
        public enum value
        {
            male=1,
            female,
        }

        public Gender()
        { }
        public Gender(value val)
        {
            gender = val;
        }

        public override string ToString()
        {
            return gender.ToString();
        }

    }
}
