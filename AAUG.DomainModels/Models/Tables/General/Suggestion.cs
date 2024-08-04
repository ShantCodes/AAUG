using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DomainModels;

public class Suggestion
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsApproved { get; set; }
    public bool Expired { get; set; }
    public int UserId { get; set; }
    public DateOnly Date { get; set; }

    public AaugUser? AaugUser { get; set; }
    public virtual ICollection<SuggestionVote> SuggestionVotes {get; set;}
}
