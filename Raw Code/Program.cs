using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Main_executable
{
    class Program
    {
        static List<Type> GetIClass1(string filename)
        {
            //READING FROM DISK TO MEMORY (THE DLL FILE)
            Assembly classLibrary1 = null;
            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byte[] buffer = new byte[1024];
                        int read = 0;
                        while ((read = fs.Read(buffer, 0, 1024)) > 0)
                            ms.Write(buffer, 0, read);
                        classLibrary1 = Assembly.Load(ms.ToArray());

                    }
                }
            }

            catch (Exception)
            {
                return null;
            }


            //FETCHING CLASSES FROM THE DLL            
            List<Type> items = new List<Type>();

            foreach (Type type in classLibrary1.GetExportedTypes())
            {
                if (type.IsClass == true)
                {
                    items.Add(type);
                }

            }
            return items;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please put the location and file name with extension of the dll file");

            var classes = GetIClass1(Console.ReadLine());  //PUT LOCATION AND FILE NAME OF DLL IN 


            if (classes == null)
            {
                throw new Exception("Path to the dll file is incorrect or the dll does not exist");
            }

            Console.WriteLine("\n\nClasses found in the dll file:");

            foreach (var class_ in classes)   //LOOPING THROUGH ALL THE CLASSES FOUND IN THE DLL
            {
                Console.WriteLine(class_.FullName);
            }

            //We can even create their instance by using Activator
            Console.WriteLine("\n\nCreating instance and running functions of these classes:");

            foreach (var class_ in classes)   //LOOPING THROUGH ALL THE CLASSES FOUND IN THE DLL
            {
                dynamic instance = Activator.CreateInstance(class_); //CREATING AN INSTANCE OF THE CLASS

                instance.work();    //RUNNING THE FUNCTION IN THE CLASSES TO DISPLAY

            }



        }
    }
}
