namespace AAUG.DomainModels.Dtos;

public class EventLikeInsertDto
{
    public int EventId { get; set; }
    public int UserId { get; set; }
}
public class EventLikeGetDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int UserId { get; set; }
    public AaugUserGetDto? User { get; set; }

    public class EventLikeDeleteDto
    {
    }
}

public class EventLikeDeleteDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public int UserId { get; set; }
}