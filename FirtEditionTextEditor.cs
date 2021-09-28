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
        IConsoleManger _console;

        public FirtEditionTextEditor(IConsoleManger console)
        {
            lines = new Dictionary<int, string>();
            cursorPosition = new CursorPosition();
            _console = console;
        }

        private void Read(IEnumerable<KeyboardListener> listeners)
        {
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                var keyRead = _console.ReadKey();
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
                _console.RenderLineFromPosition(
                    cursorPosition, 
                    lines[cursorPosition.Top], 
                    true);
            }
        }

        private string GetCurrentLineValue()
        {
            if (lines.ContainsKey(cursorPosition.Top))
                return lines[cursorPosition.Top];

            return ConsoleManager.Space(cursorPosition.Left);
        }

        private void InsertLine(string lineValue)
        {
            if (lines.ContainsKey(cursorPosition.Top))
                lines[cursorPosition.Top] = lineValue;
            else
                lines.Add(cursorPosition.Top, lineValue);
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
            _console.ClearAll();
            lines.Add(3, "Hola mundo");
            _console.RenderAll(cursorPosition, lines);
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
            _console.RenderLineFromPosition(cursorPosition, lines[cursorPosition.Top]);
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
                cursorPosition.Top = line - 1;
                cursorPosition.Left = 0;
                _console.ClearLine(cursorPosition);
                cursorPosition.Top++;
                _console.RenderLineFromPosition(cursorPosition, lines[line]);    
            }

            cursorPosition = previousCursor;
            cursorPosition.Left = 0;
            _console.MoveCursorTo(cursorPosition);
        }
    }
}
