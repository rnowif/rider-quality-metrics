using System.Linq;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.QualityMetrics
{
    [ElementProblemAnalyzer(typeof(ITypeMemberDeclaration))]
    public class CohesionProblemAnalyser : ElementProblemAnalyzer<ITypeMemberDeclaration>
    {
        private readonly CohesionInsightsProvider _provider;

        public CohesionProblemAnalyser(CohesionInsightsProvider provider)
        {
            _provider = provider;
        }

        protected override void Run(ITypeMemberDeclaration declaration, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            // We only analyse classes
            if (!(declaration.DeclaredElement is IClass classElement))
            {
                return;
            }

            // TODO: Actual calculate it
            var complexity = classElement.Methods.Count();

            consumer.AddHighlighting(new CohesionCodeInsightHighlight(declaration, complexity, _provider));
        }
    }
}
