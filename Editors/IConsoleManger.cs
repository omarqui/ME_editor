using System;
using System.Collections.Generic;

namespace ConsoleTest
{
    interface IConsoleManger
    {
        void MoveCursorTo(CursorPosition position);
        void ClearAll();
        void ClearLine(CursorPosition position);
        void RenderLineFromPosition(CursorPosition position, string content, bool restorePosition = false);
        ConsoleKeyInfo ReadKey();
        void RenderAll(CursorPosition currentPosition, Dictionary<int, string> lines);
    }
}
