using System;

namespace ConsoleTest
{
    abstract class TextEditor {
        protected KeyboardListener[] keyboardListeners;

        public TextEditor(KeyboardListener[] listeners)
        {
            keyboardListeners = listeners;
            foreach (KeyboardListener listener in keyboardListeners)
            {
                listener.setTextEditor(this);
            }
        }
        
        public abstract void Startup();

        public abstract void MoveCursorToDown();

        public abstract void MoveCursorToUP();

        public abstract void MoveCursorToRight();

        public abstract void MoveCursorToLeft();
    }
}
