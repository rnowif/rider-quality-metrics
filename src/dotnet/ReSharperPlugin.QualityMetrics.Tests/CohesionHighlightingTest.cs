using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace ReSharperPlugin.QualityMetrics.Tests
{
    public class CohesionHighlightingTest : CSharpHighlightingTestBase
    {
        protected override string RelativeTestDataPath => "CSharp";

        protected override bool HighlightingPredicate(IHighlighting highlighting, IPsiSourceFile sourceFile, IContextBoundSettingsStore settingsStore)
        {
            return highlighting is ICohesionHighlighting;
        }

        [Test]
        public void TestClassWithNoTtc()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestClassWithLowTtc()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestClassWithOneMethod()
        {
            DoNamedTest2();
        }
    }
}
