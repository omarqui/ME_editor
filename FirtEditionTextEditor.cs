using System;
using System.Collections.Generic;
using System.Linq;
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
            RenderLine(cursorPosition.Top);
            cursorPosition = previousCursor;
            Console.CursorLeft = cursorPosition.Left;
        }

        private void RenderLine(int top)
        {
            ClearLine(top);
            Console.Write(lines.GetValueOrDefault(top));
        }

        private void ClearLine(int top)
        {
            Console.CursorTop = top;
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

        public override void Delete()
        {
            string currentLine = GetCurrentLineValue();
            int startIndex = cursorPosition.Left - 1;

            if (currentLine == null || currentLine == String.Empty) return;
            if (currentLine.Length <= startIndex) return;
            if (startIndex < 0) return;

            StringBuilder builder = new StringBuilder(currentLine);
            builder.Remove(startIndex, 1);
            InsertLine(builder.ToString());
            RenderCurrentLine();
        }

        public override void InsertLine()
        {
            var downLines = lines.Where(l => l.Key >= cursorPosition.Top)
                .OrderByDescending(l => l.Key)
                .ToList();

            downLines.ForEach(l =>
                {
                    lines.Remove(l.Key);
                    lines.Add(l.Key + 1, l.Value);
                });

            RenderDownLines(downLines.Select(l=>l.Key+1).ToArray());
        }        

        private void RenderDownLines(int[] lineKeys)
        {
            CursorPosition previousCursor = (CursorPosition)cursorPosition.Clone();
            foreach (var line in lineKeys)
            {
                ClearLine(line-1);
                Console.CursorTop = line;
                RenderLine(line);    
            }
            
            cursorPosition = previousCursor;
            Console.CursorTop = cursorPosition.Top;
            Console.CursorLeft = 0;
        }
    }
}
