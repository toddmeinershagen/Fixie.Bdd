using System;

namespace Explore.Fixie.Tests.Framework
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public abstract class CategoryAttribute : Attribute
    {
        public string Name => GetType().Name.Replace("Attribute", "");
    }
}