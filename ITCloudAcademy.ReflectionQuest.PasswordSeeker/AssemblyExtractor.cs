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
            GetResult
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
                FieldsProcessing(type);
                PropsProcessing(type);
                MethodsProcessing(type);

            }
        }

        private void MethodsProcessing(Type type)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                if((Attribute.GetCustomAttribute(method, typeof(PasswordIsHereAttribute), false) != null))
                {
                    PasswordIsHereAttribute attribute = (PasswordIsHereAttribute)Attribute.GetCustomAttribute(method, typeof(PasswordIsHereAttribute));
                    object obj = Activator.CreateInstance(type);
                    int chunckNumber = attribute.ChunkNo;
                    string passwordChuck = method.Invoke(obj, new object[0] { } ).ToString();
                    AllMembers.Add(chunckNumber, passwordChuck);
                }
            }
        }

        private void PropsProcessing(Type type)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var prop in properties)
            {
                if ((Attribute.GetCustomAttribute(prop, typeof(PasswordIsHereAttribute), false) != null))
                {
                    PasswordIsHereAttribute attribute = (PasswordIsHereAttribute)Attribute.GetCustomAttribute(prop, typeof(PasswordIsHereAttribute));
                    int chunckNumber = attribute.ChunkNo;
                    string passwordChuck = prop.GetValue(Activator.CreateInstance(type)).ToString();
                    AllMembers.Add(chunckNumber, passwordChuck);
                }
            }
        }

        private void FieldsProcessing(Type type)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fieldInfos)
            {
                if ((Attribute.GetCustomAttribute(field, typeof(PasswordIsHereAttribute), false) != null))
                {
                    PasswordIsHereAttribute attribute = (PasswordIsHereAttribute)Attribute.GetCustomAttribute(field, typeof(PasswordIsHereAttribute));
                    int chunckNumber = attribute.ChunkNo;
                    string passwordChuck = field.GetValue(Activator.CreateInstance(type)).ToString();
                    AllMembers.Add(chunckNumber, passwordChuck);
                }
            }
        }
    }
}
