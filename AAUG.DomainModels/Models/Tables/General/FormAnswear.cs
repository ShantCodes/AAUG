namespace AAUG.DomainModels;

public class FormAnswear
{
    public int Id { get; set; }
    public int AnswearerUserId { get; set; }
    public int FormQuestionId { get; set; }
    public string Answear { get; set; }
    public virtual FormQuestion FormQuestion {get; set;}

}
