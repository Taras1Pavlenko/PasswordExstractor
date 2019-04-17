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

            appropriateTypes = GetAppropriateTypes(types, nameof(IgnoreMeAttribute));


            CreateInstanceOf(types);

            Console.ReadKey();
            Console.WriteLine(password);
        }


        public static List<Type> GetAppropriateTypes(Type[] typesArray, string attributeType)
        {
            List<Type> types = new List<Type>();
            foreach (var type in typesArray)
            {
                object[] attrbs = type.GetCustomAttributes(false);
                if (attrbs.Length == 0)
                {
                    types.Add(type);
                }
                else
                {
                    foreach (var attrb in attrbs)
                    {
                        if (!(attributeType == attrb.GetType().Name))
                        {
                            types.Add(type);
                        }
                    }
                }
            }
            return types;
        }

        public static void CreateInstanceOf(Type[] types)
        {
            foreach (var obj in types)
            {
                var instanceOf = Activator.CreateInstance(obj.GetType());
                WorkWithObject(instanceOf);
            }
        }

        private static void WorkWithObject(object instanceOf)
        {
            MemberInfo[] memberInfos = instanceOf.GetType().GetMembers();
            foreach (var item in memberInfos)
            {
                Console.WriteLine(item);
            }
        }
    }
}
