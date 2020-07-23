using System.Collections.Generic;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon.CodeInsights;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Rider.Model;
using JetBrains.TextControl.DocumentMarkup;
using ReSharperPlugin.QualityMetrics;
using ReSharperPlugin.QualityMetrics.Cohesion;
using Severity = JetBrains.ReSharper.Feature.Services.Daemon.Severity;

[assembly: RegisterHighlighter(CohesionCodeInsightHighlight.HighlightAttributeId, EffectType = EffectType.NONE, TransmitUpdates = true, Layer = HighlighterLayer.SYNTAX + 1, GroupId = HighlighterGroupIds.HIDDEN)]

namespace ReSharperPlugin.QualityMetrics
{
    [SolutionComponent]
    public class CohesionInsightsProvider : ICodeInsightsProvider
    {
        public bool IsAvailableIn(ISolution solution)
        {
            return true;
        }

        public void OnClick(CodeInsightsHighlighting highlighting, ISolution solution)
        {
        }

        public void OnExtraActionClick(CodeInsightsHighlighting highlighting, string actionId, ISolution solution)
        {
        }

        public string ProviderId => nameof(CohesionInsightsProvider);
        public string DisplayName => "Cohesion";
        public CodeLensAnchorKind DefaultAnchor => CodeLensAnchorKind.Top;

        public ICollection<CodeLensRelativeOrdering> RelativeOrderings => new CodeLensRelativeOrdering[] {new CodeLensRelativeOrderingFirst()};
    }

    [StaticSeverityHighlighting(Severity.INFO, HighlightingGroupIds.CodeInsightsGroup, AttributeId = HighlightAttributeId)]
    public class CohesionCodeInsightHighlight : CodeInsightsHighlighting
    {
        public const string HighlightAttributeId = "Cohesion Code Insight Highlight";

        private static string GetLensText(TightClassCohesion tightClassCohesion)
            => (tightClassCohesion.IsError ? "non-cohesive" : tightClassCohesion.IsWarning ? "poorly cohesive" : "quite cohesive") + $" ({tightClassCohesion.Value})";

        private static string GetMoreText(TightClassCohesion tightClassCohesion) => $"Cohesion (TCC) of {tightClassCohesion.Value}";

        public CohesionCodeInsightHighlight(ITypeMemberDeclaration declaration, TightClassCohesion tightClassCohesion, ICodeInsightsProvider provider)
            : base(declaration.GetNameDocumentRange(), GetLensText(tightClassCohesion), GetMoreText(tightClassCohesion), GetMoreText(tightClassCohesion), provider, declaration.DeclaredElement, null)
        {
        }
    }
}
