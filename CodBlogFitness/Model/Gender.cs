using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    class Gender
    {
        /// <summary>
        /// 
        /// </summary>
        public value gender { get; private set; }
        public enum value
        {
            male,
            female,
        }

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
