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
            //CHANGE THESE BELOW 4 VARIABLE VALUES ACCORDING TO YOUR SYSTEM (BUILDING IT WITH VISUAL STUDIO)
            //IF YOU WOULD LIKE TO BUILD IT WITH ANY OTHER METHODS, PLEASE CHANGE THE VARIABLE "final_path" TO THE CORRECT LOCATION OF THE DLL FILE (INCLUDING THE DLL FILE NAME WITH EXTENSION)


            string path_of_project = @"C:\Users\user\Desktop\";  //UPTO THE FOLDER NAME "Reading Dll files" (note : make sure you end it with '\')
            string folder_name = @"Reading Dll files\";  //NAME OF FOLDER WHICH CONTAIN .sln file
            int dotnet_sdk_version = 5;  //example : 3, 5, 6, 7   - depends on what you use to compile it
            bool built_using_release = true;   //if you built the dll using debug, then please make it false



            string final_path = path_of_project + folder_name + @"Dll Generator\bin\" + ((built_using_release == true) ? @"Release\net" : @"Debug\net") + dotnet_sdk_version + @".0\Dll Generator.dll";
            
            var classes = GetIClass1(final_path);

            Console.WriteLine(final_path);

            if (classes == null)
            {
                throw new Exception("Path to the dll file is incorrect or the dll does not exist, please build the Dll generator project first to create dll");
            }

            Console.WriteLine("Classes found in the dll file:");

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
