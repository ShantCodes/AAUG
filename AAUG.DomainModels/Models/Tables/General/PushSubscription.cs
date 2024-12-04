namespace AAUG.DomainModels.Models.Tables.General;

public class PushSubscription
{
    public int Id { get; set; }
    public string EndPoint  { get; set; }
    public string P256dhKey  { get; set; }
    public string AuthKey  { get; set; }
}