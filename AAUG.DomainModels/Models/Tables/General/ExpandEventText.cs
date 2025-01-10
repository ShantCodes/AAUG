namespace AAUG.DomainModels.Models.Tables.General;

public class ExpandEventText
{
    public int Id { get; set; }
    public string Details { get; set; }
    public byte OrderBy { get; set; }
    public int EventId { get; set; }

    public virtual ICollection<ExpandEventFile> ExpandEventFiles { get; set; }
}