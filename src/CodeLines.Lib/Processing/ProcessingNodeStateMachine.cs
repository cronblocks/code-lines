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

            }
        }
        }
        }

        private void UpdateLineInfo(string trimmedLine)
        {
            _smLineHasCode = false;
            _smLineHasCommentText = false;
            _smLineHasStartedMultilineComment = false;
            _smLineHasEndedMultilineComment = false;
            
                int singleLineCommentIndex = -1;
                int multipleLineCommentStartIndex = -1;
                int multipleLineCommentEndIndex = -1;
                int lastIndex = trimmedLine.Length - 1;

                if (!string.IsNullOrEmpty(SingleLineCommentPattern) && trimmedLine.Contains(SingleLineCommentPattern))
                {
                    singleLineCommentIndex = trimmedLine.IndexOf(SingleLineCommentPattern);
                }

                if (!string.IsNullOrEmpty(MultipleLineCommentStartPattern) && trimmedLine.Contains(MultipleLineCommentStartPattern))
                {
                    multipleLineCommentStartIndex = trimmedLine.IndexOf(MultipleLineCommentStartPattern);
                }

                if (!string.IsNullOrEmpty(MultipleLineCommentEndPattern) && trimmedLine.Contains(MultipleLineCommentEndPattern))
                {
                    multipleLineCommentEndIndex = trimmedLine.IndexOf(MultipleLineCommentEndPattern);
                }
        }
    }
}
