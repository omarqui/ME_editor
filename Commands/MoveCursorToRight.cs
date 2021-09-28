using System;

namespace ConsoleTest
{
    class MoveCursorToRight : TextEditorCommand
    {
        public override void Execute()
        {
            _textEditor.MoveCursorToRight();
            if (Console.BufferWidth <= Console.CursorLeft+1) return;
            Console.CursorLeft++;
        }
    }
}
