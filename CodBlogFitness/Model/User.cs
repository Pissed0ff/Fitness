using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessBL.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Serializable]
    public class User
    {
        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public DateTime BirthDate { get; private set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        public User(string name,
                    Gender gender,
                    DateTime birthDate,
                    double weight,
                    double height)
        {
            if (name == null)
                throw new ArgumentNullException("Имя не может быть пустым",nameof(name));

            if (Gender == null)
                throw new ArgumentNullException("Пол не выбран", nameof(gender));

            if (birthDate < DateTime.Parse("01.01.1900") || birthDate> DateTime.Now)
                throw new ArgumentNullException("Неверная дата рождения", nameof(birthDate));

            if (weight <= 0)
                throw new ArgumentNullException("Неправильно указан вес ",nameof(weight));
            if (height <= 0)
                throw new ArgumentNullException("Неправильно указан рост", nameof(height));

            Name = name;
            Gender = gender;
            BirthDate = birthDate;
            Weight = weight;
            Height = height;

        }

        public override string ToString()
        {
            return Name;
        }

    }
}
