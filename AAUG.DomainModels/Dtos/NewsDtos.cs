namespace AAUG.DomainModels.Models.Tables.General;

public class NewsForInsertDto
{
    public int Id { get; set; }

    public string NewsTitle { get; set; } = null!;

    public string NewsDetails { get; set; } = null!;

    public int CreatorUserId { get; set; }
    public int NewsFileId { get; set; }


}

