using System;

namespace ConsoleTest
{
    abstract class TextEditor {
        protected KeyboardListener[] _keyboardListeners;

        public abstract void Delete();

        public abstract void Startup();

        public abstract void MoveCursorToDown();

        public abstract CursorPosition GetCursorPosition();

        public abstract void MoveCursorToUP();

        public abstract void InsertLine();

        public abstract void MoveCursorToRight();

        public abstract void MoveCursorToLeft();

        public abstract void RemoveLine();

        public void SetKeyboardListeners(KeyboardListener[] keyboardListeners)
        {
            _keyboardListeners = keyboardListeners;
        }
    }
}
