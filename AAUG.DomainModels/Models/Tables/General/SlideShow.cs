namespace AAUG.DomainModels.Models.Tables.General;

public class SlideShow 
{
    public int Id { get; set; }
    public int MediaFileId { get; set; }
    public string? Description { get; set; }
    public short TitleId { get; set; }
    public bool IsActive { get; set; }

    //relations
    public virtual SlideShowTitle SlideShowTitle { get; set; }
    public virtual MediaFile MediaFile { get; set; }
}