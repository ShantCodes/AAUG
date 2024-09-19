namespace AAUG.DomainModels.ViewModels;

public class SlideShowGetViewModel
{
    public int Id { get; set; }
    public int MediaFileId { get; set; }
    public string? Description { get; set; }
    public short TitleId { get; set; }
    public bool IsActive { get; set; }

}