namespace AAUG.DomainModels.ViewModels;

public class EventLikeGetViewModel
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int UserId { get; set; }
    public AaugUserGetViewModel? User { get; set; }
}