using System;

namespace ConsoleTest
{
    abstract class TextEditor {
        protected KeyboardListener[] _keyboardListeners;

        public abstract void Delete();

        public abstract void Startup();

        public abstract void MoveCursorToDown();

        public abstract void MoveCursorToUP();

        public abstract void MoveCursorToRight();

        public abstract void MoveCursorToLeft();

        public void setKeyboardListeners(KeyboardListener[] keyboardListeners)
        {
            _keyboardListeners = keyboardListeners;
        }
    }
}
