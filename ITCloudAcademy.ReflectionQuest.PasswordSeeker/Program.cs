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
            AssemblyExtractor assemblyExtractor = new AssemblyExtractor();
            assemblyExtractor.Run();

            Console.ReadKey();
        }
    }
}
