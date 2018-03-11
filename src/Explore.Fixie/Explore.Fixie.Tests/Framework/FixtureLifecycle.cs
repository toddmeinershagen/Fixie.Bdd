using System;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Kernel;
using Fixie;

namespace Explore.Fixie.Tests.Framework
{
    public class FixtureLifecycle : Lifecycle
    {
        public void Execute(TestClass testClass, Action<CaseAction> runCases)
        {
            var methodWasExplicitlyRequested = testClass.TargetMethod != null;
            var skipClass = testClass.Type.Has<SkipAttribute>() && !methodWasExplicitlyRequested;
            object instance;

            if (skipClass)
            {
                instance = null;
            }
            else
            {
                var fixture = InitializeFixture();
                var method = typeof(ISpecimenBuilder).GetMethod("Create");
                instance = method.Invoke(fixture, new object[] { testClass.Type, new SpecimenContext(fixture) });
            }

            runCases(@case =>
            {
                var skipMethod = @case.Method.Has<SkipAttribute>() && !methodWasExplicitlyRequested;

                if (skipClass)
                    @case.Skip("Whole class skipped");
                else if (!skipMethod)
                    @case.Execute(instance);
            });

            instance.Dispose();
        }

        static IFixture InitializeFixture()
        {
            return new Fixture()
                .Customize(new AutoConfiguredNSubstituteCustomization());
        }
    }
}