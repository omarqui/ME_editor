using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TextEditor textEditor = new FirtEditionTextEditor();
            KeyboardListener[] keyboardListeners = GetKeyboardListeners(textEditor);
            textEditor.setKeyboardListeners(keyboardListeners);
            textEditor.Startup();
        }

        private static KeyboardListener[] GetKeyboardListeners(TextEditor textEditor)
        {
            return new KeyboardListener[]{
                new GeneralKeyboardListener(ConsoleKey.UpArrow, new MoveCursorToUP(textEditor)),
                new GeneralKeyboardListener(ConsoleKey.DownArrow, new MoveCursorToDown(textEditor)),
                new GeneralKeyboardListener(ConsoleKey.LeftArrow, new MoveCursorToLeft(textEditor)),
                new GeneralKeyboardListener(ConsoleKey.RightArrow, new MoveCursorToRight(textEditor)),
                // new GeneralKeyboardListener(ConsoleKey.I, new PrintInfoCursor()),
                new GeneralKeyboardListener(ConsoleKey.Backspace, new Delete(textEditor))
            };
        }
    }

    interface CommandFactory {
        TextEditorCommand createCommand();
    }
}
