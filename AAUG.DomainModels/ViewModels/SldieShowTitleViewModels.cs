namespace AAUG.DomainModels.ViewModels;

public class SlideShowTitleInsertViewModel
{
    public string Description { get; set; }
}

public class SlideShowTitleGetViewModel
{
    public short? Id { get; set; }
    public string? Description { get; set; }
    public List<SlideShowGetViewModel> SlideShowGetViewModels { get; set; }
}