using System;

namespace Dll_Generator
{
    public class Class1
    {
        public void work()
        {
            Console.WriteLine("This is from Class1");
        }
    }

    public class Class2
    {
        public void work()
        {
            Console.WriteLine("This is from Class2");
        }
    }

    public class Class3
    {
        public void work()
        {
            Console.WriteLine("This is from Class3");
        }
    }

    public struct Struct1
    {
        public int X { get; set; }
        public float Y { get; set; }
    }

    public struct Struct2
    {
        public int X { get; set; }
        public float Y { get; set; }
    }
}
