namespace AAUG.DomainModels.Models.Tables.General;

public class SlideShow 
{
    public int Id { get; set; }
    public int MediaFileId { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    //relations
    // public virtual SlideShowTitle Title { get; set; }
    public virtual MediaFile MediaFile { get; set; }
}