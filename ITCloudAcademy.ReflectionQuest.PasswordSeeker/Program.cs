using System;
using System.IO;
using System.Reflection;
using ITCloudAcademy.ReflectionQuest.Attributes;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class Program
    {
        static void Main()
        {
            string pathToDll = Path.GetFullPath("..\\..\\ITCloudAcademy.ReflectionQuest.Password.dll");

            string password = string.Empty; // TODO: Write password seeker by using reflection

            Assembly assembly = Assembly.LoadFrom(pathToDll);

            Type[] types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.GetCustomAttributes(false).Length == 0)
                {
                    Console.WriteLine(type.Name);
                }
            }

            Console.ReadKey();
            Console.WriteLine(password);
        }
    }
}
