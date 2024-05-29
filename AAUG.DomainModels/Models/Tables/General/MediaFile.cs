using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class MediaFile
{
    public int Id { get; set; }

    public string FileLocation { get; set; } = null!;

    public virtual ICollection<AaugUser> AaugUserNationalCardFiles { get; set; } = new List<AaugUser>();

    public virtual ICollection<AaugUser> AaugUserProfilePictureFiles { get; set; } = new List<AaugUser>();

    public virtual ICollection<AaugUser> AaugUserUniversityCardFiles { get; set; } = new List<AaugUser>();
}
