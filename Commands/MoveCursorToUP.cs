using System;

namespace ConsoleTest
{
    class MoveCursorToUP : TextEditorCommand
    {
        public override void Execute()
        {
            _textEditor?.MoveCursorToUP();
            if (Console.CursorTop == 0) return;
            Console.CursorTop--;
        }
    }
}
