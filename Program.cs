using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TextEditor textEditor = new FirtEditionTextEditor(new ConsoleManager());
            KeyboardListener[] keyboardListeners = GetKeyboardListeners(textEditor);
            textEditor.setKeyboardListeners(keyboardListeners);
            textEditor.Startup();
        }

        private static KeyboardListener[] GetKeyboardListeners(TextEditor textEditor)
        {
            return new KeyboardListener[]{
                new GeneralKeyboardListener(new MoveCursorToUP(textEditor), ConsoleKey.UpArrow),
                new GeneralKeyboardListener(new MoveCursorToDown(textEditor), ConsoleKey.DownArrow),
                new GeneralKeyboardListener(new MoveCursorToLeft(textEditor), ConsoleKey.LeftArrow),
                new GeneralKeyboardListener(new MoveCursorToRight(textEditor), ConsoleKey.RightArrow),
                // new GeneralKeyboardListener(ConsoleKey.I, new PrintInfoCursor()),
                new GeneralKeyboardListener(new Delete(textEditor), ConsoleKey.Backspace),
                new GeneralKeyboardListener(new Enter(textEditor), ConsoleKey.Enter)
            };
        }
    }

    interface CommandFactory {
        TextEditorCommand createCommand();
    }
}
