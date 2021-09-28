namespace ConsoleTest
{
    abstract class TextEditorCommand
    {
        protected TextEditor _textEditor;
        public abstract void Execute();

        public void setTextEditor(TextEditor textEditor)
        {
            _textEditor = textEditor;
        }
    }
}
