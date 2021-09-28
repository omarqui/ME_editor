using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest
{
    class FirtEditionTextEditor : TextEditor
    {
        Dictionary<int, string> lines;
        CursorPosition cursorPosition;

        public FirtEditionTextEditor(KeyboardListener[] listeners) : base(listeners)
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

                cursorPosition.Left++;
                string currentLine = GetCurrentLineValue();                
                builder.Clear();
                builder.Append(currentLine);
                builder.Append(keyRead.KeyChar);
                InsertLine(builder.ToString());
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
                Console.CursorLeft = 0;
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
            Console.CursorLeft = 0;            
            Console.Write(lines.GetValueOrDefault(cursorPosition.Top));            
            cursorPosition = previousCursor;
            Console.CursorLeft = cursorPosition.Left;
        }

        public override void MoveCursorToDown()
        {
            cursorPosition.Top++;
        }

        public override void MoveCursorToLeft()
        {
            cursorPosition.Left--;
        }

        public override void MoveCursorToRight()
        {
            cursorPosition.Left++;
        }

        public override void MoveCursorToUP()
        {
            cursorPosition.Top--;
        }

        public override void Startup()
        {
            Console.Clear();
            lines.Add(3, "Hola mundo");
            Render();
            Read(keyboardListeners);
        }
    }
}
