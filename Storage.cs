using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lesson_2
{
    static class Storage
    {
        public static void SaveToFile(string fileName, Person[] people)
        {
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, people);
            } 
        }

        public static Person[] LoadFromFile(string fileName)
        {
            try
            {
                using (StreamReader file = File.OpenText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (Person[])serializer.Deserialize(file, typeof(Person[]));
                }
            }
            catch
            {
                return new Person[0];
            }
        }
    }
}
