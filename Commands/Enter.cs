using System;

namespace ConsoleTest
{
    class Enter : TextEditorCommand
    {
        public Enter(TextEditor textEditor) : base(textEditor)
        {
        }

        public override void Execute()
        {
            (new MoveCursorToDown(_textEditor)).Execute();
            _textEditor.InsertLine();
        }
    }
}
