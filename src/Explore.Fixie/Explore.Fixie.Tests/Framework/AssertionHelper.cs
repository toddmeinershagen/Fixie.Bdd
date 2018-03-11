using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System
{
    public static class AssertionHelper
    {
        public static void CheckAllPropertiesAreNotNull<T>(this T objectToInspect, params Func<T, object>[] getters)
        {
            if (getters.Any(f => f(objectToInspect) == null))
                Assert.Fail("some of the properties are null");
        }
    }
}