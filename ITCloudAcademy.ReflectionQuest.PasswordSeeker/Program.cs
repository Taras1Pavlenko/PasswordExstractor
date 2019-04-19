using System;

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
