using System;

namespace ConsoleTest
{
    class MoveCursorToLeft : TextEditorCommand
    {
        public override void Execute()
        {
            _textEditor.MoveCursorToLeft();
            if (Console.CursorLeft == 0) return;            
            Console.CursorLeft--;
        }
    }
}
