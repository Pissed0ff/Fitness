using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Controller
{
    public interface IDataSaver
    {
        void Save<T>(string fileName, List<T> obj);
        public List<T> Load<T>(string fileName);
    }
}
