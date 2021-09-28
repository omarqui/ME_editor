using System;

namespace ConsoleTest
{
    class PrintInfoCursor : TextEditorCommand
    {
        public override void Execute()
        {
            Console.WriteLine($"Left: {Console.CursorLeft} / Top: {Console.CursorTop} | BufferHeight: {Console.BufferHeight} / BufferWidth: {Console.BufferWidth}");
        }
    }
}