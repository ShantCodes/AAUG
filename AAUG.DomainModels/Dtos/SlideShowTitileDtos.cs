namespace AAUG.DomainModels.Dtos;

public class SlideShowTitleInsertDto
{
    public string Description { get; set; }
}

public class SlideshowTitleGetDto
{
    public int Id { get; set; }
    public string Description { get; set; }
}