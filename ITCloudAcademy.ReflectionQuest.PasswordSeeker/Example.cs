using ITCloudAcademy.ReflectionQuest.Attributes;

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker.Namespace1
{
    public class Class2315324
    {
        [PasswordIsHere(ChunkNo = 1)]
        private string field2w3445 = "supper";

        [PasswordIsHere(ChunkNo = 3)]
        public string Property235 { get; } = "word";

        public string Property23e5 { get; } = "fake!";
    }
}

namespace ITCloudAcademy.ReflectionQuest.PasswordSeeker.Namespace2 { 

    public class Class345424
    {
        private string field26w3445 = "fake!";

        [PasswordIsHere(ChunkNo = 2)]
        public string Method23441() => "pass";
    }

    [IgnoreMe]
    public class Class2315324324
    {
        [PasswordIsHere(ChunkNo = 1)]
        private string field2w3445 = "fake!";
    }
}
