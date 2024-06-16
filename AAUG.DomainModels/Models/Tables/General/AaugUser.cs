using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class AaugUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public int? RoleId { get; set; }

    public short MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public int? ProfilePictureFileId { get; set; }

    public int NationalCardFileId { get; set; }

    public int UniversityCardFileId { get; set; }

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }

    public bool IsApproved { get; set; }

    public virtual ICollection<Camp> Camps { get; set; } = new List<Camp>();

    public virtual ICollection<EventLike> EventLikes { get; set; } = new List<EventLike>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual MediaFile NationalCardFile { get; set; } = null!;

    public virtual MediaFile? ProfilePictureFile { get; set; }

    public virtual MediaFile UniversityCardFile { get; set; } = null!;

    public virtual ICollection<UserMajor> UserMajors { get; set; } = new List<UserMajor>();

    public virtual ICollection<UserTalent> UserTalents { get; set; } = new List<UserTalent>();
    public virtual ICollection<UserTicketsRelation> UserTicketsRelations { get; set; } 

    public virtual ICollection<Suggestion> Suggestions {get; set;}

    public virtual ICollection<SuggestionVote> SuggestionVotes {get; set;}

    public virtual ICollection<FormQuestion> FormQuestions {get; set;}
}
