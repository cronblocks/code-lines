using CodeLines.Lib.Types;

namespace CodeLines.Lib.Processing
{
    internal partial class ProcessingNode
    {
        private void ResetStateMachine()
        {

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
