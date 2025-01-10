namespace AAUG.DomainModels.Models.Tables.General;

public class ExpandEventFile
{
    public int Id { get; set; }
    public int MediaFileId { get; set; }
    public int ExpandEventTextId { get; set; }

    public MediaFile MediaFile { get; set; }
    public ExpandEventText ExpandEventText { get; set; }
}