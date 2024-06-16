using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DomainModels;

public class FormQuestion
{
    public int Id { get; set; }
    public int CreatorUserId { get; set; }
    public string Question { get; set; }
    public string FormTopic { get; set; }

    public virtual AaugUser aaugUser {get; set;}
    public virtual ICollection<FormAnswear> FormAnswears {get; set;}
}
