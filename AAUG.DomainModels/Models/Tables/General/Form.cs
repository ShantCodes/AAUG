namespace AAUG.DomainModels.Models.Tables.General;

public class Form
{
    public int Id { get; set; }
    public string FormTitle { get; set; }
    public int CreatorUserId { get; set; }
    public virtual ICollection<FormQuestion> FormQuestions {get; set;}
}
