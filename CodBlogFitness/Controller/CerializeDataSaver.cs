using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FitnessBL.Controller
{
    class CerializeDataSaver : IDataSaver
    {
        public List<T> Load<T>(string fileName)
        {
            var Formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && Formatter.Deserialize(fs) is List<T> obj)
                    return obj;
                else return new List<T>();
            }
        }

        public void Save<T>(string fileName, List<T> obj)
        {
            var Formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                Formatter.Serialize(fs, obj);
            }
        }
    }
}
