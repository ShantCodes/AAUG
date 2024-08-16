namespace AAUG.DomainModels.Enums;

public class MediaPaths
{
    public const short EventsFolder = 21;
    public const short ProfileFolder = 22;
    public const short CampsFolder = 23;
    public const short NewsFolder = 24;

    public static string Mapper(short id)
    {
        return id switch
        {
            EventsFolder => "EventsFolder",
            ProfileFolder => "ProfileFolder",
            CampsFolder => "CampsFolder",
            NewsFolder => "NewsFolder",
            _ => "0"
        };
    }
}