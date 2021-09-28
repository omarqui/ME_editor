using System;

namespace ConsoleTest
{
    class CursorPosition : ICloneable{
        public int Top { get; set; }
        public int Left { get; set; }

        public object Clone()
        {
            CursorPosition clone = new CursorPosition();
            clone.Top = Top;
            clone.Left = Left;
            return clone;
        }
    }
}
