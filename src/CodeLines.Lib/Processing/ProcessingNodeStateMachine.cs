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

        private void UpdateStateMachine(ref FileResult fileResult, string trimmedLine)
        {
            if (string.IsNullOrEmpty(trimmedLine))
            {
                fileResult.BlankLines++;
            }
            else
            {
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

                switch (_smState)
                {
                    case SMState.Normal:
                        break;
                    
                    case SMState.CommentLines:
                        break;
                }
            }
        }
    }
}
