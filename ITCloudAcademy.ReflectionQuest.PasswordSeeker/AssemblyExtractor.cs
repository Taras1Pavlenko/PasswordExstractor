using ITCloudAcademy.ReflectionQuest.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class AssemblyExtractor
    {
        private string pathToDll = Path.GetFullPath("..\\..\\ITCloudAcademy.ReflectionQuest.Password.dll");
        private string password = String.Empty;
        private List<Type> types = new List<Type>();
        private SortedList<int, object> AllMembers = new SortedList<int, object>();

        public void Run()
        {
            GetNecessaryTypes();
        }

        private void GetNecessaryTypes()
        {
            Assembly assembly = Assembly.LoadFrom(pathToDll);
            Type[] allTypes = assembly.GetTypes();
            foreach (var type in allTypes)
            {
                if ((Attribute.GetCustomAttribute(type, typeof(IgnoreMeAttribute), false) == null))
                {
                    types.Add(type);
                }
            }
            GetGetNecessaryMembers();
        }

        private void GetGetNecessaryMembers()
        {
            foreach (var type in types)
            {
                FieldInfo[] memberInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var member in memberInfos)
                {
                    if ((Attribute.GetCustomAttribute(member, typeof(PasswordIsHereAttribute), false) != null))
                    {
                        PasswordIsHereAttribute password = (PasswordIsHereAttribute)Attribute
                            .GetCustomAttribute(member, typeof(PasswordIsHereAttribute));
                        int number = password.ChunkNo;
                        AllMembers.Add(number, CreateInstance(type, member));
                        Console.WriteLine(member);
                    }
                }
            }
        }

        private object CreateInstance(Type type, FieldInfo member)
        {
            var a = type.Name;
            var inctance = Activator.CreateInstance(type.GetType());
            
           return member.GetValue(type);
        }
    }
}
