using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class AaugUser
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public int? ProfilePictureFileId { get; set; }

    public int? NationalCardFileId { get; set; }

    public int? UniversityCardFileId { get; set; }
    public int? ReceiptFileId { get; set; }

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }

    public bool IsApproved { get; set; }

    public DateTime? SubscribeDate { get; set; }

    public bool Subscribed { get; set; }
    public bool IsSubApproved { get; set; }

    public virtual ICollection<Camp>? Camps { get; set; }

    public virtual ICollection<EventLike>? EventLikes { get; set; }

    public virtual ICollection<Event>? Events { get; set; }

    public virtual MediaFile? NationalCardFile { get; set; }

    public virtual MediaFile? ProfilePictureFile { get; set; }

    public virtual MediaFile? UniversityCardFile { get; set; }
    public virtual MediaFile? ReceiptFile { get; set; }

    // public virtual ICollection<UserMajor>? UserMajors { get; set; }

    // public virtual ICollection<UserTalent>? UserTalents { get; set; }
    // public virtual ICollection<UserTicketsRelation>? UserTicketsRelations { get; set; }

    // public virtual ICollection<Suggestion>? Suggestions { get; set; }

    // public virtual ICollection<SuggestionVote>? SuggestionVotes { get; set; }

    // public virtual ICollection<FormQuestion>? FormQuestions { get; set; }

    public virtual ICollection<News>? News { get; set; }
    public virtual IdentityUser IdentityUser { get; set; }
}
