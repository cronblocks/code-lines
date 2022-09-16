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

            }
        }
    }
}
