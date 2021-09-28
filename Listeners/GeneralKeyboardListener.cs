using System;

namespace ConsoleTest
{
    class GeneralKeyboardListener : KeyboardListener
    {
        TextEditorCommand _command;
        ConsoleKey _key;

        public GeneralKeyboardListener(ConsoleKey keyToListen, TextEditorCommand command)
        {
            _command = command;
            _key = keyToListen;
        }
        
        public bool OnKeyPress(ConsoleKey keyPressed)
        {
            if (keyPressed != _key) return false;            
            _command.Execute();
            return true;
        }

        public void setTextEditor(TextEditor textEditor)
        {
            _command.setTextEditor(textEditor);
        }
    }
}
