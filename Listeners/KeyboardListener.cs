using System;

namespace ConsoleTest
{
    interface KeyboardListener {
        bool OnKeyPress(ConsoleKey keyPressed);
        void setTextEditor(TextEditor textEditor);
    }
}
