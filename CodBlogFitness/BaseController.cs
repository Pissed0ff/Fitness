using FitnessBL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;


namespace FitnessBL
{

    public abstract class BaseController
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public User CurrentUser { get; protected set; }

        public BaseController() { }

        /// <summary>
        /// Сохранение списка в банарном формате
        /// </summary>
        /// <typeparam name="T">Тип элементов списка</typeparam>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Save<T>(string fileName, List<T> obj)
        {
            var Formatter = new BinaryFormatter();
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                Formatter.Serialize(fs, obj);
                return true;
            }
        }

        /// <summary>
        /// Загрузка списка из бинарного файла
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Сохранение списка в формате xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public void SaveXml<T>(string fileName, List<T> obj)
        {
            var formatter = new XmlSerializer(typeof(List<T>));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, obj);
                Console.WriteLine("Saved successfully");
            }
        }

        /// <summary>
        /// Загрузка списка из файла xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<T> LoadXml<T>(string fileName)
        {
            var formatter = new XmlSerializer(typeof(List<T>));
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<T> obj)
                    return obj;
                else return new List<T>();
            }
        }

    }

}
