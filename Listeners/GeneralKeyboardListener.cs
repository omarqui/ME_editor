using System;
using System.Collections.Generic;

namespace ConsoleTest
{
    class GeneralKeyboardListener : KeyboardListener
    {
        TextEditorCommand _command;
        IEnumerable<ConsoleKey> _keys;

        public GeneralKeyboardListener(TextEditorCommand command, params ConsoleKey[] keysToListen)
        {
            _command = command;
            _keys = keysToListen;
        }

        public bool OnKeyPress(ConsoleKey keyPressed)
        {
            foreach (var key in _keys)
            {
                if (keyPressed != key) return false;
            }
            
            _command.Execute();
            return true;
        }
    }
}
