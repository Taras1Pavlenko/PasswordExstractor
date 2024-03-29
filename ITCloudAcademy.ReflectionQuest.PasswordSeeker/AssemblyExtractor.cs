﻿using ITCloudAcademy.ReflectionQuest.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker
{
    class AssemblyExtractor
    {
        private string pathToDll = Path.GetFullPath("..\\..\\ITCloudAcademy.ReflectionQuest.Password.dll");
        private StringBuilder password = new StringBuilder();
        private List<Type> types = new List<Type>();
        private SortedList<int, object> ChunksList = new SortedList<int, object>();
        public void Run()
        {
            GetNecessaryTypes();
            GetNecessaryMembers();
            password = GetResult();
            Console.WriteLine($"Password is: {password}");
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
        }
        private void GetNecessaryMembers()
        {
            foreach (var type in types)
            {
                object inctance = Activator.CreateInstance(type);
                FieldsProcessing(type, inctance);
                PropsProcessing(type, inctance);
                MethodsProcessing(type, inctance);
            }
        }
        private void MethodsProcessing(Type type, object inctance)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                if ((Attribute.GetCustomAttribute(method, typeof(PasswordIsHereAttribute), false) != null))
                {
                    PasswordIsHereAttribute attribute = (PasswordIsHereAttribute)Attribute.GetCustomAttribute(method, typeof(PasswordIsHereAttribute));
                    int chunkNumber = attribute.ChunkNo;
                    string passwordChuck = method.Invoke(inctance, new object[0] { }).ToString();
                    ChunksList.Add(chunkNumber, passwordChuck);
                }
            }
        }
        private void PropsProcessing(Type type, object inctance)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                if ((Attribute.GetCustomAttribute(prop, typeof(PasswordIsHereAttribute), false) != null))
                {
                    PasswordIsHereAttribute attribute = (PasswordIsHereAttribute)Attribute.GetCustomAttribute(prop, typeof(PasswordIsHereAttribute));
                    int chunkNumber = attribute.ChunkNo;
                    string passwordChuck = prop.GetValue(inctance).ToString();
                    ChunksList.Add(chunkNumber, passwordChuck);
                }
            }
        }
        private void FieldsProcessing(Type type, object inctance)
        {
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fieldInfos)
            {
                if ((Attribute.GetCustomAttribute(field, typeof(PasswordIsHereAttribute), false) != null))
                {
                    PasswordIsHereAttribute attribute = (PasswordIsHereAttribute)Attribute.GetCustomAttribute(field, typeof(PasswordIsHereAttribute));
                    int chunkNumber = attribute.ChunkNo;
                    string passwordChuck = field.GetValue(inctance).ToString();
                    ChunksList.Add(chunkNumber, passwordChuck);
                }
            }
        }
        private StringBuilder GetResult()
        {
            StringBuilder pass = new StringBuilder();
            foreach (var chunk in ChunksList)
            {
                pass.Append(chunk.Value);
            }
            return pass;
        }
    }
}
