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
            _textEditor.Delete();
            (new MoveCursorToLeft(_textEditor)).Execute();
        }
    }
}
