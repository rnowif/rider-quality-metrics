using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperPlugin.QualityMetrics;

[assembly: RegisterConfigurableSeverity(CohesionWarningHighlight.SeverityId, null, HighlightingGroupIds.CodeSmell,
    "Element lacks cohesion", "The cohesion of this element is below the warning threshold", Severity.WARNING)]

namespace ReSharperPlugin.QualityMetrics
{

    [ConfigurableSeverityHighlighting(SeverityId, KnownLanguage.ANY_LANGUAGEID, OverlapResolve = OverlapResolveKind.WARNING)]
    public class CohesionWarningHighlight : ICohesionHighlighting
    {
        public const string SeverityId = "PoorCohesion";

        private readonly DocumentRange _documentRange;

        public CohesionWarningHighlight(ITypeMemberDeclaration declaration, string message, DocumentRange documentRange)
        {
            ToolTip = message;
            _documentRange = documentRange;
        }

        public bool IsValid() => true;
        public DocumentRange CalculateRange() => _documentRange;
        public string ToolTip { get; }

        public string ErrorStripeToolTip => ToolTip;
    }
}
