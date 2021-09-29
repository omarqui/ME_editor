using System;

namespace ConsoleTest
{
    class Delete : TextEditorCommand
    {
        public Delete(TextEditor textEditor) : base(textEditor)
        {
        }

        public override void Execute()
        {
            if (_textEditor.GetCursorPosition().Left == 0)
            {
                _textEditor.RemoveLine();
                (new MoveCursorToUP(_textEditor)).Execute();
                return;
            }

            _textEditor.Delete();
            (new MoveCursorToLeft(_textEditor)).Execute();
        }
    }
}
