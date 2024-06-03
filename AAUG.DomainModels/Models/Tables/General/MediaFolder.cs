using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AAUG.DomainModels;

public partial class MediaFolder
{
    public short Id { get; set; }
    public short MediaDriveId { get; set; }
    public string Name { get; set; }
    public string VirtualName { get; set; }
    public short ParentId { get; set; }
}
