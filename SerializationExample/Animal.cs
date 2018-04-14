using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace SerializationExample
{   [Serializable()]
    public class Animal : ISerializable
    {
        public int AnimalID { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        private static List<Animal> theAnimalsList = new List<Animal>
        {
            new Animal("Mario", 12, 21),
            new Animal("Luigi", 55, 24),
            new Animal("Peach", 40, 20)
    };

        public Animal(){}
        public Animal (string name = "No Name", double weight = 0, double height = 0)
        {
            Name = name;
            Weight = weight;
            Height = height;
            //theAnimalsList.Add(this);
        }

        //Special constructor
        //take serialized object and create a C# object with it
        public Animal(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Weight = (double)info.GetValue("Weight", typeof(double));
            Height = (double)info.GetValue("Height", typeof(double));
        }
        
        //create serialized data object
        //creating multiple serialized objects, the key would need to be different each time
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Weight", Weight);
            info.AddValue("Height", Height);
            Console.WriteLine(info.GetType().ToString());
        }

        public override string ToString()
        {
            return string.Format($"{Name} weighs {Weight}lbs and is {Height} inches tall");
        }

        public static List<Animal> GetListOfAnimals()
        {
            return theAnimalsList;
        }

        public static void NullifyTheAnimalsList()
        {
            theAnimalsList = null;
        }

        public static void PopulateListOfAnimalsFromExternal(List<Animal> animallistin)
        {
            theAnimalsList = animallistin;
        }

        public static void DisplayAllAnimalsInList()
        {
            foreach(Animal a in theAnimalsList)
            {
                Console.WriteLine(a.ToString());
            }
        }



    }
}
