using System;
using System.Collections.Generic;

namespace AAUG.DomainModels.Models.Tables.General;

public partial class MediaFile
{
    public int Id { get; set; }
    public Guid Gid { get; set; }
    public short MediaFolderId { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public double Size { get; set; }
    public bool IsOmit { get; set; }
    public DateTime Date { get; set; }

    public MediaFolder? MediaFolder { get; set; }

    public virtual ICollection<AaugUser>? AaugUserNationalCardFiles { get; set; }

    public virtual ICollection<AaugUser>? AaugUserProfilePictureFiles { get; set; }

    public virtual ICollection<AaugUser>? AaugUserUniversityCardFiles { get; set; }
    public virtual ICollection<AaugUser>? AaugUserReceiptFiles { get; set; }
    public virtual ICollection<News>? News { get; set; }
    public virtual ICollection<SlideShow> SlideShows { get; set; }
}
