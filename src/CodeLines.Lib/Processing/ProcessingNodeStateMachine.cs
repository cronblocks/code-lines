using CodeLines.Lib.Types;

namespace CodeLines.Lib.Processing
{
    internal partial class ProcessingNode
    {
        private enum State
        {
            Normal,
            CommentLines
        }

        private State _state;

        private void ResetStateMachine()
        {
            _state = State.Normal;
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
