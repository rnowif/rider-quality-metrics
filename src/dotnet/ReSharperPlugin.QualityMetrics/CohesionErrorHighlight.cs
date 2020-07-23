using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using ReSharperPlugin.QualityMetrics;

[assembly: RegisterConfigurableSeverity(CohesionErrorHighlight.SeverityId, null, HighlightingGroupIds.CodeSmell,
    "Element is not cohesive", "The cohesion of this element is below the error threshold", Severity.ERROR)]

namespace ReSharperPlugin.QualityMetrics
{

    [ConfigurableSeverityHighlighting(SeverityId, KnownLanguage.ANY_LANGUAGEID, OverlapResolve = OverlapResolveKind.ERROR)]
    public class CohesionErrorHighlight : ICohesionHighlighting
    {
        public const string SeverityId = "NoCohesion";

        private readonly DocumentRange _documentRange;

        public CohesionErrorHighlight(ITypeMemberDeclaration declaration, string message, DocumentRange documentRange)
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
