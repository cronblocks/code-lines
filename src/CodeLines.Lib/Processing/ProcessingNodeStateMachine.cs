using CodeLines.Lib.Helpers;
using CodeLines.Lib.Types;

namespace CodeLines.Lib.Processing
{
    internal partial class ProcessingNode
    {
        private enum SMState
        {
            Normal,
            CommentLines
        }

        private SMState _smState = SMState.Normal;

        private void ResetStateMachine()
        {
            _smState = SMState.Normal;
        }

        private bool _smLineHasCode = false;
        private bool _smLineHasCommentText = false;
        private bool _smLineHasStartedMultilineComment = false;
        private bool _smLineHasEndedMultilineComment = false;

        private void UpdateStateMachine(ref FileResult fileResult, string trimmedLine)
        {
            if (string.IsNullOrEmpty(trimmedLine))
            {
                fileResult.BlankLines++;
            }
            else
            {
                UpdateLineInfo(trimmedLine);

                switch (_smState)
                {
                    case SMState.Normal:
                        if (_smLineHasCode)                    { fileResult.CodeLines++;          }
                        if (_smLineHasCommentText)             { fileResult.CommentLines++;       }
                        if (_smLineHasStartedMultilineComment) { _smState = SMState.CommentLines; }

                        break;

                    case SMState.CommentLines:
                        if (_smLineHasEndedMultilineComment && !_smLineHasStartedMultilineComment)
                        {
                            _smState = SMState.Normal;
                        }

                        if (_smLineHasCode)        { fileResult.CodeLines++;    }
                        if (_smLineHasCommentText) { fileResult.CommentLines++; }

                        break;
                }
            }
        }

