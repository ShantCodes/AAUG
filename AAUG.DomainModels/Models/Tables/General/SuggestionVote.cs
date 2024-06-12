using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DomainModels;

public class SuggestionVote
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SuggestionId { get; set; }
    public bool Vote { get; set; }

    public virtual AaugUser AaugUser { get; set; }
    public virtual Suggestion Suggestion { get; set; }
}
