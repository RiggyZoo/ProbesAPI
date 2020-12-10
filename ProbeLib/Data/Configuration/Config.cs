namespace DemoLib.Data
{
    public class Config : IConfig
    {
        public string Url { get; set; }
        public string ListOfProbeEndPoint { get; set; }
        public string FilterEndpoint { get; set; }
        public string AppId { get; set; }
        public string TableId { get; set; }
        public string Token { get; set; }
        public string ApiFilterEndPoint { get; set; }
        public string OneProbeEndPoint { get; set; }



    }
}