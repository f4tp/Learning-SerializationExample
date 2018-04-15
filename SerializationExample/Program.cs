using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
namespace SerializationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkWithBinarySerializer();
            WorkWithXmlSerializer();
            SerializeListOfAnimalsXml();
        }


        public static void WorkWithBinarySerializer()
        {
            //Creeate obj and send to serialized data
            Animal bowser1 = new Animal("Bowser", 45, 25);
            Stream streamW = File.Open("AnimalData.dat", FileMode.Create);
            BinaryFormatter myBinForm = new BinaryFormatter();
            myBinForm.Serialize(streamW, bowser1);
            streamW.Close();
            bowser1 = null;
            Console.ReadLine();


            // read it back in
            streamW = File.Open("AnimalData.dat", FileMode.Open);
            myBinForm = new BinaryFormatter();
            bowser1 = (Animal)myBinForm.Deserialize(streamW);
            Console.WriteLine(bowser1.ToString());
            Console.ReadLine();
        }


        private static void WorkWithXmlSerializer()
        {
            //create object and serialize it to XML
            Animal bowser2 = new Animal("Bowser", 50, 25);
            XmlSerializer mySerializer = new XmlSerializer(typeof(Animal));
            using (TextWriter tw = new StreamWriter(@"C:\Users\Quad\source\repos\SerializationExample\SerializationExample\bin\Debug\bowser.xml"))
            {
                mySerializer.Serialize(tw, bowser2);
            }
            bowser2 = null;
            //read in XML object and create C# object with it
            XmlSerializer myDeserializer = new XmlSerializer(typeof(Animal));
            TextReader reader = new StreamReader(@"C:\Users\Quad\source\repos\SerializationExample\SerializationExample\bin\Debug\bowser.xml");
            object obj = myDeserializer.Deserialize(reader);
            bowser2 = (Animal)obj;
            reader.Close();
            Console.WriteLine(bowser2.ToString());
            Console.ReadLine();

        }

        public static void SerializeListOfAnimalsXml()
        {
            //create xml serializable object list and send to xml file
            using (Stream fs = new FileStream(@"C:\Users\Quad\source\repos\SerializationExample\SerializationExample\bin\Debug\animals.xml", FileMode.Create))
            {
                XmlSerializer listSerializer = new XmlSerializer(typeof(List<Animal>));
                listSerializer.Serialize(fs, Animal.GetListOfAnimals());
            }
            Animal.NullifyTheAnimalsList();
            Console.ReadLine();

            //read the xml file back in again and create C# objects
            XmlSerializer serializer3 = new XmlSerializer(typeof(List<Animal>));
            using (FileStream fs2 = File.OpenRead(@"C:\Users\Quad\source\repos\SerializationExample\SerializationExample\bin\Debug\animals.xml"))
            {
                Animal.PopulateListOfAnimalsFromExternal((List<Animal>)serializer3.Deserialize(fs2));
            }
            Animal.DisplayAllAnimalsInList();
            Console.ReadLine();
            //test
        }

    }
}
