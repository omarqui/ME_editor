using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    class FirtEditionTextEditor : TextEditor
    {
        const int INITIAL_LINE_POSITION = 0;
        Dictionary<int, string> _lines;
        CursorPosition _cursorPosition;
        IConsoleManger _console;

        public FirtEditionTextEditor(IConsoleManger console)
        {
            _lines = new Dictionary<int, string>();
            _cursorPosition = new CursorPosition();
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
                _cursorPosition.Left++;
                // Render();
                _console.RenderLineFromPosition(
                    _cursorPosition, 
                    _lines[_cursorPosition.Top], 
                    true);
            }
        }

        private string GetCurrentLineValue()
        {
            if (_lines.ContainsKey(_cursorPosition.Top))
                return _lines[_cursorPosition.Top];

            return ConsoleManager.Space(_cursorPosition.Left);
        }

        private void InsertLine(string lineValue)
        {
            if (_lines.ContainsKey(_cursorPosition.Top))
                _lines[_cursorPosition.Top] = lineValue;
            else
                _lines.Add(_cursorPosition.Top, lineValue);
        }

        public override void MoveCursorToDown()
        {
            _cursorPosition.Top++;
        }

        public override void MoveCursorToLeft()
        {
            if (_cursorPosition.Left == INITIAL_LINE_POSITION) return;
            _cursorPosition.Left--;
        }

        public override void MoveCursorToRight()
        {
            _cursorPosition.Left++;
        }

        public override void MoveCursorToUP()
        {
            if (_cursorPosition.Top == INITIAL_LINE_POSITION) return;
            _cursorPosition.Top--;
        }

        public override void Startup()
        {
            _console.ClearAll();
            _lines.Add(3, "Hola mundo");
            _console.RenderAll(_cursorPosition, _lines);
            Read(_keyboardListeners);
        }

        public override void Delete()
        {
            string currentLine = GetCurrentLineValue();
            int startIndex = _cursorPosition.Left - 1;

            if (startIndex <= 0) return;
            if (currentLine == null || currentLine == String.Empty) return;
            if (currentLine.Length <= startIndex) return;
            

            StringBuilder builder = new StringBuilder(currentLine);
            builder.Remove(startIndex, 1);
            InsertLine(builder.ToString());
            _console.RenderLineFromPosition(_cursorPosition, _lines[_cursorPosition.Top]);
        }

        public override void RemoveLine()
        {
            var downLines = MoveDownLines(-1);
            RenderDownLines(downLines);
        }

        public override void InsertLine()
        {
            var downLines = MoveDownLines(1);
            RenderDownLines(downLines);
        }

        private IEnumerable<KeyValuePair<int, string>> MoveDownLines(int linesToMove)
        {
            var downLines = _lines.Where(l => l.Key >= _cursorPosition.Top)
                .OrderByDescending(l => l.Key)
                .ToList();

            downLines.ForEach(l =>
            {
                _lines.Remove(l.Key);
                _lines.Add(l.Key + linesToMove, l.Value);
            });

            var newDownLines = _lines.Where(l => l.Key >= _cursorPosition.Top)
                .OrderByDescending(l => l.Key);

            return newDownLines;
        }

        private void RenderDownLines(IEnumerable<KeyValuePair<int, string>> lineKeys)
        {
            CursorPosition previousCursor = (CursorPosition)_cursorPosition.Clone();
            
            foreach (var line in lineKeys)
            {
                _cursorPosition.Top = line.Key - 1;
                _cursorPosition.Left = 0;
                _console.ClearLine(_cursorPosition);
                _cursorPosition.Top++;
                _console.RenderLineFromPosition(_cursorPosition, line.Value);    
            }

            _cursorPosition = previousCursor;
            _cursorPosition.Left = 0;
            _console.MoveCursorTo(_cursorPosition);
        }

        public override CursorPosition GetCursorPosition()
        {
            return _cursorPosition;
        }
    }
}
