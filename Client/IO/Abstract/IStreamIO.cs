namespace Client.IO.Abstract
{
    public interface IStreamIO
    {
        TextElementBase TextElement { get; }
        FieldElementBase FieldElement { get; }
        ListElementBase ListElement { get; }
    }
}
