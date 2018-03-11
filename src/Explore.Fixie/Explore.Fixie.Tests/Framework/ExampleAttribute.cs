using System;

namespace Explore.Fixie.Tests.Framework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExampleAttribute : Attribute
    {
        public ExampleAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }

        public object[] Parameters { get; private set; }
    }
}