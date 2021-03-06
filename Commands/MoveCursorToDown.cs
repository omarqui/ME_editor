using System;

namespace ConsoleTest
{
    class MoveCursorToDown : TextEditorCommand
    {
        public MoveCursorToDown(TextEditor textEditor) : base(textEditor)
        {
        }

        public override void Execute()
        {
            _textEditor.MoveCursorToDown();
            if (Console.BufferHeight <= Console.CursorTop+1) return;
            Console.CursorTop++;
        }
    }
}
