using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Formatting;

namespace Raconteur.IDEIntegration.Intellisense
{
    internal class CompletionSource : ICompletionSource
    {
        private bool IsDisposed;

        public void Dispose()
        {
            if (IsDisposed) return;
            
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            var currentLine = GetCurrentLineFrom(session);
            var feature = session.TextView.TextSnapshot.GetText();
            var completions = new CompletionCalculator { Feature = feature };

            completionSets.Add(new CompletionSet("Steps", "Steps", GetSpanForLine(currentLine, session),
                completions.For(currentLine.Extent.GetText().Trim()), null));
        }

        private ITextViewLine GetCurrentLineFrom(ICompletionSession session)
        {
            return session.TextView.Caret.ContainingTextViewLine;
        }

        private ITrackingSpan GetSpanForLine(ITextViewLine line, ICompletionSession session)
        {
            var currentPoint = (session.TextView.Caret.Position.BufferPosition) - 1;
            return currentPoint.Snapshot.CreateTrackingSpan(line.ExtentIncludingLineBreak, SpanTrackingMode.EdgeInclusive);
        }
    }
}