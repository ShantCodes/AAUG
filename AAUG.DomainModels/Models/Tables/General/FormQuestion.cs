using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DomainModels;

public class FormQuestion
{
    public int Id { get; set; }
    public string Question { get; set; }
    public int FormId { get; set; }
    //public virtual Form Form {get; set;}
    //public virtual ICollection<FormAnswear> FormAnswears {get; set;}
}
