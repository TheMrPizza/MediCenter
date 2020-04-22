using System;

namespace Client.IO.Abstract
{
    public interface IStreamIO
    {
        TextElementBase TextElement { get; }
        FieldElementBase<string> FieldTextElement { get; }
        FieldElementBase<DateTime> FieldDateElement { get; }
        ListElementBase ListElement { get; }
    }
}
