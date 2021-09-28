namespace ConsoleTest
{
    abstract class TextEditorCommand
    {
        protected TextEditor _textEditor;

        protected TextEditorCommand(TextEditor textEditor)
        {
            _textEditor = textEditor;
        }

        public abstract void Execute();        
    }
}
