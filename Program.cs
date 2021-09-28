using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyboardListener[] keyboardListeners = {
                new GeneralKeyboardListener(ConsoleKey.UpArrow, new MoveCursorToUP()),
                new GeneralKeyboardListener(ConsoleKey.DownArrow, new MoveCursorToDown()),
                new GeneralKeyboardListener(ConsoleKey.LeftArrow, new MoveCursorToLeft()),
                new GeneralKeyboardListener(ConsoleKey.RightArrow, new MoveCursorToRight()),
                new GeneralKeyboardListener(ConsoleKey.I, new PrintInfoCursor())
            };

            TextEditor textEditor = new FirtEditionTextEditor(keyboardListeners);
            textEditor.Startup();
        }
    }

    interface CommandFactory {
        TextEditorCommand createCommand();
    }
}
