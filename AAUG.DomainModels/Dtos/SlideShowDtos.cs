namespace AAUG.DomainModels.Dtos;

public class SlideShowGetDto
{
    public int Id { get; set; }
    public int MediaFileId { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }

}

public class SlideShowInsertDto
{
    public int MediaFileId { get; set; }
    public string? Description { get; set; }
    public bool? IsActive {get ; set;} 
}