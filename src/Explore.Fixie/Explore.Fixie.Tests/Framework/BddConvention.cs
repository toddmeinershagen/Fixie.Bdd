using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Explore.Fixie.Tests.Framework;
using Fixie;

namespace Explore.Fixie.Tests
{
    public class TestcaseClassPerClass : BddConvention
    {
        public TestcaseClassPerClass(string[] include) : base(include)
        {
            Classes
                .Where(c => c.Name.EndsWith("Scenarios", StringComparison.InvariantCultureIgnoreCase))
                .Where(c => c.GetConstructors().All(ci => ci.GetParameters().Length == 0));
        }
    }

    public class TestcaseClassPerFixture : BddConvention
    {
        public TestcaseClassPerFixture(string[] include) : base(include)
        {
            Classes
                .Where(c => c.Name.IsGherkinStep())
                .Where(c => c.GetConstructors().Count() == 1);
        }
    }

    public abstract class BddConvention : Convention
    {
        protected BddConvention(string[] include)
        {
            var desiredCategories = include;
            var shouldRunAll = !desiredCategories.Any();

            Methods
                .Where(mi => mi.IsPublic && mi.IsVoid())
                .Where(m => m.Name.IsGherkinStep())
                .Where(method =>
                    shouldRunAll ||
                    MethodHasAnyDesiredCategory(method, desiredCategories) ||
                    ClassHasAnyDesiredCategory(method.DeclaringType, desiredCategories));

            Parameters
                .Add<FromExampleAttribute>();

            Lifecycle<FixtureLifecycle>();
        }

        protected bool ClassHasAnyDesiredCategory(Type type, string[] desiredCategories)
        {
            return Categories(type).Any(testCategory => desiredCategories.Contains(testCategory.Name, StringComparer.InvariantCultureIgnoreCase));
        }

        protected bool MethodHasAnyDesiredCategory(MethodInfo method, string[] desiredCategories)
        {
            return Categories(method).Any(testCategory => desiredCategories.Contains(testCategory.Name, StringComparer.InvariantCultureIgnoreCase));
        }

        protected CategoryAttribute[] Categories(Type type)
        {
            return type.GetCustomAttributes<CategoryAttribute>(true).ToArray();
        }

        protected CategoryAttribute[] Categories(MethodInfo method)
        {
            return method.GetCustomAttributes<CategoryAttribute>(true).ToArray();
        }

        protected class FromExampleAttribute : ParameterSource
        {
            public IEnumerable<object[]> GetParameters(MethodInfo method)
            {
                return method.GetCustomAttributes<ExampleAttribute>(true).Select(input => input.Parameters);
            }
        }
    }
}