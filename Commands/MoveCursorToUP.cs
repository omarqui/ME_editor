using System;

namespace ConsoleTest
{
    class MoveCursorToUP : TextEditorCommand
    {
        public MoveCursorToUP(TextEditor textEditor) : base(textEditor)
        {
        }

        public override void Execute()
        {
            _textEditor?.MoveCursorToUP();
            if (Console.CursorTop == 0) return;
            Console.CursorTop--;
        }
    }
}
