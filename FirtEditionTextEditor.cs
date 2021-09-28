using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest
{
    class FirtEditionTextEditor : TextEditor
    {
        const int INITIAL_LINE_POSITION = 0;
        Dictionary<int, string> lines;
        CursorPosition cursorPosition;

        public FirtEditionTextEditor()
        {
            lines = new Dictionary<int, string>();
            cursorPosition = new CursorPosition();
        }

        private void Read(IEnumerable<KeyboardListener> listeners)
        {
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                var keyRead = Console.ReadKey();
                var key = keyRead.Key;
                bool wasHandled = false;
                foreach (KeyboardListener listener in listeners)
                {
                    wasHandled = listener.OnKeyPress(key);
                    if (wasHandled) break;
                }

                if (wasHandled) continue;

                string currentLine = GetCurrentLineValue();
                builder.Clear();
                builder.Append(currentLine);
                builder.Append(keyRead.KeyChar);
                InsertLine(builder.ToString());
                cursorPosition.Left++;
                // Render();
                RenderCurrentLine();
            }
        }

        private string GetCurrentLineValue()
        {
            if (lines.ContainsKey(cursorPosition.Top))
                return lines[cursorPosition.Top];

            return Space(cursorPosition.Left);
        }

        private void InsertLine(string lineValue)
        {
            if (lines.ContainsKey(cursorPosition.Top))
                lines[cursorPosition.Top] = lineValue;
            else 
                lines.Add(cursorPosition.Top, lineValue);
        }

        private string Space(int cant)
        {
            if (cant <= 0) return "";

            return new string(' ', cant);
        }

        private void Render()
        {
            Console.Clear();

            CursorPosition previousCursor = (CursorPosition)cursorPosition.Clone();
            foreach (var line in lines)
            {
                Console.CursorTop = line.Key;
                Console.CursorLeft = INITIAL_LINE_POSITION;
                cursorPosition.Top = line.Key;
                Console.Write(line.Value);
            }
            cursorPosition = previousCursor;
            Console.CursorTop = cursorPosition.Top;
            Console.CursorLeft = cursorPosition.Left;
        }

        private void RenderCurrentLine()
        {
            CursorPosition previousCursor = (CursorPosition)cursorPosition.Clone();
            ClearLine(cursorPosition.Top);
            Console.Write(lines.GetValueOrDefault(cursorPosition.Top));            
            cursorPosition = previousCursor;
            Console.CursorLeft = cursorPosition.Left;
        }

        private void ClearLine(int top)
        {
            Console.CursorLeft = INITIAL_LINE_POSITION;
            Console.Write(Space(Console.BufferWidth));
            Console.CursorTop--;
        }

        public override void MoveCursorToDown()
        {
            cursorPosition.Top++;
        }

        public override void MoveCursorToLeft()
        {
            if (cursorPosition.Left == INITIAL_LINE_POSITION) return;
            cursorPosition.Left--;
        }

        public override void MoveCursorToRight()
        {
            cursorPosition.Left++;
        }

        public override void MoveCursorToUP()
        {
            if (cursorPosition.Top == INITIAL_LINE_POSITION) return;
            cursorPosition.Top--;
        }

        public override void Startup()
        {
            Console.Clear();
            lines.Add(3, "Hola mundo");
            Render();
            Read(_keyboardListeners);
        }
    }
}