        private void UpdateLineInfo(string trimmedLine)
        {
            //
            // Initializing flags' state
            //
            _smLineHasCode = false;
            _smLineHasCommentText = false;
            _smLineHasStartedMultilineComment = false;
            _smLineHasEndedMultilineComment = false;

            //
            // Capturing indices
            //
            int singleLineCommentIndex = -1;
            int multipleLineCommentStartIndex = -1;
            int multipleLineCommentEndIndex = -1;

            if (!string.IsNullOrEmpty(SingleLineCommentPattern) && trimmedLine.Contains(SingleLineCommentPattern))
            {
                singleLineCommentIndex = trimmedLine.IndexOf(SingleLineCommentPattern);
            }

            if (!string.IsNullOrEmpty(MultipleLineCommentStartPattern) &&
                !string.IsNullOrEmpty(MultipleLineCommentEndPattern))
            {
                // Both patterns must be defined for multiple-line comments

                if (MultipleLineCommentStartPattern != MultipleLineCommentEndPattern)
                {
                    // Both patterns are DIFFERENT

                    if (trimmedLine.Contains(MultipleLineCommentStartPattern))
                    {
                        multipleLineCommentStartIndex = trimmedLine.IndexOf(MultipleLineCommentStartPattern);
                    }

                    if (trimmedLine.Contains(MultipleLineCommentEndPattern))
                    {
                        multipleLineCommentEndIndex = trimmedLine.IndexOf(MultipleLineCommentEndPattern);
                    }
                }
                else if (MultipleLineCommentStartPattern == MultipleLineCommentEndPattern)
                {
                    // Both patterns are SAME

                    switch (_smState)
                    {
                        case SMState.Normal:
                            // Start Pattern should come first

                            if (trimmedLine.Contains(MultipleLineCommentStartPattern))
                            {
                                multipleLineCommentStartIndex = trimmedLine.IndexOf(MultipleLineCommentStartPattern);
                            }

                            if (multipleLineCommentStartIndex >= 0 &&
                                trimmedLine
                                .GetTrimmedPartAfter(MultipleLineCommentStartPattern)
                                .Contains(MultipleLineCommentEndPattern))
                            {
                                multipleLineCommentEndIndex =
                                                        multipleLineCommentStartIndex + MultipleLineCommentStartPattern.Length +
                                                        trimmedLine
                                                        .GetTrimmedPartAfter(MultipleLineCommentStartPattern)
                                                        .IndexOf(MultipleLineCommentEndPattern);
                            }

                            break;
                        case SMState.CommentLines:
                            // End Pattern should come first

                            if (trimmedLine.Contains(MultipleLineCommentEndPattern))
                            {
                                multipleLineCommentEndIndex = trimmedLine.IndexOf(MultipleLineCommentEndPattern);
                            }

                            if (multipleLineCommentEndIndex >= 0 &&
                                trimmedLine
                                .GetTrimmedPartAfter(MultipleLineCommentEndPattern)
                                .Contains(MultipleLineCommentStartPattern))
                            {
                                multipleLineCommentStartIndex =
                                                        multipleLineCommentEndIndex + MultipleLineCommentEndPattern.Length +
                                                        trimmedLine
                                                        .GetTrimmedPartAfter(MultipleLineCommentEndPattern)
                                                        .IndexOf(MultipleLineCommentStartPattern);
                            }

                            break;
                    }
                }
            }

            //
            // Deciding upon line construction
            //
            switch (_smState)
            {
                case SMState.Normal:

                    if ((multipleLineCommentStartIndex >= 0 && multipleLineCommentStartIndex > singleLineCommentIndex) ||
                        singleLineCommentIndex >= 0)
                    {
                        // For: [Code][Single-line Comment][Comment]
                        //      [Code][Single-line Comment][Comment][Multiple-line Comment Start]

                        string codePart = trimmedLine.GetTrimmedPartBefore(SingleLineCommentPattern);
                        string commentPart = trimmedLine.GetTrimmedPartAfter(SingleLineCommentPattern);

                        if (!string.IsNullOrEmpty(codePart))
                        {
                            _smLineHasCode = true;
                        }

                        if (!string.IsNullOrEmpty(commentPart))
                        {
                            _smLineHasCommentText = true;
                        }
                    }
                    else if (multipleLineCommentStartIndex >= 0 && multipleLineCommentEndIndex >= 0 &&
                             multipleLineCommentEndIndex > multipleLineCommentStartIndex)
                    {
                        // For: [Code][Multiple-line Comment Start][Comment][Multiple-line Comment End][Code]

                        string codePart1 = trimmedLine.GetTrimmedPartBefore(MultipleLineCommentStartPattern);
                        string codePart2 = trimmedLine.GetTrimmedPartAfter(MultipleLineCommentEndPattern);
                        string commentPart = trimmedLine.GetTrimmedPartBetween(
                                                            MultipleLineCommentStartPattern, MultipleLineCommentEndPattern);

                        if (!string.IsNullOrEmpty(codePart1) || !string.IsNullOrEmpty(codePart2))
                        {
                            _smLineHasCode = true;
                        }

                        if (!string.IsNullOrEmpty(commentPart))
                        {
                            _smLineHasCommentText = true;
                        }
                    }
                    else if (multipleLineCommentStartIndex >= 0)
                    {
                        // For: [Code][Multiple-line Comment Start][Comment]

                        string codePart = trimmedLine.GetTrimmedPartBefore(MultipleLineCommentStartPattern);
                        string commentPart = trimmedLine.GetTrimmedPartAfter(MultipleLineCommentStartPattern);

                        _smLineHasStartedMultilineComment = true;

                        if (!string.IsNullOrEmpty(codePart))
                        {
                            _smLineHasCode = true;
                        }

                        if (!string.IsNullOrEmpty(commentPart))
                        {
                            _smLineHasCommentText = true;
                        }
                    }
                    else
                    {
                        // For: [Code]

                        _smLineHasCode = true;
                    }

                    break;

                case SMState.CommentLines:

                    if (multipleLineCommentStartIndex >= 0 && multipleLineCommentEndIndex >= 0 &&
                        multipleLineCommentEndIndex < multipleLineCommentStartIndex)
                    {
                        // For: [Comment][Multiple-line Comment End][Code][Multiple-line Comment Start][Comment]

                        string codePart = trimmedLine.GetTrimmedPartBetween(MultipleLineCommentEndPattern, MultipleLineCommentStartPattern);
                        string commentPart1 = trimmedLine.GetTrimmedPartBefore(MultipleLineCommentEndPattern);
                        string commentPart2 = trimmedLine.GetTrimmedPartAfter(MultipleLineCommentStartPattern);

                        _smLineHasStartedMultilineComment = true;
                        _smLineHasEndedMultilineComment = true;

                        if (!string.IsNullOrEmpty(codePart))
                        {
                            _smLineHasCode = true;
                        }

                        if (!string.IsNullOrEmpty(commentPart1) || !string.IsNullOrEmpty(commentPart2))
                        {
                            _smLineHasCommentText = true;
                        }
                    }
                    else if (multipleLineCommentEndIndex >= 0)
                    {
                        // For: [Comment][Multiple-line Comment End][Code]

                        string codePart = trimmedLine.GetTrimmedPartAfter(MultipleLineCommentEndPattern);
                        string commentPart = trimmedLine.GetTrimmedPartBefore(MultipleLineCommentEndPattern);

                        _smLineHasEndedMultilineComment = true;

                        if (!string.IsNullOrEmpty(codePart))
                        {
                            _smLineHasCode = true;
                        }

                        if (!string.IsNullOrEmpty(commentPart))
                        {
                            _smLineHasCommentText = true;
                        }
                    }
                    else
                    {
                        // For: [Comment]

                        _smLineHasCommentText = true;
                    }

                    break;
            }
        }
    }
}
