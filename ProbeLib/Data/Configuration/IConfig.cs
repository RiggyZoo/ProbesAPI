namespace DemoLib.Data
{
    public interface IConfig
    {
        string ApiFilterEndPoint { get; set; }
        string AppId { get; set; }
        string FilterEndpoint { get; set; }
        string ListOfProbeEndPoint { get; set; }
        string OneProbeEndPoint { get; set; }
        string TableId { get; set; }
        string Token { get; set; }
        string Url { get; set; }
    }
}