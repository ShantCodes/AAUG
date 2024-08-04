using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DomainModels;

public partial class UserTicketsRelation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AaugUser? AaugUser {get; set;}
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
}
