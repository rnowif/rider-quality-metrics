using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.UI.RichText;
using ReSharperPlugin.QualityMetrics.Cohesion;

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

            var tightClassCohesion = CalculateTightClassCohesion(classElement);

            if (tightClassCohesion.IsError)
            {
                consumer.AddHighlighting(new CohesionErrorHighlight(declaration, GetMessage(declaration, classElement, tightClassCohesion), declaration.GetNameDocumentRange()));
            } else if (tightClassCohesion.IsWarning)
            {
                consumer.AddHighlighting(new CohesionWarningHighlight(declaration, GetMessage(declaration, classElement, tightClassCohesion), declaration.GetNameDocumentRange()));
            }

            consumer.AddHighlighting(new CohesionCodeInsightHighlight(declaration, tightClassCohesion, _provider));
        }

        private static TightClassCohesion CalculateTightClassCohesion(IClass classElement)
        {
            // TODO: Actually calculate it
            return TightClassCohesion.Create(60, 100);
        }

        private static string GetMessage(ITreeNode declaration, IDeclaredElement classElement, TightClassCohesion cohesion)
        {
            var declarationType = DeclaredElementPresenter.Format(declaration.Language, DeclaredElementPresenter.KIND_PRESENTER, classElement);
            var declaredElementName = DeclaredElementPresenter.Format(declaration.Language, DeclaredElementPresenter.NAME_PRESENTER, classElement);

            return $"{declarationType.Capitalize()} '{declaredElementName}' has a cohesion (TCC) of {cohesion.Value}";
        }
    }
}
