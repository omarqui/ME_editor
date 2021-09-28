using System;
using System.Collections.Generic;

namespace ConsoleTest
{
    class ConsoleManager : IConsoleManger
    {
        public void ClearAll()
        {
            Console.Clear();
        }

        public void ClearLine(CursorPosition position)
        {
            // FIXME dont manipulate Console.Cursor without update local CursorPosition
            Console.CursorTop = position.Top;
            Console.CursorLeft = 0;
            Console.Write(Space(Console.BufferWidth));
            Console.CursorTop--;
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public void RenderLineFromPosition(CursorPosition position, string content, bool restorePosition = false)
        {
            ClearLine(position);
            Console.Write(content);
            if (restorePosition) MoveCursorTo(position);
        }

        void IConsoleManger.RenderAll(CursorPosition currentPosition, Dictionary<int, string> lines)
        {
            Console.Clear();
            foreach (var line in lines)
            {
                Console.CursorTop = line.Key;
                Console.CursorLeft = 0;
                Console.Write(line.Value);
            }

            MoveCursorTo(currentPosition);
        }

        public void MoveCursorTo(CursorPosition position)
        {
            Console.CursorTop = position.Top;
            Console.CursorLeft = position.Left;
        }

        static public string Space(int cant)
        {
            if (cant <= 0) return "";

            return new string(' ', cant);
        }
    }
}
