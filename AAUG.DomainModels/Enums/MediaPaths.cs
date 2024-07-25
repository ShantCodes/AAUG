namespace AAUG.DomainModels.Enums;

public class MediaPaths
{
    public const short EventsFolder = 21;
    public const short ProfileFolder = 22;
    public const short CampsFolder = 23;

    public static string Mapper(short id)
    {
        return id switch
        {
            EventsFolder => "EventsFolder",
            ProfileFolder => "ProfileFolder",
            CampsFolder => "CampsFolder",
            _ => "0"
        };
    }
}