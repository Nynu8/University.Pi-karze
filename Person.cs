using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    internal class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public uint Age { get; set; }
        public uint Weight { get; set; }

        public Person(string name, string surname, uint age, uint weight)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Weight = weight;
        }

        //public Person(string name, string surname) : this(name, surname, 22, 25) { };

        public override string ToString()
        {
            return $"{Surname} {Name}, age: {Age}, weight: {Weight}";
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            if(!(obj is Person tmp))
            {
                return false;
            }

            if (tmp.Surname != this.Surname) return false;
            if (tmp.Name != this.Name) return false;
            if (tmp.Weight != this.Weight) return false;
            if (tmp.Age != this.Age) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return Surname.GetHashCode() ^ Name.GetHashCode() ^ Weight.GetHashCode() ^ Age.GetHashCode();
        }
    }
}
