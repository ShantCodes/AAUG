namespace AAUG.DomainModels.Dtos.Media;

public class MediaFileInsertDto
{
    public Guid Gid { get; set; }
    public short MediaFolderId { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public double Size { get; set; }
    public bool IsOmit { get; set; }
    public DateTime Date { get; set; }
}

public class MediaFileGetDto
{
    public int Id { get; set; }
    public Guid Gid { get; set; }
    public short MediaFolderId { get; set; }
    public string Name { get; set; }
    public string Extension { get; set; }
    public double Size { get; set; }
    public bool IsOmit { get; set; }
    public DateTime Date { get; set; }  
}

