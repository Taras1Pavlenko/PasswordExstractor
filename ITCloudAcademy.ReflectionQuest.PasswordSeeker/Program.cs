using System;
using System.Collections.Generic;
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

            List<Type> appropriateTypes = new List<Type>();

            Assembly assembly = Assembly.LoadFrom(pathToDll);

            Type[] types = assembly.GetTypes();

            appropriateTypes = GetAppropriateTypes(types, nameof(IgnoreMeAttribute), false);
            Console.ReadKey();
            Console.WriteLine(password);
        }

        public static List<Type> GetAppropriateTypes(Type[] typesArray, string attributeType, bool withThisAttrib)
        {
            List<Type> types = new List<Type>();
            foreach (var type in typesArray)
            {
                object[] attrbs = type.GetCustomAttributes(false);

                if (withThisAttrib)
                {
                    foreach (var attrb in attrbs)
                    {
                        if (attributeType.GetType() == attrb.GetType())
                        {
                            types.Add(type);
                        }
                    }
                }
                else
                {
                    foreach (var attrb in attrbs)
                    {
                        if (!(attributeType.GetType() == attrb.GetType()))
                        {
                            types.Add(type);
                        }
                    }
                }
            }
            return types;
        }
    }
}
