namespace AAUG.DomainModels;

public class Ticket
{
    public int Id { get; set; }
    public string TicketTitle { get; set; }
    public string? TicketDescription { get; set; }

    public virtual ICollection<UserTicketsRelation> UserTicketsRelations { get; set; }
}
