using System.Linq;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.UI.RichText;
using ReSharperPlugin.QualityMetrics.Cohesion;

namespace ReSharperPlugin.QualityMetrics
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration))]
    public class CohesionProblemAnalyser : ElementProblemAnalyzer<IClassDeclaration>
    {
        private readonly CohesionInsightsProvider _provider;

        public CohesionProblemAnalyser(CohesionInsightsProvider provider)
        {
            _provider = provider;
        }

        protected override void Run(IClassDeclaration declaration, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            if (declaration.DeclaredElement == null)
            {
                return;
            }

            var tightClassCohesion = CalculateTightClassCohesion(declaration);

            if (tightClassCohesion.IsError)
            {
                consumer.AddHighlighting(new CohesionErrorHighlight(declaration, GetMessage(tightClassCohesion, declaration.Language, declaration.DeclaredElement), declaration.GetNameDocumentRange()));
            } else if (tightClassCohesion.IsWarning)
            {
                consumer.AddHighlighting(new CohesionWarningHighlight(declaration, GetMessage(tightClassCohesion, declaration.Language, declaration.DeclaredElement), declaration.GetNameDocumentRange()));
            }

            consumer.AddHighlighting(new CohesionCodeInsightHighlight(declaration, tightClassCohesion, _provider));
        }

        private static TightClassCohesion CalculateTightClassCohesion(IClassDeclaration declaration)
        {
            var methods = declaration.MethodDeclarations;

            var methodsIdentifiers = methods.Select(m => m.NameIdentifier.Name).ToList();
            var nbPossibleConnections = methodsIdentifiers.Count * (methodsIdentifiers.Count - 1) / 2;
            var numberOfEdges = methods
                .SelectMany(m => m.Descendants<IInvocationExpression>().Collect()
                    .Select(e => ((IReferenceExpression)e.InvokedExpression).NameIdentifier.Name))
                .Count(identifier => methodsIdentifiers.Contains(identifier));

            return TightClassCohesion.Create(numberOfEdges, nbPossibleConnections);
        }

        private static string GetMessage(TightClassCohesion cohesion, PsiLanguageType language, IDeclaredElement element)
        {
            var declarationType = DeclaredElementPresenter.Format(language, DeclaredElementPresenter.KIND_PRESENTER, element);
            var declaredElementName = DeclaredElementPresenter.Format(language, DeclaredElementPresenter.NAME_PRESENTER, element);

            return $"{declarationType.Capitalize()} '{declaredElementName}' has a cohesion (TCC) of {cohesion.Value:F2}";
        }
    }
}
