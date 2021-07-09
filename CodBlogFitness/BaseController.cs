using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;


namespace FitnessBL
{
    [Serializable]
    public abstract class BaseController
    {
        public bool Save<T>(string fileName, List<T> obj)
        {
            var Formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                Formatter.Serialize(fs, obj);
                return true;
            }
        }
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

        public void SaveXml<T>(string fileName, T obj)
        {
            var formatter = new XmlSerializer(typeof(T));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, obj);
                Console.WriteLine("Saved successfully");
            }
        }

        public T LoadXml<T>(string fileName)
        {
            var formatter = new XmlSerializer(typeof(T));
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is T obj)
                    return obj;
                else return default;
            }
        }

    }

}
