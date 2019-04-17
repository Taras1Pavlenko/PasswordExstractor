using System;

namespace ITCloudAcademy.ReflectionQuest.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property)]
    public class PasswordIsHereAttribute : Attribute
    {
        public int ChunkNo { get; set; }
    }
}