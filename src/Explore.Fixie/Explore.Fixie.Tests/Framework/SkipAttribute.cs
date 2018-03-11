using System;

namespace Explore.Fixie.Tests.Framework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class SkipAttribute : Attribute
    {
        public SkipAttribute(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; private set; }
    }
}